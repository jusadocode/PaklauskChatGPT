﻿using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Decorators;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Managers;
using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;
using RAID2D.Client.Services;
using RAID2D.Client.UI;
using RAID2D.Client.Utils;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Diagnostics;

namespace RAID2D.Client;

public partial class MainForm : Form
{
    private readonly ServerConnection server = new();
    private readonly GameState gameState = new(new Point(0, 0), Direction.Right);
    public Dictionary<string, ServerPlayer> serverPlayers = [];

    private readonly Player player = new();

    private readonly GUI UI = GUI.GetInstance();

    private readonly Dictionary<PictureBox, IMovementStrategy> entityMovementStrategies = [];

    private readonly Dictionary<PictureBox, int> shieldedEnemyHealth = new();

    private readonly IDropSpawner dropSpawner = new DropSpawner();

    private IEntitySpawner entitySpawner = new DayEntitySpawner();

    public MainForm() { Initialize(); }

    void Initialize() // Main initialization method, that gets run when the form is created, before the game loop starts
    {
        InitializeComponent();

        InitializeServer();
        InitializeGUI();
        InitializeDevTools();
        InitializeGameLoop();
        InitializePlayer();

        Console.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void FixedUpdate(double deltaTime) // Main game loop, that gets run every frame, deltaTime = time since last frame
    {
        SendDataToServer();

        if (!IsFormFocused())
            return;

        HandlePlayerInput();

        if (player.IsDead() || UI.IsPaused())
            return;

        HandleGUI(deltaTime);

        foreach (PictureBox box in this.Controls.OfType<PictureBox>().ToList())
        {
            HandleBulletCollision(box);

            HandleDropPickup(box);
            HandleEnemyInteraction(box);
            HandleMutatedEnemyInteraction(box);

            HandleEntityMovement(box);
        }
    }

    private void InitializeDevTools()
    {
#if DEBUG
        UI.CreateDevButtons(player, server, SpawnEntities, AddControl);
        server.Connect(Constants.ServerDefaultUrl);
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
        this.FormBorderStyle = FormBorderStyle.None;
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
            onPanelCreate: AddControl);

        // Initialize day/night cycle
        DayTimeManager.Initialize(this);
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

        DayTimeManager.Update(deltaTime);
        entitySpawner = DayTimeManager.IsDay() ? new DayEntitySpawner() : new NightEntitySpawner();
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

    private void HandleEnemyInteraction(PictureBox enemy)
    {
        if (!player.IntersectsWith(enemy) || !IsEnemy(enemy))
            return;

        player.TakeDamage(Constants.EnemyDamage);
    }

    private void HandleMutatedEnemyInteraction(PictureBox enemy)
    {
        if (!player.IntersectsWith(enemy) || (!IsPulsingEnemy(enemy) && !IsShieldedEnemy(enemy)))
            return;

        if (IsPulsingEnemy(enemy))
        {
            player.TakeDamage(Constants.PulsingEnemyDamage);

            RemoveControl(enemy);
            entityMovementStrategies.Remove(enemy);
        }

        if (IsShieldedEnemy(enemy))
        {
            player.TakeDamage(Constants.EnemyDamage);
        }
    }

    private void HandleDropPickup(PictureBox drop)
    {
        if (!player.IntersectsWith(drop) || !IsDrop(drop))
            return;

        switch (drop.Tag as string)
        {
            case Constants.DropAmmoTag:
                PickupAmmoDrop(drop);
                break;
            case Constants.DropAnimalTag:
                PickupAnimalDrop(drop);
                break;
            case Constants.DropMedicalTag:
                PickupMedicalDrop(drop);
                break;
            case Constants.DropValuableTag:
                PickupValuableDrop(drop);
                break;
        }
    }

    private void PickupAmmoDrop(PictureBox ammoDropPicture)
    {
        AmmoDropData ammoDrop = DropManager.GetAmmoDropData(ammoDropPicture.Name);

        player.PickupAmmo(ammoDrop.AmmoAmount);
        RemoveControl(ammoDropPicture);
    }

    private void PickupAnimalDrop(PictureBox animalDropPicture)
    {
        AnimalDropData animalDrop = DropManager.GetAnimalDropData(animalDropPicture.Name);

        player.PickupHealable(animalDrop.HealthAmount);
        RemoveControl(animalDropPicture);
    }

    private void PickupMedicalDrop(PictureBox medicalDropPicture)
    {
        MedicalDropData medicalDrop = DropManager.GetMedicalDropData(medicalDropPicture.Name);

        player.PickupHealable(medicalDrop.HealthAmount);
        RemoveControl(medicalDropPicture);
    }

    private void PickupValuableDrop(PictureBox valuableDropPicture)
    {
        ValuableDropData valuableDrop = DropManager.GetValuableDropData(valuableDropPicture.Name);

        player.PickupValuable(valuableDrop.CashAmount);
        RemoveControl(valuableDropPicture);
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

    private void SpawnAnimalDrop(Point location, string animalName)
    {
        IDroppableItem animalDrop = dropSpawner.CreateDrop(Constants.DropAnimalTag, location, animalName);

        PictureBox animalPictureBox = new AnimalDropBuilder()
            .SetTag(animalDrop)
            .SetName(animalDrop.Name)
            .SetImage(animalDrop.Image)
            .SetLocation(animalDrop.Location)
            .SetSize(animalDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        AddControl(animalPictureBox);
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

    private void SpawnValuableDrop(Point location)
    {
        IDroppableItem valuableDrop = dropSpawner.CreateDrop(Constants.DropValuableTag, location);

        PictureBox valuablePictureBox = new ValuableDropBuilder()
            .SetTag(valuableDrop)
            .SetName(valuableDrop.Name)
            .SetImage(valuableDrop.Image)
            .SetLocation(valuableDrop.Location)
            .SetSize(valuableDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        AddControl(valuablePictureBox);
    }

    private void HandleEntityMovement(PictureBox entity)
    {
        if (!IsEnemyOrAnimal(entity))
            return;

        entityMovementStrategies.TryGetValue(entity, out IMovementStrategy? movementStrategy);
        if (movementStrategy is null)
            return;

        uint fleeRadius = IsEnemy(entity) ? Constants.EnemyFleeRadius : Constants.AnimalFleeRadius;
        int speed = IsEnemy(entity) ? Constants.EnemySpeed : Constants.AnimalSpeed;

        if (player.DistanceTo(entity) < fleeRadius)
        {
            if (IsAnimal(entity) && movementStrategy is not FleeMovement)
                entityMovementStrategies[entity] = new FleeMovement(player.PictureBox, speed);
            else if (IsEnemy(entity) && movementStrategy is not ChaseMovement)
                entityMovementStrategies[entity] = new ChaseMovement(player.PictureBox, speed);
        }
        else
        {
            if (movementStrategy is not WanderMovement)
                entityMovementStrategies[entity] = new WanderMovement(speed / 2); ;
        }

        movementStrategy.Move(entity);
    }

    private void HandleBulletCollision(PictureBox entity)
    {
        if (!IsEnemyOrAnimal(entity))
            return;

        foreach (PictureBox bullet in this.Controls.OfType<PictureBox>().ToList())
        {
            if (!IsBullet(bullet) || !entity.Bounds.IntersectsWith(bullet.Bounds))
                continue;

            if (IsShieldedEnemy(entity))
            {
                ManageShieldedEnemyHealth(entity);
                RemoveControl(bullet);
                return;
            }

            player.RegisterKill(bullet.Bounds.Location, AddControl, RemoveControl);

            if (IsEnemy(entity))
            {
                SpawnValuableDrop(entity.Location);
                SpawnEnemy();
            }
            else if (IsAnimal(entity))
            {
                SpawnAnimalDrop(entity.Location, entity.Name);
                SpawnAnimal();
            }

            RemoveControl(entity);
            RemoveControl(bullet);
            entityMovementStrategies.Remove(entity);
        }
    }

    private void SpawnEnemy()
    {
        IEnemy enemy = entitySpawner.CreateEnemy();

        if (Rand.Next(0, 101) < Constants.MutatedEnemySpawnChance)
        {
            switch (Rand.Next(0, 3))
            {
                case 0:
                    enemy = new ShieldedEnemyDecorator(enemy);
                    break;
                case 1:
                    enemy = new PulsingEnemyDecorator(enemy);
                    break;
                case 2:
                    enemy = new ShieldedEnemyDecorator(new PulsingEnemyDecorator(enemy));
                    break;
                default:
                    break;
            }
        }

        PictureBox pictureBox = enemy.PictureBox;

        if (IsShieldedEnemy(pictureBox))
            shieldedEnemyHealth[pictureBox] = Constants.ShieldedEnemyMaxHealth;

        entityMovementStrategies[pictureBox] = new WanderMovement(Constants.EnemySpeed / 2);

        AddControl(pictureBox);
    }

    private void SpawnAnimal()
    {
        var animal = entitySpawner.CreateAnimal();
        var pictureBox = animal.PictureBox;

        entityMovementStrategies[pictureBox] = new WanderMovement(Constants.AnimalSpeed / 2);

        AddControl(pictureBox);
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < Constants.AnimalCount; i++)
            SpawnAnimal();

        for (int i = 0; i < Constants.EnemyCount; i++)
            SpawnEnemy();
    }

    private void RestartGame()
    {
        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
            RemoveControl(control);

        AddControl(player.Respawn());
        SpawnEntities();
    }

    private static bool IsDrop(Control drop) => drop.Tag as string is Constants.DropAmmoTag or Constants.DropAnimalTag or Constants.DropMedicalTag or Constants.DropValuableTag;
    private static bool IsEnemyOrAnimal(Control control) => IsAnimal(control) || IsEnemy(control);
    private static bool IsEnemy(Control enemy) => enemy.Tag is string tag && (tag == Constants.EnemyTag || IsPulsingEnemy(enemy) || IsShieldedEnemy(enemy));
    private static bool IsPulsingEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.PulsingEnemyTag);
    private static bool IsShieldedEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.ShieldedEnemyTag);
    private static bool IsAnimal(Control animal) => animal.Tag as string is Constants.AnimalTag;
    private static bool IsBullet(Control bullet) => bullet.Tag as string is Constants.BulletTag;

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

    private void ManageShieldedEnemyHealth(PictureBox enemy)
    {
        if (shieldedEnemyHealth.TryGetValue(enemy, out int currentHealth))
        {
            currentHealth -= 10;

            shieldedEnemyHealth[enemy] = currentHealth;

            if (currentHealth <= 0)
            {
                RemoveControl(enemy);
                SpawnValuableDrop(enemy.Location);
                SpawnEnemy();
                shieldedEnemyHealth.Remove(enemy);
            }
        }
    }
}
