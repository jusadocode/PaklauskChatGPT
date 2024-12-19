﻿using RAID2D.Client.Commands.DayTime;
using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Effects;
using RAID2D.Client.Entities;
using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Interaction_Handlers;
using RAID2D.Client.Iterators;
using RAID2D.Client.Managers;
using RAID2D.Client.Mementos;
using RAID2D.Client.Players;
using RAID2D.Client.Services;
using RAID2D.Client.States;
using RAID2D.Client.UI;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System;
using System.Diagnostics;

namespace RAID2D.Client;

public partial class MainForm : Form
{
    public readonly GameState gameState = new(new Point(0, 0), Direction.Right);
    public readonly ServerConnection server = new();
    public readonly Dictionary<string, ServerPlayer> serverPlayers = [];

    public readonly Player player = new();

    public readonly GUI UI = GUI.GetInstance();

    public readonly IDropSpawner dropSpawner = new DropSpawner();

    public readonly DayTime dayTime = new();
    public readonly DayTimeController dayTimeController = new();

    public static readonly IEntitySpawner dayEntitySpawner = new DayEntitySpawner();
    public static readonly IEntitySpawner nightEntitySpawner = new NightEntitySpawner();
    public IEntitySpawner entitySpawner = dayEntitySpawner;

    public readonly Dictionary<PictureBox, EntityContext> entityContexts = [];

    public readonly Stack<PlayerMemento> undoStack = new();

    public readonly InteractionHandlerBase animalInteractionHandler = new AnimalInteractionHandler();
    public readonly InteractionHandlerBase dropInteractionHandler = new DropInteractionHandler();
    public readonly InteractionHandlerBase enemyInteractionHandler = new EnemyInteractionHandler();

    public readonly EntityList entityList = new();
    public readonly DropList dropList = new();
    public readonly BulletList bulletList = new();


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

    public void FixedUpdate(double deltaTime)
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

        for (var it = entityList.GetIterator(); it.HasNext();)
        {
            IEntity entity = it.Next();

            HandleEntityMovement(entity);
            enemyInteractionHandler.HandleInteractionWithPlayer(entity.PictureBox);

            for (var it2 = bulletList.GetIterator(); it2.HasNext();)
            {
                Bullet bullet = it2.Next();

                animalInteractionHandler.HandleInteractionWithBullet(entity.PictureBox, bullet.PictureBox);
                enemyInteractionHandler.HandleInteractionWithBullet(entity.PictureBox, bullet.PictureBox);
            }
        }

        for (var it = dropList.GetIterator(); it.HasNext();)
        {
            IDroppableItem drop = it.Next();

            dropInteractionHandler.HandleInteractionWithPlayer(drop.PictureBox);
        }
    }

    public void InitializeHandlers()
    {
        animalInteractionHandler.Form = this;
        dropInteractionHandler.Form = this;
        enemyInteractionHandler.Form = this;
    }

    public void SaveState()
    {
        undoStack.TryPeek(out PlayerMemento? last);

        if (player.Kills % 10 == 0 &&
            player.Kills != 0 &&
            last != null && last.Kills != player.Kills)
        {
            undoStack.Push(player.SaveState());
        }
    }

    public void Undo()
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
    public void InitializeDevTools()
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

    public void InitializeServer()
    {
        server.SetCallbacks(GetDataFromServer);
    }

    public void InitializeGUI()
    {
        // Force fullscreen on startup
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None; // paliekam fullscreen prasau
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

    public void InitializeDayTime()
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

    public void InitializeGameLoop()
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

    public void InitializePlayer()
    {
        player.OnEmptyMagazine += SpawnAmmoDrop;
        player.OnLowHealth += SpawnMedicalDrop;
        RestartGame();
    }

    public void HandleGUI(double deltaTime)
    {
        UI.UpdateFPS(1 / deltaTime);
    }

    public void HandleDayTime(double deltaTime)
    {
        dayTime.Update(deltaTime);
    }

    public async void SendDataToServer()
    {
        if (!server.IsConnected())
            return;

        gameState.Location = player.PictureBox.Location;
        gameState.Direction = player.Direction;

        await server.SendGameStateAsync(gameState);
    }

    public void GetDataFromServer(GameState gameState)
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

    public void HandlePlayerInput()
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
            player.ShootBullet((bullet) =>
            {
                AddControl(bullet.PictureBox);
                bulletList.Add(bullet);
            }, 
            RemoveControl);
    }

    public void SpawnAmmoDrop()
    {
        IDroppableItem ammoDrop = dropSpawner.CreateDrop(Constants.DropAmmoTag);
        this.dropList.Add(ammoDrop);

        PictureBox ammoDropPictureBox = new AmmoDropBuilder()
            .SetTag(ammoDrop)
            .SetName(ammoDrop.Name)
            .SetImage(ammoDrop.Image)
            .SetLocation(ammoDrop.Location)
            .SetSize(ammoDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        ammoDrop.PictureBox = ammoDropPictureBox;

        AddControl(ammoDropPictureBox);
    }

    public void SpawnMedicalDrop()
    {
        IDroppableItem medicalDrop = dropSpawner.CreateDrop(Constants.DropMedicalTag);
        this.dropList.Add(medicalDrop);

        PictureBox medicalPictureBox = new MedicalDropBuilder()
            .SetTag(medicalDrop)
            .SetName(medicalDrop.Name)
            .SetImage(medicalDrop.Image)
            .SetLocation(medicalDrop.Location)
            .SetSize(medicalDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        medicalDrop.PictureBox = medicalPictureBox;

        AddControl(medicalPictureBox);
    }

    public void HandleEntityMovement(IEntity entity)
    {
        if (!entityContexts.TryGetValue(entity.PictureBox, out EntityContext? context))
        {
            context = new EntityContext();
            entityContexts[entity.PictureBox] = context;
        }

        uint fleeRadius = (entity is IEnemy) ? Constants.EnemyFleeRadius : Constants.AnimalFleeRadius;

        if (player.DistanceTo(entity.PictureBox) < fleeRadius)
        {
            if ((entity is IAnimal))
            {
                context.SetState(new FleeState());
            }
            else
            {
                context.SetState(new ChaseState());
            }
        }
        else
        {
            context.SetState(new IdleState());
        }

        context.UpdateState(entity.PictureBox, player);
    }

    public void SpawnEntities()
    {
        for (int i = 0; i < Constants.AnimalCount; i++)
            animalInteractionHandler.SpawnEntity();

        for (int i = 0; i < Constants.EnemyCount; i++)
            enemyInteractionHandler.SpawnEntity();
    }

    public void RestartGame()
    {
        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
            RemoveControl(control);

        AddControl(player.Respawn());
        SpawnEntities();
    }

    public static bool IsEnemyOrAnimal(Control control) => IsAnimal(control) || IsEnemy(control);
    public static bool IsEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.EnemyTag);
    public static bool IsAnimal(Control animal) => animal.Tag as string is Constants.AnimalTag;

    public bool IsFormFocused() => ActiveForm == this;

    public void AddControl(Control control)
    {
        this.Controls.Add(control);
        control.BringToFront();
        this.player.PictureBox.BringToFront();
    }

    public void RemoveControl(Control control)
    {
        this.Controls.Remove(control);
        control.Dispose();
    }
}
