using RAID2D.Client.Commands.DayTime;
using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Interaction_Handlers;
using RAID2D.Client.Managers;
using RAID2D.Client.Mementos;
using RAID2D.Client.Players;
using RAID2D.Client.Services;
using RAID2D.Client.States;
using RAID2D.Client.UI;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Diagnostics;

namespace RAID2D.Client;

public partial class MainForm : Form
{
    private readonly GameState gameState = new(new Point(0, 0), Direction.Right);
    private readonly ServerConnection server = new();
    private readonly Dictionary<string, ServerPlayer> serverPlayers = [];

    private readonly Player player = new();

    private readonly GUI UI = GUI.GetInstance();

    private readonly IDropSpawner dropSpawner = new DropSpawner();

    private readonly DayTime dayTime = new();
    private readonly DayTimeController dayTimeController = new();

    private static readonly IEntitySpawner dayEntitySpawner = new DayEntitySpawner();
    private static readonly IEntitySpawner nightEntitySpawner = new NightEntitySpawner();
    private IEntitySpawner entitySpawner = dayEntitySpawner;

    private readonly Dictionary<PictureBox, EntityContext> entityContexts = [];

    private readonly Stack<PlayerMemento> undoStack = new();

    private readonly InteractionHandlerBase animalInteractionHandler = new AnimalInteractionHandler();
    private readonly InteractionHandlerBase dropInteractionHandler = new DropInteractionHandler();
    private readonly InteractionHandlerBase enemyInteractionHandler = new EnemyInteractionHandler();

    public MainForm() { Initialize(); }

    void Initialize()
    {
        InitializeComponent();

        InitializeHandlers();
        InitializeGUI();
        InitializeDayTime();
        InitializeServer();
        InitializeDevTools();
        InitializeGameLoop();
        InitializePlayer();

        Debug.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void FixedUpdate(double deltaTime)
    {
        SendDataToServer();

        if (!IsFormFocused())
            return;

        HandlePlayerInput();

        if (player.IsDead() || UI.IsPaused())
            return;

        HandleGUI(deltaTime);
        HandleDayTime(deltaTime);

        SaveState();

        foreach (PictureBox entity in this.Controls.OfType<PictureBox>().ToList())
        {
            HandleEntityMovement(entity);

            dropInteractionHandler.HandleInteractionWithPlayer(entity);
            enemyInteractionHandler.HandleInteractionWithPlayer(entity);
            animalInteractionHandler.HandleInteractionWithPlayer(entity);

            foreach (PictureBox bullet in this.Controls.OfType<PictureBox>().ToList())
            {
                dropInteractionHandler.HandleInteractionWithBullet(entity, bullet);
                enemyInteractionHandler.HandleInteractionWithBullet(entity, bullet);
                animalInteractionHandler.HandleInteractionWithBullet(entity, bullet);
            }
        }
    }

    private void InitializeHandlers()
    {
        animalInteractionHandler.Player = this.player;
        animalInteractionHandler.DropSpawner = this.dropSpawner;
        animalInteractionHandler.EntitySpawner = this.entitySpawner;

        dropInteractionHandler.Player = this.player;
        dropInteractionHandler.DropSpawner = this.dropSpawner;
        dropInteractionHandler.EntitySpawner = this.entitySpawner;

        enemyInteractionHandler.Player = this.player;
        enemyInteractionHandler.DropSpawner = this.dropSpawner;
        enemyInteractionHandler.EntitySpawner = this.entitySpawner;
    }

    private void SaveState()
    {
        undoStack.TryPeek(out PlayerMemento? last);

        if (player.Kills % 10 == 0 && 
            player.Kills != 0 && 
            (last != null && last.Kills != player.Kills))
        {
            undoStack.Push(player.SaveState());
        }
    }

    private void Undo()
    {
        if (undoStack.Count > 0)
        {
            PlayerMemento lastState = undoStack.Pop();
            player.RestoreState(lastState);
        }
        else
        {
            Console.WriteLine("No saved states to undo.");
        }
    }
    private void InitializeDevTools()
    {
#if DEBUG
        UI.CreateDevButtons(
            onUndoClick: dayTimeController.Undo,
            player: player,
            server: server,
            onSpawnEntitiesClick: SpawnEntities,
            onButtonCreate: AddControl);
        server.Connect(Constants.ServerUrl);
#endif
    }

    private void InitializeServer()
    {
        server.SetCallbacks(GetDataFromServer);
    }

    private void InitializeGUI()
    {
        // Force fullscreen on startup
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.Sizable;
        this.Bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);

        // Pause the game on alt-tab
        this.Activated += (s, e) => UI.SetPauseMenuVisibility(false);
        this.Deactivate += (s, e) => UI.SetPauseMenuVisibility(true);

        // Initialize GUI elements
        UI.BindElements(FpsLabel, AmmoLabel, KillsLabel, CashLabel, HealthBar);
        UI.SetResolution(ClientSize);
        UI.CreatePauseMenu(
            onConnectClick: server.Connect,
            onDisconnectClick: async () => await server.DisconnectAsync(),
            onQuitClick: Application.Exit,
            onLastCheckpointClick: () =>
            {
                Undo();
                UI.SetPauseMenuVisibility(false);
            },
            onPanelCreate: AddControl
        );
    }

    private void InitializeDayTime()
    {
        dayTime.Initialize(
            this,
            onDayStart: () =>
            {
                dayTimeController.SetCommand(new SetDayCommand(dayTime));
                dayTimeController.Run();

                entitySpawner = dayEntitySpawner;
            },
            onNightStart: () =>
            {
                dayTimeController.SetCommand(new SetNightCommand(dayTime));
                dayTimeController.Run();
                entitySpawner = nightEntitySpawner;
            });

        dayTimeController.SetCommand(new SetDayCommand(dayTime));
        dayTimeController.Run();
    }

    private void InitializeGameLoop()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        double lastUpdateTime = stopwatch.Elapsed.TotalSeconds;

        Timer gameTimer = new()
        {
            Enabled = true,
            Interval = Constants.GameTimerInterval,
        };
        gameTimer.Tick += (s, e) =>
        {
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            double deltaTime = currentTime - lastUpdateTime;
            lastUpdateTime = currentTime;

            FixedUpdate(deltaTime);
        };
    }

    private void InitializePlayer()
    {
        player.OnEmptyMagazine += SpawnAmmoDrop;
        player.OnLowHealth += SpawnMedicalDrop;
        RestartGame();
    }

    private void HandleGUI(double deltaTime)
    {
        UI.UpdateFPS(1 / deltaTime);
    }

    private void HandleDayTime(double deltaTime)
    {
        dayTime.Update(deltaTime);
    }

    private async void SendDataToServer()
    {
        if (!server.IsConnected())
            return;

        gameState.Location = player.PictureBox.Location;
        gameState.Direction = player.Direction;

        await server.SendGameStateAsync(gameState);
    }

    private void GetDataFromServer(GameState gameState)
    {
        //Console.WriteLine($"Received gameState={gameState}");

        serverPlayers.TryGetValue(gameState.ConnectionID, out ServerPlayer? serverPlayer);

        if (serverPlayer == null)
        {
            ServerPlayer newPlayer = new();
            newPlayer.Create(gameState);

            serverPlayers.Add(gameState.ConnectionID, newPlayer);

            if (!newPlayer.IsRendered && newPlayer.PictureBox != null)
            {
                newPlayer.IsRendered = true;

                this.Invoke((MethodInvoker)delegate
                {
                    AddControl(newPlayer.PictureBox);
                });

                Console.WriteLine($"added new player at loc={newPlayer.PictureBox.Location}");
            }
        }
        else
        {
            this.Invoke((MethodInvoker)delegate
            {
                serverPlayer.Update(gameState);
            });
        }
    }

    private void HandlePlayerInput()
    {
        if (player.IsDead())
        {
            if (InputManager.IsKeyDown(Keys.Enter))
                RestartGame();
            return;
        }

        if (InputManager.IsKeyDownOnce(Keys.Escape))
            UI.TogglePauseMenuVisibility();

        if (UI.IsPaused())
            return;

        player.Move();

        if (InputManager.IsKeyDownOnce(Keys.Space))
            player.ShootBullet(AddControl, RemoveControl);
    }

    private void SpawnAmmoDrop()
    {
        IDroppableItem ammoDrop = dropSpawner.CreateDrop(Constants.DropAmmoTag);

        PictureBox ammoDropPictureBox = new AmmoDropBuilder()
            .SetTag(ammoDrop)
            .SetName(ammoDrop.Name)
            .SetImage(ammoDrop.Image)
            .SetLocation(ammoDrop.Location)
            .SetSize(ammoDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        AddControl(ammoDropPictureBox);
    }

    private void SpawnMedicalDrop()
    {
        IDroppableItem medicalDrop = dropSpawner.CreateDrop(Constants.DropMedicalTag);

        PictureBox medicalPictureBox = new MedicalDropBuilder()
            .SetTag(medicalDrop)
            .SetName(medicalDrop.Name)
            .SetImage(medicalDrop.Image)
            .SetLocation(medicalDrop.Location)
            .SetSize(medicalDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        AddControl(medicalPictureBox);
    }

    private void HandleEntityMovement(PictureBox entity)
    {
        if (!IsEnemyOrAnimal(entity))
            return;

        if (!entityContexts.TryGetValue(entity, out EntityContext? context))
        {
            context = new EntityContext();
            entityContexts[entity] = context;
        }

        uint fleeRadius = IsEnemy(entity) ? Constants.EnemyFleeRadius : Constants.AnimalFleeRadius;

        if (player.DistanceTo(entity) < fleeRadius)
        {
            if (IsAnimal(entity))
                context.SetState(new FleeState());
            else if (IsEnemy(entity))
                context.SetState(new ChaseState());
        }
        else
        {
            context.SetState(new IdleState());
        }

        context.UpdateState(entity, player);
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < Constants.AnimalCount; i++)
            animalInteractionHandler.SpawnEntity();

        for (int i = 0; i < Constants.EnemyCount; i++)
            enemyInteractionHandler.SpawnEntity();
    }

    private void RestartGame()
    {
        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
            RemoveControl(control);

        AddControl(player.Respawn());
        SpawnEntities();
    }

    private static bool IsEnemyOrAnimal(Control control) => IsAnimal(control) || IsEnemy(control);
    private static bool IsEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.EnemyTag);
    private static bool IsAnimal(Control animal) => animal.Tag as string is Constants.AnimalTag;

    private bool IsFormFocused() => ActiveForm == this;

    private void AddControl(Control control)
    {
        this.Controls.Add(control);
        control.BringToFront();
        this.player.PictureBox.BringToFront();
    }

    private void RemoveControl(Control control)
    {
        this.Controls.Remove(control);
        control.Dispose();
    }
}
