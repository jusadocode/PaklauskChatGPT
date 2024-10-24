using Client.Drops;
using Client.Enums;
using Client.Movement;
using Client.UI;

namespace Client;

public partial class MainForm : Form
{
    bool goLeft, goRight, goUp, goDown, gameOver;
    Direction facing = Direction.Up;
    int playerHealth = 100;
    int value = 0;
    readonly int speed = 10;
    int ammo = 10;
    readonly int zombieSpeed = 3;
    readonly Random random = new();
    int score;
    private readonly List<PictureBox> zombiesList = [];
    private readonly List<PictureBox> animalsList = [];

    private readonly Dictionary<string, ValuableItem> valuableItems = new()
    {
        { "gold", new ValuableItem("gold", 100, 10, Assets.gold) },
        { "rolex", new ValuableItem("rolex", 60, 20, Assets.rolex) },
        { "parcel_box", new ValuableItem("parcel_box", 20, 35, Assets.parcel_box) },
        { "cigarettes", new ValuableItem("cigarettes", 20, 35, Assets.cigarettes) }
    };

    private readonly Dictionary<string, AnimalDrop> animaldrops = new()
    {
        {"pork", new AnimalDrop("pork", 100, 10, Assets.boarMeat)},
    };

    private readonly Dictionary<string, MedicalItem> medicalItems = new()
    {
        { "small_medkit", new MedicalItem("small_medkit", 20, 90, Assets.small_medkit) },
        { "large_medkit", new MedicalItem("large_medkit", 50, 90, Assets.large_medkit) },
        { "health_potion", new MedicalItem("health_potion", 100, 90, Assets.large_medkit) }
    };

    // Create a dictionary to store each animal's movement strategy
    private readonly Dictionary<PictureBox, IMovementStrategy> animalMovementStrategies = [];

    private readonly Dictionary<PictureBox, IMovementStrategy> zombieMovementStrategies = [];

    public MainForm()
    {
        InitializeComponent();
        RestartGame();

        // Set the form's background to a color you want to be transparent
        //BackColor = Color.Lime; // Use a color not used in your images
        //TransparencyKey = Color.Lime; // This color will be treated as transparent
        //FormBorderStyle = FormBorderStyle.None; // Optional: Remove the border
        // Initialize the animal movement timer
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {

        UIManager.Instance.Initialize(txtAmmo, txtScore, valueLabel, healthBar);

        UIManager.Instance.UpdateUI(ammo, score, value);

        CheckPlayerHealth();

        foreach (Control control in Controls)
        {
            if (control is not PictureBox box)
                continue;

            if (player.Bounds.IntersectsWith(box.Bounds))
                HandleItemPickup(box);
            if ((box.Tag as string) == "zombie")
                HandleZombieInteractions(box);
            if (box.Tag as string is "animal" or "zombie")
                HandleBulletCollision(box);
        }

        HandlePlayerMovement();

        MoveAnimals();

        MoveZombies();
    }

    private void CheckPlayerHealth()
    {

        if (playerHealth > 1)
        {
            UIManager.Instance.UpdateHealth(playerHealth);
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameOver = true;
        player.Image = Assets.dead;
        GameTimer.Stop();
    }

    private void HandlePlayerMovement()
    {
        if (goLeft && player.Left > 0)
            player.Left -= speed;
        if (goRight && player.Left + player.Width < ClientSize.Width)
            player.Left += speed;
        if (goUp && player.Top > 45)
            player.Top -= speed;
        if (goDown && player.Top + player.Height < ClientSize.Height)
            player.Top += speed;
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
        if (player.Bounds.IntersectsWith(zombie.Bounds))
        {
            playerHealth -= 1;

            if (playerHealth == 20)
                SpawnRandomMedicalItem(zombie.Location);

        }
    }

    private void HandleBulletCollision(PictureBox zombieOrAnimal)
    {
        bool zombie = (zombieOrAnimal.Tag as string) == "zombie";
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
            int dropChance = random.Next(0, 100);
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
                _ = zombiesList.Remove(zombieOrAnimal);
                _ = animalMovementStrategies.Remove(zombieOrAnimal);
                MakeZombies();
            }

            if (animal)
            {
                _ = animalMovementStrategies.Remove(zombieOrAnimal);
                SpawnAnimals();
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
                player.Image = Assets.left;
                break;
            case Keys.Right:
                goRight = true;
                facing = Direction.Right;
                player.Image = Assets.right;
                break;
            case Keys.Up:
                goUp = true;
                facing = Direction.Up;
                player.Image = Assets.up;
                break;
            case Keys.Down:
                goDown = true;
                facing = Direction.Down;
                player.Image = Assets.down;
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
            BulletLeft = player.Left + (player.Width / 2),
            BulletTop = player.Top + (player.Height / 2)
        };
        shootBullet.MakeBullet(this);
    }

    private void MakeZombies()
    {
        PictureBox zombie = new()
        {
            Tag = "zombie",
            Image = Assets.zdown,
            Left = random.Next(0, 900),
            Top = random.Next(0, 800),
            SizeMode = PictureBoxSizeMode.AutoSize
        };
        zombiesList.Add(zombie);
        Controls.Add(zombie);

        zombieMovementStrategies[zombie] = new WanderMovement(this, zombieSpeed);

        player.BringToFront();
    }

    private void DropAmmo()
    {
        PictureBox ammoDrop = new()
        {
            Name = "amoo",
            Tag = "ammo",
            Image = Assets.ammo,
            SizeMode = PictureBoxSizeMode.AutoSize,
            Left = random.Next(0, ClientSize.Width),
            Top = random.Next(0, ClientSize.Height)
        };
        Controls.Add(ammoDrop);

        ammoDrop.BringToFront();
        player.BringToFront();
    }
    private void SpawnAnimals()
    {

        Random randomanimal = new();
        Image image;
        string name;
        int animalid = randomanimal.Next(1, 3);
        if (animalid == 1)
        {
            name = "boar";
            image = Assets.boardown;
        }
        else
        {
            name = "goat";
            image = Assets.goatdown;
        }

        PictureBox itemPictureBox = new()
        {
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Tag = "animal",
            Size = new Size(145, 145), // Increase size here
            Name = name,
            //itemPictureBox.Parent = this;
            BackColor = Color.Transparent
        };
        // Position the item randomly on the screen
        itemPictureBox.Left = random.Next(10, ClientSize.Width - itemPictureBox.Width - 10);
        itemPictureBox.Top = random.Next(60, ClientSize.Height - itemPictureBox.Height - 10);

        // Add the item to the controls
        Controls.Add(itemPictureBox);
        itemPictureBox.BringToFront();

        animalMovementStrategies[itemPictureBox] = new WanderMovement(this, zombieSpeed / 2);
    }

    private void MoveAnimals()
    {
        foreach (Control control in Controls)
        {
            if (control is PictureBox animal && (animal.Tag as string) == "animal")
            {
                // Calculate distance to player
                double distanceToPlayer = GetDistance(animal, player);
                int fleeRadius = 300; // Adjust this value as needed

                if (animalMovementStrategies.TryGetValue(animal, out IMovementStrategy movementStrategy))
                {
                    if (distanceToPlayer < fleeRadius)
                    {
                        if (movementStrategy is not FleeMovement)
                        {
                            movementStrategy = new FleeMovement(player, zombieSpeed);
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
            if (control is PictureBox zombie && (zombie.Tag as string) == "zombie")
            {
                // Calculate distance to player
                double distanceToPlayer = GetDistance(zombie, player);
                int fleeRadius = 500; // Adjust this value as needed

                if (zombieMovementStrategies.TryGetValue(zombie, out IMovementStrategy movementStrategy))
                {
                    if (distanceToPlayer < fleeRadius)
                    {
                        if (movementStrategy is not FollowPlayerMovement)
                        {
                            movementStrategy = new FollowPlayerMovement(player, this, zombieSpeed);
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
        int randomValue = random.Next(0, totalChance); // Generate a random number between 0 and the total chance

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
        int randomValue = random.Next(0, totalChance); // Generate a random number between 0 and the total chance

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

            int offsetX = random.Next(-30, 30); // Offset between -30 to +30
            int offsetY = random.Next(-30, 30); // Offset between -30 to +30

            itemPictureBox.Left = Math.Max(10, Math.Min(location.X + offsetX, ClientSize.Width - itemPictureBox.Width - 10));
            itemPictureBox.Top = Math.Max(60, Math.Min(location.Y + offsetY, ClientSize.Height - itemPictureBox.Height - 10));

            Controls.Add(itemPictureBox);

            itemPictureBox.BringToFront();
            player.BringToFront();
        }
    }

    private void RestartGame()
    {
        player.Image = Assets.up;

        foreach (PictureBox zombie in zombiesList)
        {
            zombie.Dispose();
        }

        zombiesList.Clear();

        foreach (PictureBox animal in animalsList)
        {
            animal.Dispose();
        }

        animalsList.Clear();

        foreach (Control control in this.Controls.OfType<PictureBox>().ToList())
        {
            if (control.Tag is (object?)"zombie" or (object?)"animal" or (object?)"medical")
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
        }

        for (int i = 0; i < 3; i++)
        {
            MakeZombies();
            SpawnAnimals();
        }

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
            Image = Assets.hitmarker, // Assign the converted Image here
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
