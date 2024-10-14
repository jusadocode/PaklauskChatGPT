using Timer = System.Windows.Forms.Timer;

namespace Client
{
    public partial class MainForm : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        Direction facing = Direction.Up;
        int playerHealth = 100;
        int value = 0;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        Random random = new Random();
        int score;
        private List<PictureBox> zombiesList = new List<PictureBox>();
        private Timer animalMovementTimer = new Timer();
        Random randomAnimals = new Random();

        private Dictionary<string, ValuableItem> valuableItems = new Dictionary<string, ValuableItem>
        {
            { "gold", new ValuableItem("gold", 100, 10, Properties.Resources.gold) },
            { "rolex", new ValuableItem("rolex", 60, 20, Properties.Resources.rolex) },
            { "parcel_box", new ValuableItem("parcel_box", 20, 35, Properties.Resources.parcel_box) },
            { "cigarettes", new ValuableItem("cigarettes", 20, 35, Properties.Resources.cigarettes) }
        };

        private Dictionary<string, AnimalDrop> animaldrops = new Dictionary<string, AnimalDrop>
        {
            {"pork", new AnimalDrop("pork", 100, 10, Properties.Resources.boarMeat)},
        };


        private Dictionary<string, MedicalItem> medicalItems = new Dictionary<string, MedicalItem>
        {
            { "small_medkit", new MedicalItem("small_medkit", 20, 90, Properties.Resources.small_medkit) },
            { "large_medkit", new MedicalItem("large_medkit", 50, 90, Properties.Resources.large_medkit) },
            { "health_potion", new MedicalItem("health_potion", 100, 90, Properties.Resources.large_medkit) }
        };

        public MainForm()
        {
            InitializeComponent();
            RestartGame();

            // Set the form's background to a color you want to be transparent
            //BackColor = Color.Lime; // Use a color not used in your images
            //TransparencyKey = Color.Lime; // This color will be treated as transparent
            //FormBorderStyle = FormBorderStyle.None; // Optional: Remove the border
            // Initialize the animal movement timer
            animalMovementTimer.Interval = 200; // Adjust this to control movement speed (500ms = 0.5 seconds)
            animalMovementTimer.Tick += MoveAnimals;
            animalMovementTimer.Start();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {

            UIManager.Instance.Initialize(txtAmmo, txtScore, valueLabel, healthBar);

            UIManager.Instance.UpdateUI(ammo, score, value);


            HandlePlayerMovement();

            foreach (Control control in Controls)
            {
                if (control is not PictureBox box)
                    continue;

                if (player.Bounds.IntersectsWith(box.Bounds))
                    HandleItemPickup(box);
                if (box.Tag as string == "zombie")
                    HandleZombieInteractions(box);
                if (box.Tag as string == "animal" || box.Tag as string == "zombie")
                    HandleBulletCollision(box);
            }
        }

        private void UpdatePlayerHealth()
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

        private void UpdateUI()
        {
            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills: " + score;
            valueLabel.Text = "Value: " + value + "$";
        }

        private void EndGame()
        {
            gameOver = true;
            player.Image = Properties.Resources.dead;
            GameTimer.Stop();
        }

        private void HandlePlayerMovement()
        {
            if (goLeft && player.Left > 0) player.Left -= speed;
            if (goRight && player.Left + player.Width < ClientSize.Width) player.Left += speed;
            if (goUp && player.Top > 45) player.Top -= speed;
            if (goDown && player.Top + player.Height < ClientSize.Height) player.Top += speed;
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
                value += item.Value;
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
                playerHealth = Math.Min(playerHealth + item.HealthSize, 100);
        }

        private void HealPlayer(AnimalDrop item)
        {
            if (playerHealth < 100)
                playerHealth = Math.Min(playerHealth + item.HealthSize, 100);
        }

        private void HandleZombieInteractions(PictureBox zombie)
        {
            // Damage player if zombie touches
            if (player.Bounds.IntersectsWith(zombie.Bounds))
            {

                playerHealth -= 1;
                UpdatePlayerHealth();

                if (playerHealth == 20)
                    SpawnRandomMedicalItem();
            }

            // Move zombie towards player
            if (zombie.Left > player.Left)
            {
                zombie.Left -= zombieSpeed;
                zombie.Image = Properties.Resources.zleft;
            }
            else if (zombie.Left < player.Left)
            {
                zombie.Left += zombieSpeed;
                zombie.Image = Properties.Resources.zright;
            }
            if (zombie.Top > player.Top)
            {
                zombie.Top -= zombieSpeed;
                zombie.Image = Properties.Resources.zup;
            }
            else if (zombie.Top < player.Top)
            {
                zombie.Top += zombieSpeed;
                zombie.Image = Properties.Resources.zdown;
            }
        }

        private void HandleBulletCollision(PictureBox zombieOrAnimal)
        {
            bool zombie = zombieOrAnimal.Tag as string == "zombie";
            bool animal = zombieOrAnimal.Tag as string == "animal";

            foreach (Control control in Controls)
            {
                if (control is not PictureBox bullet ||
                    bullet.Tag as string != "bullet" ||
                    !zombieOrAnimal.Bounds.IntersectsWith(bullet.Bounds))
                    continue;

                score++;

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
                    zombiesList.Remove(zombieOrAnimal);
                    MakeZombies();
                }
                if (animal)
                {
                    SpawnAnimals();
                }
            }
        }

        private void MoveAnimals(object? sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is PictureBox animal && (animal.Tag as string) == "animal")
                {
                    int moveDirection = random.Next(0, 4);

                    switch (moveDirection)
                    {
                        case 0:
                            if (animal.Top > 0)
                                animal.Top -= 15;
                            break;
                        case 1:
                            if (animal.Top < ClientSize.Height - animal.Height)
                                animal.Top += 15;
                            break;
                        case 2:
                            if (animal.Left > 0)
                                animal.Left -= 15;
                            break;
                        case 3:
                            if (animal.Left < ClientSize.Width - animal.Width)
                                animal.Left += 15;
                            break;
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    goLeft = true;
                    facing = Direction.Left;
                    player.Image = Properties.Resources.left;
                    break;
                case Keys.Right:
                    goRight = true;
                    facing = Direction.Right;
                    player.Image = Properties.Resources.right;
                    break;
                case Keys.Up:
                    goUp = true;
                    facing = Direction.Up;
                    player.Image = Properties.Resources.up;
                    break;
                case Keys.Down:
                    goDown = true;
                    facing = Direction.Down;
                    player.Image = Properties.Resources.down;
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
                    if (ammo < 1) DropAmmo();
                    break;
                case Keys.Enter when gameOver:
                    RestartGame();
                    break;
            }
        }

        private void ShootBullet(Direction direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.BulletDirection = direction;
            shootBullet.BulletLeft = player.Left + (player.Width / 2);
            shootBullet.BulletTop = player.Top + (player.Height / 2);
            shootBullet.MakeBullet(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = random.Next(0, 900);
            zombie.Top = random.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            Controls.Add(zombie);
            player.BringToFront();
        }

        private void DropAmmo()
        {
            PictureBox ammoDrop = new()
            {
                Name = "amoo",
                Tag = "ammo",
                Image = Properties.Resources.ammo,
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

            Random randomanimal = new Random();
            Image image;
            string name;
            int animalid = randomanimal.Next(1, 3);
            if (animalid == 1)
            {
                name = "boar";
                image = Properties.Resources.boardown;
            }
            else
            {
                name = "goat";
                image = Properties.Resources.goatdown;
            }
            PictureBox itemPictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "animal",
                Size = new Size(145, 145), // Increase size here
                Name = name,
            };
            //itemPictureBox.Parent = this;
            itemPictureBox.BackColor = Color.Transparent;
            // Position the item randomly on the screen
            itemPictureBox.Left = random.Next(10, ClientSize.Width - itemPictureBox.Width - 10);
            itemPictureBox.Top = random.Next(60, ClientSize.Height - itemPictureBox.Height - 10);

            // Add the item to the controls
            Controls.Add(itemPictureBox);
            itemPictureBox.BringToFront();
        }

        private void DropValuableItem(Point location)
        {
            // Calculate the total chance based on the values in the dictionary
            int totalChance = valuableItems.Values.Sum(item => item.SpawnChance); // Sum of all drop chances
            int randomValue = random.Next(0, totalChance); // Generate a random number between 0 and the total chance

            int cumulativeChance = 0;
            ValuableItem? selectedItem = null;

            // Loop through the dictionary to find the one to drop based on cumulative probability
            foreach (var itemPair in valuableItems)
            {
                ValuableItem item = itemPair.Value;
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
                PictureBox itemPictureBox = new PictureBox
                {
                    Image = selectedItem.Image,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "valuable",
                    Name = selectedItem.Name,
                    Size = new Size(50, 50)
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

        private void SpawnRandomMedicalItem()
        {
            // Randomly select a medical item from the dictionary
            var randomItemKey = medicalItems.Keys.ElementAt(random.Next(0, medicalItems.Count));
            MedicalItem selectedMedicalItem = medicalItems[randomItemKey];

            PictureBox itemPictureBox = new PictureBox
            {
                Image = selectedMedicalItem.Image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "medical",
                Name = selectedMedicalItem.Name,
                Size = new Size(50, 50)
            };

            // Position the item randomly on the screen
            itemPictureBox.Left = random.Next(10, ClientSize.Width - itemPictureBox.Width - 10);
            itemPictureBox.Top = random.Next(60, ClientSize.Height - itemPictureBox.Height - 10);

            // Add the item to the controls
            Controls.Add(itemPictureBox);
            itemPictureBox.BringToFront();
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
                PictureBox itemPictureBox = new PictureBox
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
            player.Image = Properties.Resources.up;

            // Remove zombies on restart
            foreach (PictureBox i in zombiesList)
            {
                Controls.Remove(i);
            }

            zombiesList.Clear();

            // Spawn initial zombies
            for (int i = 0; i < 3; i++)
            {
                MakeZombies();

            }
            for (int i = 0; i < randomAnimals.Next(1, 4); i++)
            {
                SpawnAnimals();
            }


            // Reset game stats
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            ammo = 10;
            value = 0;

            GameTimer.Start();
        }
    }
}
