using RAID2D.Client;
using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Managers;
using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Services;
using RAID2D.Client.UI;
using System.Diagnostics;
using RAID2D.Client.Drops.Builders;

namespace Client;

public partial class MainForm : Form
{
    private readonly ServerConnection server = new();

    private readonly Player player = new();

    private readonly GUI UI = GUI.GetInstance();

    private readonly Dictionary<PictureBox, IMovementStrategy> entityMovementStrategies = [];

    private readonly IDropSpawner dropSpawner = new DropSpawner();

    private IEntitySpawner entitySpawner = new DayEntitySpawner();

    public MainForm() { Initialize(); }

    void Initialize() // Main initialization method, that gets run when the form is created, before the game loop starts
    {
        InitializeComponent();

        InitializeGUI();
        InitializeGameLoop();
        InitializePlayer();

        Console.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void FixedUpdate(double deltaTime) // Main game loop, that gets run every frame, deltaTime = time since last frame
    {
        HandlePlayerInput();

        if (player.IsDead())
            return;

        UI.UpdateFPS(1 / deltaTime);

        DayTimeManager.Update(deltaTime);
        entitySpawner = DayTimeManager.IsDay() ? new DayEntitySpawner() : new NightEntitySpawner();

        foreach (PictureBox box in this.Controls.OfType<PictureBox>().ToList())
        {
            HandleBulletCollision(box);

            HandleDropPickup(box);
            HandleEnemyInteraction(box);

            HandleEntityMovement(box);
        }
    }

    private void InitializeGUI()
    {
        // Force fullscreen on startup
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);

        UI.BindElements(FpsLabel, AmmoLabel, KillsLabel, CashLabel, HealthBar);
        UI.SetResolution(ClientSize);
        UI.CreatePauseMenu(onConnectClick: server.InitializeConnection, onDisconnectClick: async () => await server.DisconnectAsync(), onQuitClick: Application.Exit, onPanelCreate: AddControl);
        UI.CreateDevButtons(player, server, SpawnEntities, AddControl);

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

    private void HandlePlayerInput()
    {
        if (player.IsDead())
        {
            if (InputManager.IsKeyDown(Keys.Enter))
                RestartGame();

            return;
        }

        player.Move();

        if (InputManager.IsKeyDownOnce(Keys.Space))
            player.ShootBullet(AddControl, RemoveControl);

        if (InputManager.IsKeyDownOnce(Keys.Escape))
            UI.TogglePauseMenuVisibility();
    }

    private void HandleEnemyInteraction(PictureBox enemy)
    {
        if (!player.IntersectsWith(enemy) || !IsEnemy(enemy))
            return;

        player.TakeDamage(Constants.EnemyDamage);
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

        PictureBox ammoDropPictureBox = new DropItemBuilder()
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

        PictureBox animalPictureBox = new DropItemBuilder()
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

        PictureBox medicalPictureBox = new DropItemBuilder()
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

        PictureBox valuablePictureBox = new DropItemBuilder()
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
        var enemy = entitySpawner.CreateEnemy();
        var pictureBox = enemy.PictureBox;

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

    private void SpawnAnimals(uint count)
    {
        for (int i = 0; i < count; i++)
            SpawnAnimal();
    }

    private void SpawnEnemies(uint count)
    {
        for (int i = 0; i < count; i++)
            SpawnEnemy();
    }

    private void SpawnEntities(uint count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnAnimal();
            SpawnEnemy();
        }
    }

    private void RestartGame()
    {
        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
            RemoveControl(control);

        AddControl(player.Respawn());
        SpawnAnimals(Constants.AnimalCount);
        SpawnEnemies(Constants.EnemyCount);
    }

    private static bool IsDrop(Control drop) => drop.Tag as string is Constants.DropAmmoTag or Constants.DropAnimalTag or Constants.DropMedicalTag or Constants.DropValuableTag;
    private static bool IsEnemyOrAnimal(Control control) => control.Tag as string is Constants.EnemyTag or Constants.AnimalTag;
    private static bool IsEnemy(Control enemy) => enemy.Tag as string is Constants.EnemyTag;
    private static bool IsAnimal(Control animal) => animal.Tag as string is Constants.AnimalTag;
    private static bool IsBullet(Control bullet) => bullet.Tag as string is Constants.BulletTag;

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
