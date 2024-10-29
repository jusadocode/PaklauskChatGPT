using Microsoft.AspNetCore.SignalR.Client;
using RAID2D.Client;
using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.MovementStrategies;
using RAID2D.Client.UI;
using RAID2D.Client.Utils;
using System.Diagnostics;

namespace Client;

public partial class MainForm : Form
{
    private HubConnection server;

    private readonly Player player = new();

    private readonly UIManager UI = UIManager.GetInstance();

    private readonly Dictionary<PictureBox, IMovementStrategy> entityMovementStrategies = [];

    private readonly IDropSpawner dropSpawner = new DropSpawner();

    private IEntitySpawner entitySpawner = new DayEntitySpawner();

    public MainForm() { Initialize(); }

    void Initialize() // Main initialization method, that gets run when the form is created, before the game loop starts
    {
        InitializeComponent();

        InitializeSignalR();
        InitializeFullscreenWindow();
        InitializeDayTimeCycle();
        UI.InitializeLabels(FpsLabel, AmmoLabel, KillsLabel, CashLabel, HealthBar);
        UI.InitializeResolution(ClientSize);
        UI.CreateDevUI(player, SpawnEntities, SendDevMessage, AddControl);
        InitializePlayer();
        InitializeGameLoop();
        RestartGame();

        Console.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void FixedUpdate(double deltaTime) // Main game loop, that gets run every frame, deltaTime = time since last frame
    {
        UI.UpdateFPS(1 / deltaTime);

        HandlePlayerInput();

        if (player.IsDead())
            return;

        foreach (PictureBox box in this.Controls.OfType<PictureBox>().ToList())
        {
            HandleBulletCollision(box);

            HandleDropPickup(box);
            HandleEnemyInteraction(box);

            HandleEntityMovement(box);
        }
    }

    private async void InitializeSignalR()
    {
        server = new HubConnectionBuilder()
            .WithUrl("https://localhost:7260/chathub")
            .Build();

        server.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Invoke(() =>
            {
                Console.WriteLine($"Receivied message from server: \"{user}: {message}\"");
            });
        });

        await server.StartAsync();
    }

    private async void SendMessage(string user, string message)
    {
        await server.InvokeAsync("SendMessage", user, message);
    }

    private async void SendDevMessage()
    {
        await server.InvokeAsync("SendMessage", "DEV", "Sending a test message");
    }

    private void InitializeFullscreenWindow()
    {
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
    }

    private void InitializeDayTimeCycle()
    {
        this.BackColor = Constants.DayColor;
        uint currentHour = Constants.MiddleOfDay;

        Timer dayNightTimer = new()
        {
            Enabled = true,
            Interval = Constants.DayTimeUpdateInterval
        };
        dayNightTimer.Tick += (s, e) =>
        {
            currentHour = (currentHour + 1) % Constants.EndOfDay;

            uint lowerBound = Constants.MiddleOfDay - (Constants.MiddleOfDay / 2);
            uint upperBound = Constants.MiddleOfDay + (Constants.MiddleOfDay / 2);
            entitySpawner = (currentHour > lowerBound && currentHour <= upperBound) ? new DayEntitySpawner() : new NightEntitySpawner();

            float lerpFactor;
            if (currentHour <= Constants.MiddleOfDay)
                lerpFactor = (float)currentHour / Constants.MiddleOfDay;
            else
                lerpFactor = 1.0f - ((float)(currentHour - Constants.MiddleOfDay) / Constants.MiddleOfDay);

            int r = (int)(Constants.NightColor.R + ((Constants.DayColor.R - Constants.NightColor.R) * lerpFactor));
            int g = (int)(Constants.NightColor.G + ((Constants.DayColor.G - Constants.NightColor.G) * lerpFactor));
            int b = (int)(Constants.NightColor.B + ((Constants.DayColor.B - Constants.NightColor.B) * lerpFactor));
            this.BackColor = Color.FromArgb(0xFF, r, g, b);
        };
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
    }

    private void HandlePlayerInput()
    {
        if (player.IsDead())
        {
            if (KeyManager.IsKeyDown(Keys.Enter))
                RestartGame();

            return;
        }

        player.Move();

        if (KeyManager.IsKeyDownOnce(Keys.Space))
            player.ShootBullet(AddControl, RemoveControl);
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
        IDroppableItem ammo = dropSpawner.CreateDrop(Constants.DropAmmoTag);
        PictureBox ammoPictureBox = ammo.Create();
        AddControl(ammoPictureBox);
    }

    private void SpawnAnimalDrop(Point location, string animalName)
    {
        IDroppableItem animal = dropSpawner.CreateDrop(Constants.DropAnimalTag, location, animalName);
        PictureBox animalPictureBox = animal.Create();
        AddControl(animalPictureBox);
    }

    private void SpawnMedicalDrop()
    {
        IDroppableItem medical = dropSpawner.CreateDrop(Constants.DropMedicalTag);
        PictureBox medicalPictureBox = medical.Create();
        AddControl(medicalPictureBox);
    }

    private void SpawnValuableDrop(Point location)
    {
        IDroppableItem valuable = dropSpawner.CreateDrop(Constants.DropValuableTag, location);
        PictureBox valuablePictureBox = valuable.Create();
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
