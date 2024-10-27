using Client.Drops;
using Client.Entities.Spawners;
using Client.Movement;
using Client.UI;
using Client.Utils;

namespace Client;

public partial class MainForm : Form
{
    private readonly Player player = new();

    private readonly UIManager UI = UIManager.GetInstance();

    private readonly Dictionary<PictureBox, IMovementStrategy> animalMovementStrategies = [];
    private readonly Dictionary<PictureBox, IMovementStrategy> enemyMovementStrategies = [];

    private IEntitySpawner entitySpawner = new DayEntitySpawner();
    private uint currentHour = Constants.MiddleOfDay;

    public MainForm()
    {
        Initialize();
    }

    void Initialize()
    {
        InitializeComponent();
        ForceFullscreen();
        InitializeDayNightCycle();
        UI.Initialize(AmmoLabel, KillsLabel, CashLabel, HealthBar, ClientSize);

        player.OnDeath += EndGame;
        player.OnEmptyMagazine += DropAmmo;
        player.OnLowHealth += () => SpawnRandomMedicalItem(player.PictureBox?.Location ?? (Point)(UI.Resolution / 2));
        AddControl(player.Create());
        SpawnEntities(3);

        Console.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {
        HandleDayAndNightCycle();

        foreach (PictureBox box in this.Controls.OfType<PictureBox>().ToList())
        {
            if (player.IntersectsWith(box))
                HandleItemPickup(box);

            if (box.Tag as string is Constants.AnimalTag or Constants.EnemyTag)
                HandleBulletCollision(box);
            if (box.Tag as string is Constants.EnemyTag)
                HandleEnemy(box);
            if (box.Tag as string is Constants.AnimalTag)
                HandleAnimal(box);
        }
    }

    private void ForceFullscreen()
    {
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
    }

    private void InitializeDayNightCycle()
    {
        this.BackColor = Constants.DayColor;

        Timer dayNightTimer = new()
        {
            Interval = Constants.DayTimeUpdateInterval
        };
        dayNightTimer.Tick += UpdateDayNightCycle;
        dayNightTimer.Start();
    }

    private void UpdateDayNightCycle(object? sender, EventArgs e)
    {
        currentHour = (currentHour + 1) % Constants.EndOfDay;

        float lerpFactor;
        if (currentHour <= Constants.MiddleOfDay)
            lerpFactor = (float)currentHour / Constants.MiddleOfDay;
        else
            lerpFactor = 1.0f - ((float)(currentHour - Constants.MiddleOfDay) / Constants.MiddleOfDay);

        int r = (int)(Constants.NightColor.R + ((Constants.DayColor.R - Constants.NightColor.R) * lerpFactor));
        int g = (int)(Constants.NightColor.G + ((Constants.DayColor.G - Constants.NightColor.G) * lerpFactor));
        int b = (int)(Constants.NightColor.B + ((Constants.DayColor.B - Constants.NightColor.B) * lerpFactor));
        this.BackColor = Color.FromArgb(0xFF, r, g, b);
    }

    private void HandleDayAndNightCycle()
    {
        uint lowerBound = Constants.MiddleOfDay - (Constants.MiddleOfDay / 2);
        uint upperBound = Constants.MiddleOfDay + (Constants.MiddleOfDay / 2);

        entitySpawner = (currentHour > lowerBound && currentHour <= upperBound) ? new DayEntitySpawner() : new NightEntitySpawner();
    }

    private void EndGame()
    {
        GameTimer.Stop();
    }

    private void HandleItemPickup(PictureBox item)
    {
        switch (item.Tag as string)
        {
            case Constants.AmmoDropTag:
                PickupAmmo(item);
                break;
            case "valuable":
                PickupValuableItem(item);
                break;
            case "animaldrop":
                PickupAnimalDrop(item);
                break;
            case "medical":
                PickupMedicalItem(item);
                break;
        }
    }

    private void PickupAmmo(PictureBox ammo)
    {
        RemoveControl(ammo);
        player.PickupAmmo(5);
    }

    private void PickupValuableItem(PictureBox box)
    {
        if (Constants.ValuableDrops.TryGetValue(box.Name, out ValuableItem? item))
        {
            RemoveControl(box);
            player.PicupValuable(item.value);
        }
    }

    private void PickupMedicalItem(PictureBox box)
    {
        if (Constants.MedicalDrops.TryGetValue(box.Name, out MedicalItem? item))
        {
            RemoveControl(box);
            player.PickupHealable(item.HealingAmount);
        }
    }

    private void PickupAnimalDrop(PictureBox box)
    {
        if (Constants.AnimalDrops.TryGetValue(box.Name, out AnimalDrop? item))
        {
            RemoveControl(box);
            player.PickupHealable(item.HealingAmount);
        }
    }

    private void HandleAnimal(PictureBox animal)
    {
        double distance = player.DistanceTo(animal);

        if (animalMovementStrategies.TryGetValue(animal, out IMovementStrategy movementStrategy))
        {
            if (distance < Constants.AnimalFleeRadius)
            {
                if (movementStrategy is not FleeMovement)
                {
                    movementStrategy = new FleeMovement(player.PictureBox, Constants.ZombieSpeed);
                    animalMovementStrategies[animal] = movementStrategy; // Update the strategy in the dictionary
                }
            }
            else
            {
                if (movementStrategy is not WanderMovement)
                {
                    movementStrategy = new WanderMovement(Constants.ZombieSpeed / 2);
                    animalMovementStrategies[animal] = movementStrategy; // Update the strategy in the dictionary
                }
            }

            movementStrategy.Move(animal);
        }
    }

    private void HandleEnemy(PictureBox enemy)
    {
        if (player.IntersectsWith(enemy))
        {
            player.TakeDamage(Constants.EnemyDamage);

            if (player.Health < Constants.PlayerLowHealthLimit)
                SpawnRandomMedicalItem(enemy.Location);
        }

        double distance = player.DistanceTo(enemy);

        if (enemyMovementStrategies.TryGetValue(enemy, out IMovementStrategy movementStrategy))
        {
            if (distance < Constants.EnemyFleeRadius)
            {
                if (movementStrategy is not FollowPlayerMovement)
                {
                    movementStrategy = new FollowPlayerMovement(player.PictureBox, Constants.ZombieSpeed);
                    enemyMovementStrategies[enemy] = movementStrategy;
                }
            }
            else
            {
                if (movementStrategy is not WanderMovement)
                {
                    movementStrategy = new WanderMovement(Constants.ZombieSpeed);
                    enemyMovementStrategies[enemy] = movementStrategy;
                }
            }

            movementStrategy.Move(enemy);
        }
    }

    private void HandleBulletCollision(PictureBox enemyOrAnimal)
    {
        bool enemy = enemyOrAnimal.Tag as string is Constants.EnemyTag;
        bool animal = enemyOrAnimal.Tag as string is Constants.AnimalTag;

        foreach (PictureBox bullet in this.Controls.OfType<PictureBox>().ToList())
        {
            if (bullet.Tag as string is not Constants.BulletTag || !enemyOrAnimal.Bounds.IntersectsWith(bullet.Bounds))
                continue;

            player.GetKill();

            CreateHitmarker(bullet.Bounds.Location);

            int dropChance = Rand.Next(0, 100);
            if (animal && dropChance < 20)
                DropAnimal(enemyOrAnimal.Location);
            if (enemy && dropChance < 50)
                DropValuableItem(enemyOrAnimal.Location);

            RemoveControl(enemyOrAnimal);
            RemoveControl(bullet);

            if (enemy)
            {
                enemyMovementStrategies.Remove(enemyOrAnimal);
                SpawnEnemy();
            }

            if (animal)
            {
                animalMovementStrategies.Remove(enemyOrAnimal);
                SpawnAnimal();
            }
        }
    }

    private void KeyIsDown(object? sender, KeyEventArgs e)
    {
        player.Move(e.KeyCode);

        switch (e.KeyCode)
        {
            case Keys.Enter when player.IsDead():
                RestartGame();
                break;
            case Keys.Space when player.Ammo > 0 && !player.IsDead():
                AddControl(player.ShootBullet());
                break;
        }
    }

    private void SpawnEnemy()
    {
        var enemy = entitySpawner.CreateEnemy();
        var pictureBox = enemy.PictureBox ?? throw new InvalidOperationException("Enemy PictureBox should not be null.");

        enemyMovementStrategies[pictureBox] = new WanderMovement(Constants.ZombieSpeed);

        AddControl(pictureBox);
    }

    private void SpawnAnimal()
    {
        var animal = entitySpawner.CreateAnimal();
        var pictureBox = animal.PictureBox ?? throw new InvalidOperationException("Animal PictureBox should not be null.");

        animalMovementStrategies[pictureBox] = new WanderMovement(Constants.ZombieSpeed / 2);

        AddControl(pictureBox);
    }

    private void SpawnEntities(uint count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            SpawnAnimal();
        }
    }

    private void DropAmmo()
    {
        AddControl(new AmmoDrop().Create());
    }

    private void DropValuableItem(Point location)
    {
        // Calculate the total chance based on the values in the dictionary
        int totalChance = Constants.ValuableDrops.Values.Sum(item => item.dropChance); // Sum of all drop chances
        int randomValue = Rand.Next(0, totalChance); // Generate a random number between 0 and the total chance

        int cumulativeChance = 0;
        ValuableItem? selectedItem = null;

        // Loop through the dictionary to find the one to drop based on cumulative probability
        foreach (var itemPair in Constants.ValuableDrops)
        {
            ValuableItem item = itemPair.Value;
            cumulativeChance += item.dropChance;

            if (randomValue < cumulativeChance)
            {
                selectedItem = item;
                break;
            }
        }

        // If an item is selected, drop it at the given location
        if (selectedItem != null)
        {
            IGameObject valuable = GameObjectFactory.CreateGameObject("valuable", location);

            if (valuable is ValuableItem valuableItem)
            {
                AddControl(valuableItem.pictureBox);
            }
        }
    }

    private void SpawnRandomMedicalItem(Point location)
    {
        IGameObject medical = GameObjectFactory.CreateGameObject("medical", location);

        // Check if the created object is a ValuableItem
        if (medical is MedicalItem medicalItem)
        {
            AddControl(medicalItem.PictureBox);
        }
    }

    private void DropAnimal(Point location)
    {
        // Calculate the total chance based on the values in the dictionary
        int totalChance = Constants.AnimalDrops.Values.Sum(item => item.SpawnChance); // Sum of all drop chances
        int randomValue = Rand.Next(0, totalChance); // Generate a random number between 0 and the total chance

        int cumulativeChance = 0;
        AnimalDrop? selectedItem = null;

        // Loop through the dictionary to find the one to drop based on cumulative probability
        foreach (var itemPair in Constants.AnimalDrops)
        {
            AnimalDrop item = itemPair.Value;
            cumulativeChance += item.SpawnChance;

            if (randomValue < cumulativeChance)
            {
                selectedItem = item;
                break;
            }
        }

        // If an item is selected, drop it at the given location
        if (selectedItem != null)
        {
            PictureBox itemPictureBox = new()
            {
                Image = selectedItem.Image,
                SizeMode = Constants.SizeMode,
                Tag = "animaldrop",
                Name = selectedItem.Name,
                Size = Constants.DropSize,
            };

            int offsetX = Rand.Next(-30, 30); // Offset between -30 to +30
            int offsetY = Rand.Next(-30, 30); // Offset between -30 to +30

            itemPictureBox.Left = Math.Max(10, Math.Min(location.X + offsetX, ClientSize.Width - itemPictureBox.Width - 10));
            itemPictureBox.Top = Math.Max(60, Math.Min(location.Y + offsetY, ClientSize.Height - itemPictureBox.Height - 10));

            AddControl(itemPictureBox);
        }
    }

    private void RestartGame()
    {
        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
        {
            if (control.Tag as string is Constants.EnemyTag or Constants.AnimalTag or Constants.MedicalTag)
                RemoveControl(control);
        }

        player.Respawn();
        SpawnEntities(3);
        GameTimer.Start();
    }

    private void CreateHitmarker(Point location)
    {
        PictureBox hitmarker = new()
        {
            Tag = Constants.HitmarkerTag,
            Name = Constants.HitmarkerTag,
            Image = Assets.Hitmarker,
            Location = location,
            Size = Constants.HitmarkerSize,
            SizeMode = Constants.SizeMode,
        };

        AddControl(hitmarker);

        Timer timer = new()
        {
            Interval = 200
        };

        timer.Tick += (s, e) =>
        {
            RemoveControl(hitmarker);

            timer.Stop();
            timer.Dispose();
        };

        timer.Start();
    }

    private void AddControl(Control control)
    {
        this.Controls.Add(control);
        control.BringToFront();
        this.player.PictureBox?.BringToFront();
    }

    private void RemoveControl(Control control)
    {
        this.Controls.Remove(control);
        control.Dispose();
    }
}
