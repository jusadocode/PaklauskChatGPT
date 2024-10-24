using Client.Drops;
using Client.Entities.Spawners;
using Client.Enums;
using Client.Movement;
using Client.UI;
using Client.Utils;

namespace Client;

public partial class MainForm : Form
{
    private readonly UIManager UI = UIManager.GetInstance();
    bool goLeft, goRight, goUp, goDown, gameOver;
    Direction facing = Direction.Up;
    int playerHealth = 100;
    int value = 0;
    readonly int speed = 10;
    int ammo = 10;
    readonly int zombieSpeed = 3;
    int score = 0;

    private readonly Dictionary<string, ValuableItem> valuableItems = new()
    {
        { "gold", new ValuableItem("gold", 100, 10, Assets.DropGold) },
        { "rolex", new ValuableItem("rolex", 60, 20, Assets.DropRolex) },
        { "parcel_box", new ValuableItem("parcel_box", 20, 35, Assets.DropParcelBox) },
        { "cigarettes", new ValuableItem("cigarettes", 20, 35, Assets.DropCigarettes) }
    };

    private readonly Dictionary<string, AnimalDrop> animaldrops = new()
    {
        {"pork", new AnimalDrop("pork", 100, 10, Assets.DropMeat)},
    };

    private readonly Dictionary<string, MedicalItem> medicalItems = new()
    {
        { "small_medkit", new MedicalItem("small_medkit", 20, 90, Assets.DropMedkitSmall) },
        { "large_medkit", new MedicalItem("large_medkit", 50, 90, Assets.DropMedkitLarge) },
        { "health_potion", new MedicalItem("health_potion", 100, 90, Assets.DropMedkitLarge) }
    };

    // Create a dictionary to store each animal's movement strategy
    private readonly Dictionary<PictureBox, IMovementStrategy> animalMovementStrategies = [];

    private readonly Dictionary<PictureBox, IMovementStrategy> zombieMovementStrategies = [];

    private IEntitySpawner entitySpawner = new DayEntitySpawner();

    private readonly Timer dayNightTimer = new();
    private int currentHour;
    private const int hoursInDay = 24;
    private const int updateInterval = 1000; // 1 second irl = 1 hour in game

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
        SpawnEntities(3);

        Console.WriteLine($"Game initialized. Current resolution: {ClientSize.Width}x{ClientSize.Height}");
    }

    private void ForceFullscreen()
    {
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Bounds = Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
    }

    private void InitializeDayNightCycle()
    {
        currentHour = hoursInDay / 2;
        this.BackColor = Color.FromArgb(255, 143, 188, 143);
        dayNightTimer.Interval = updateInterval;
        dayNightTimer.Tick += UpdateDayNightCycle;
        dayNightTimer.Start();
    }

    private void UpdateDayNightCycle(object? sender, EventArgs e)
    {
        currentHour = (currentHour + 1) % hoursInDay;

        Color dayColor = Color.FromArgb(0xFF, 0x8F, 0xBC, 0x8F); // Light green
        Color nightColor = Color.FromArgb(0xFF, 0x2F, 0x4F, 0x2F); // Darker green

        float middleHourOfTheDay = hoursInDay / 2.0f;

        float lerpFactor;
        if (currentHour <= middleHourOfTheDay)
            lerpFactor = (float)currentHour / middleHourOfTheDay;  
        else
            lerpFactor = 1.0f - ((float)(currentHour - middleHourOfTheDay) / middleHourOfTheDay);
        
        int r = (int)(nightColor.R + (dayColor.R - nightColor.R) * lerpFactor);
        int g = (int)(nightColor.G + (dayColor.G - nightColor.G) * lerpFactor);
        int b = (int)(nightColor.B + (dayColor.B - nightColor.B) * lerpFactor);

        this.BackColor = Color.FromArgb(255, r, g, b);
        Console.WriteLine($"Current hour: {currentHour}, Lerp factor: {lerpFactor}, Color: {this.BackColor}");
    }


    private void MainTimerEvent(object sender, EventArgs e)
    {
        UI.UpdateUI(ammo, score, value);

        CheckPlayerHealth();

        HandleDayCycle();

        foreach (Control control in Controls)
        {
            if (control is not PictureBox box)
                continue;

            if (Player.Bounds.IntersectsWith(box.Bounds))
                HandleItemPickup(box);
            if (box.Tag as string is "enemy")
                HandleZombieInteractions(box);
            if (box.Tag as string is "animal" or "enemy")
                HandleBulletCollision(box);
        }

        HandlePlayerMovement();

        MoveAnimals();

        MoveZombies();
    }

    private void HandleDayCycle()
    {
        entitySpawner = (currentHour is > 6 and <= 18) ? new DayEntitySpawner() : new NightEntitySpawner();
    }

    private void CheckPlayerHealth()
    {
        if (playerHealth > 1)
            UI.UpdateHealth(playerHealth);
        else
            EndGame();
    }

    private void EndGame()
    {
        gameOver = true;
        Player.Image = Assets.PlayerDead;
        GameTimer.Stop();
    }

    private void HandlePlayerMovement()
    {
        if (goLeft && Player.Left > 0)
            Player.Left -= speed;
        if (goRight && Player.Left + Player.Width < ClientSize.Width)
            Player.Left += speed;
        if (goUp && Player.Top > 45)
            Player.Top -= speed;
        if (goDown && Player.Top + Player.Height < ClientSize.Height)
            Player.Top += speed;
    }

    private void HandleItemPickup(PictureBox item)
    {
        switch (item.Tag as string)
        {
            case "ammo":
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

    private void PickupAmmo(PictureBox box)
    {
        Controls.Remove(box);
        box.Dispose();
        ammo += 5;
    }

    private void PickupValuableItem(PictureBox box)
    {
        if (valuableItems.TryGetValue(box.Name, out ValuableItem? item))
        {
            Controls.Remove(box);
            box.Dispose();
            value += item.value;
        }
    }

    private void PickupMedicalItem(PictureBox box)
    {
        if (medicalItems.TryGetValue(box.Name, out MedicalItem? item))
        {
            Controls.Remove(box);
            box.Dispose();
            HealPlayer(item);
        }
    }

    private void PickupAnimalDrop(PictureBox box)
    {
        if (animaldrops.TryGetValue(box.Name, out AnimalDrop? item))
        {
            Controls.Remove(box);
            box.Dispose();
            HealPlayer(item);
        }
    }

    private void HealPlayer(MedicalItem item)
    {
        if (playerHealth < 100)
            playerHealth = Math.Min(playerHealth + item.healingValue, 100);
    }

    private void HealPlayer(AnimalDrop item)
    {
        if (playerHealth < 100)
            playerHealth = Math.Min(playerHealth + item.HealthSize, 100);
    }

    private void HandleZombieInteractions(PictureBox zombie)
    {
        if (Player.Bounds.IntersectsWith(zombie.Bounds))
        {
            playerHealth -= 1;

            if (playerHealth == 20)
                SpawnRandomMedicalItem(zombie.Location);
        }
    }

    private void HandleBulletCollision(PictureBox zombieOrAnimal)
    {
        bool zombie = (zombieOrAnimal.Tag as string) == "enemy";
        bool animal = (zombieOrAnimal.Tag as string) == "animal";

        foreach (Control control in Controls)
        {
            if (control is not PictureBox bullet ||
                (bullet.Tag as string) != "bullet" ||
                !zombieOrAnimal.Bounds.IntersectsWith(bullet.Bounds))
            {
                continue;
            }

            score++;

            CreateHitmarker(bullet.Bounds.Location);

            // Random chance to drop valuable item (20% chance)
            int dropChance = Rand.Next(0, 100);
            if (animal && dropChance < 20)
                DropAnimal(zombieOrAnimal.Location);
            if (zombie && dropChance < 50)
                DropValuableItem(zombieOrAnimal.Location);

            // Remove bullet and zombie/animal
            Controls.Remove(zombieOrAnimal);
            zombieOrAnimal.Dispose();
            Controls.Remove(bullet);
            bullet.Dispose();

            if (zombie)
            {
                animalMovementStrategies.Remove(zombieOrAnimal);
                SpawnEnemy();
            }

            if (animal)
            {
                animalMovementStrategies.Remove(zombieOrAnimal);
                SpawnAnimal();
            }
        }
    }

    // Helper method to calculate the Euclidean distance between two PictureBox controls
    private static double GetDistance(Control a, Control b)
    {
        int deltaX = a.Left + (a.Width / 2) - (b.Left + (b.Width / 2));
        int deltaY = a.Top + (a.Height / 2) - (b.Top + (b.Height / 2));
        return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (gameOver)
            return;

        switch (e.KeyCode)
        {
            case Keys.Left:
                goLeft = true;
                facing = Direction.Left;
                Player.Image = Assets.PlayerLeft;
                break;
            case Keys.Right:
                goRight = true;
                facing = Direction.Right;
                Player.Image = Assets.PlayerRight;
                break;
            case Keys.Up:
                goUp = true;
                facing = Direction.Up;
                Player.Image = Assets.PlayerUp;
                break;
            case Keys.Down:
                goDown = true;
                facing = Direction.Down;
                Player.Image = Assets.PlayerDown;
                break;
        }
    }

    private void KeyIsUp(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                goLeft = false;
                break;
            case Keys.Right:
                goRight = false;
                break;
            case Keys.Up:
                goUp = false;
                break;
            case Keys.Down:
                goDown = false;
                break;
            case Keys.Space when ammo > 0 && !gameOver:
                ammo--;
                ShootBullet(facing);
                if (ammo < 1)
                    DropAmmo();
                break;
            case Keys.Enter when gameOver:
                RestartGame();
                break;
        }
    }

    private void ShootBullet(Direction direction)
    {
        Bullet shootBullet = new()
        {
            BulletDirection = direction,
            BulletLeft = Player.Left + (Player.Width / 2),
            BulletTop = Player.Top + (Player.Height / 2)
        };
        shootBullet.MakeBullet(this);
    }

    private void SpawnEnemy()
    {
        var enemy = entitySpawner.CreateEnemy();
        var pictureBox = enemy.PictureBox ?? throw new InvalidOperationException("Enemy PictureBox should not be null.");

        zombieMovementStrategies[pictureBox] = new WanderMovement(this, zombieSpeed);
        Controls.Add(pictureBox);
        Player.BringToFront();
    }

    private void SpawnAnimal()
    {
        var animal = entitySpawner.CreateAnimal();
        var pictureBox = animal.PictureBox ?? throw new InvalidOperationException("Animal PictureBox should not be null.");

        animalMovementStrategies[pictureBox] = new WanderMovement(this, zombieSpeed / 2);
        Controls.Add(pictureBox);
        Player.BringToFront();
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
        PictureBox ammoDrop = new()
        {
            Name = "amoo",
            Tag = "ammo",
            Image = Assets.DropAmmo,
            SizeMode = PictureBoxSizeMode.AutoSize,
            Left = Rand.Next(0, ClientSize.Width),
            Top = Rand.Next(0, ClientSize.Height)
        };
        Controls.Add(ammoDrop);

        ammoDrop.BringToFront();
        Player.BringToFront();
    }

    private void MoveAnimals()
    {
        foreach (Control control in Controls)
        {
            if (control is PictureBox animal && (animal.Tag as string) == "animal")
            {
                // Calculate distance to player
                double distanceToPlayer = GetDistance(animal, Player);
                int fleeRadius = 300; // Adjust this value as needed

                if (animalMovementStrategies.TryGetValue(animal, out IMovementStrategy movementStrategy))
                {
                    if (distanceToPlayer < fleeRadius)
                    {
                        if (movementStrategy is not FleeMovement)
                        {
                            movementStrategy = new FleeMovement(Player, zombieSpeed);
                            animalMovementStrategies[animal] = movementStrategy; // Update the strategy in the dictionary
                        }
                    }
                    else
                    {
                        if (movementStrategy is not WanderMovement)
                        {
                            movementStrategy = new WanderMovement(this, zombieSpeed / 2);
                            animalMovementStrategies[animal] = movementStrategy; // Update the strategy in the dictionary
                        }
                    }

                    movementStrategy.Move(animal);
                }
            }
        }
    }

    private void MoveZombies()
    {
        foreach (Control control in Controls)
        {
            if (control is PictureBox zombie && (zombie.Tag as string) == "enemy")
            {
                // Calculate distance to player
                double distanceToPlayer = GetDistance(zombie, Player);
                int fleeRadius = 500; // Adjust this value as needed

                if (zombieMovementStrategies.TryGetValue(zombie, out IMovementStrategy movementStrategy))
                {
                    if (distanceToPlayer < fleeRadius)
                    {
                        if (movementStrategy is not FollowPlayerMovement)
                        {
                            movementStrategy = new FollowPlayerMovement(Player, this, zombieSpeed);
                            zombieMovementStrategies[zombie] = movementStrategy;
                        }
                    }
                    else
                    {
                        if (movementStrategy is not WanderMovement)
                        {
                            movementStrategy = new WanderMovement(this, zombieSpeed);
                            zombieMovementStrategies[zombie] = movementStrategy;
                        }
                    }

                    movementStrategy.Move(zombie);
                }
            }
        }
    }

    private void DropValuableItem(Point location)
    {
        // Calculate the total chance based on the values in the dictionary
        int totalChance = valuableItems.Values.Sum(item => item.dropChance); // Sum of all drop chances
        int randomValue = Rand.Next(0, totalChance); // Generate a random number between 0 and the total chance

        int cumulativeChance = 0;
        ValuableItem? selectedItem = null;

        // Loop through the dictionary to find the one to drop based on cumulative probability
        foreach (var itemPair in valuableItems)
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
                this.Controls.Add(valuableItem.pictureBox);
            }
        }
    }

    private void SpawnRandomMedicalItem(Point location)
    {
        IGameObject medical = GameObjectFactory.CreateGameObject("medical", location);

        // Check if the created object is a ValuableItem
        if (medical is MedicalItem medicalItem)
        {
            this.Controls.Add(medicalItem.pictureBox);
        }
    }

    private void DropAnimal(Point location)
    {
        // Calculate the total chance based on the values in the dictionary
        int totalChance = animaldrops.Values.Sum(item => item.SpawnChance); // Sum of all drop chances
        int randomValue = Rand.Next(0, totalChance); // Generate a random number between 0 and the total chance

        int cumulativeChance = 0;
        AnimalDrop? selectedItem = null;

        // Loop through the dictionary to find the one to drop based on cumulative probability
        foreach (var itemPair in animaldrops)
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
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "animaldrop",
                Name = selectedItem.Name,
                Size = new Size(50, 50),
            };

            int offsetX = Rand.Next(-30, 30); // Offset between -30 to +30
            int offsetY = Rand.Next(-30, 30); // Offset between -30 to +30

            itemPictureBox.Left = Math.Max(10, Math.Min(location.X + offsetX, ClientSize.Width - itemPictureBox.Width - 10));
            itemPictureBox.Top = Math.Max(60, Math.Min(location.Y + offsetY, ClientSize.Height - itemPictureBox.Height - 10));

            Controls.Add(itemPictureBox);

            itemPictureBox.BringToFront();
            Player.BringToFront();
        }
    }

    private void RestartGame()
    {
        Player.Image = Assets.PlayerUp;

        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
        {
            if (control.Tag is (object?)"enemy" or (object?)"animal" or (object?)"medical")
            {
                Controls.Remove(control);
                control.Dispose();
            }
        }

        SpawnEntities(3);

        goUp = goDown = goLeft = goRight = false;
        gameOver = false;
        playerHealth = 100;
        score = 0;
        value = 0;
        ammo = 10;

        if (!GameTimer.Enabled)
        {
            GameTimer.Start();
        }
    }

    private void CreateHitmarker(Point location)
    {
        PictureBox hitmarker = new()
        {
            Image = Assets.Hitmarker, // Assign the converted Image here
            SizeMode = PictureBoxSizeMode.StretchImage,
            Tag = "hitmarker",
            Name = "Hitmarker",
            Size = new Size(20, 20),
            Left = location.X,
            Top = location.Y
        };

        Controls.Add(hitmarker);
        hitmarker.BringToFront();

        Timer timer = new()
        {
            Interval = 200 // Set interval to 1 second (1000 milliseconds)
        };

        timer.Tick += (s, e) =>
        {
            Controls.Remove(hitmarker);
            hitmarker.Dispose();

            timer.Stop();
            timer.Dispose();
        };

        timer.Start();
    }
}
