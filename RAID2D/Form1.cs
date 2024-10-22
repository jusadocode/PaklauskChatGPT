using RAID2D.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAID2D
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int value = 0;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        Random randNum = new Random();
        int score;
        private List<PictureBox> zombiesList = new List<PictureBox>();
        private List<PictureBox> animalsList = new List<PictureBox>();
        private List<IMovementStrategy> animalMovement = new List<IMovementStrategy>();
        Random randomAnimals = new Random();

        private static Dictionary<string, ValuableItem> valuableItems = new Dictionary<string, ValuableItem>
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

        private IMovementStrategy playerMovementStrategy;
        private IMovementStrategy zombieMovementStrategy;
        private IMovementStrategy animalMovementStrategy;

        public Form1()
        {
            InitializeComponent();
            RestartGame();

            if (instance == null)
                instance = this;

            playerMovementStrategy = new ControlledMovement(goLeft, goRight, goUp, goDown);
            zombieMovementStrategy = new FollowPlayerMovement(player, this);
            animalMovementStrategy = new WanderMovement(this);

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                GameTimer.Stop();
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills: " + score;
            valueLabel.Text = "Value: " + value + "$";

            // Player movement logic
            if (goLeft && player.Left > 0) player.Left -= speed;
            if (goRight && player.Left + player.Width < this.ClientSize.Width) player.Left += speed;
            if (goUp && player.Top > 45) player.Top -= speed;
            if (goDown && player.Top + player.Height < this.ClientSize.Height) player.Top += speed;

            // Collision and item pickup logic
            foreach (Control x in this.Controls)
            {
                // Ammo pickup
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                // Player picking up valuable item
                if (x is PictureBox && (string)x.Tag == "valuable" && player.Bounds.IntersectsWith(x.Bounds))
                {
                    if (valuableItems.TryGetValue(x.Name, out ValuableItem item))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        value += item.value; // Add value score
                    }
                }

                // Player taking medical item (animal drop)
                if (x is PictureBox && (string)x.Tag == "animaldrop" && player.Bounds.IntersectsWith(x.Bounds))
                {
                    if (animaldrops.TryGetValue(x.Name, out AnimalDrop item))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        if (playerHealth + item.healthSize > 100)
                            playerHealth = 100;
                        else
                            playerHealth += item.healthSize;
                    }
                }

                // Player taking medical item
                if (x is PictureBox && (string)x.Tag == "medical" && player.Bounds.IntersectsWith(x.Bounds))
                {
                    if (medicalItems.TryGetValue(x.Name, out MedicalItem item))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        if (playerHealth + item.healingValue > 100)
                        {
                            playerHealth = 100;
                        }
                        else
                        {
                            playerHealth += item.healingValue;
                        }
                    }
                }

                // Zombie movement and collision with player
                if (x is PictureBox && (string)x.Tag == "zombie")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;

                        if (playerHealth == 20)
                        {
                            IGameObject medical = GameObjectFactory.CreateGameObject("medical", x.Location);

                            if (medical is MedicalItem medicalItem)
                            {
                                this.Controls.Add(medicalItem.pictureBox);
                            }
                        }
                    }

                    // Move zombie towards player
                    zombieMovementStrategy.Move((PictureBox)x, zombieSpeed);
                }

                // Bullet collision with zombies and animals
                HandleBulletCollisions(x);
                MoveAnimals();
            }
        }

        private void HandleBulletCollisions(Control x)
        {
            foreach (Control j in this.Controls)
            {
                if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                {
                    if (x.Bounds.IntersectsWith(j.Bounds))
                    {
                        score++;
                        DropRandomValuable(x.Location);

                        // Remove bullet and zombie
                        RemoveZombieAndBullet(x, j);
                    }
                }

                if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "animal")
                {
                    if (x.Bounds.IntersectsWith(j.Bounds))
                    {
                        score++;
                        SpawnAnimals();

                        // Remove bullet and animal
                        RemoveAnimalAndBullet(x, j);
                    }
                }
            }
        }

        private void RemoveZombieAndBullet(Control zombie, Control bullet)
        {
            this.Controls.Remove(bullet);
            ((PictureBox)bullet).Dispose();
            this.Controls.Remove(zombie);
            ((PictureBox)zombie).Dispose();
            zombiesList.Remove((PictureBox)zombie);
            MakeZombies();
        }

        private void RemoveAnimalAndBullet(Control animal, Control bullet)
        {
            this.Controls.Remove(bullet);
            ((PictureBox)bullet).Dispose();
            this.Controls.Remove(animal);
            ((PictureBox)animal).Dispose();
        }

        private void DropRandomValuable(Point location)
        {
            int dropChance = randNum.Next(0, 100); // 60% chance
            if (dropChance < 60)
            {
                IGameObject valuable = GameObjectFactory.CreateGameObject("valuable", location);

                if (valuable is ValuableItem valuableItem)
                {
                    this.Controls.Add(valuableItem.pictureBox);
                }
            }
        }

        private void MoveAnimals()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "animal")
                {
                    animalMovementStrategy.Move((PictureBox)control,zombieSpeed);
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Up) goUp = false;
            if (e.KeyCode == Keys.Down) goDown = false;

            if (e.KeyCode == Keys.Space && ammo > 0 && !gameOver)
            {
                ammo--;
                ShootBullet(facing);
                if (ammo < 1)
                {
                    DropAmmo();
                }
            }

            if (e.KeyCode == Keys.Enter && gameOver)
            {
                RestartGame();
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2);
            shootBullet.bulletTop = player.Top + (player.Height / 2);
            shootBullet.MakeBullet(this);
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();
        }

        private void RestartGame()
        {
            player.Image = Properties.Resources.up;

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
                if (control.Tag == "zombie" || control.Tag == "animal" || control.Tag == "medical")
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


        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox
            {
                Tag = "zombie",
                Image = Properties.Resources.zdown,
                Left = randNum.Next(0, 800),
                Top = randNum.Next(0, 900),
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
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
            itemPictureBox.Parent = instance;
            itemPictureBox.BackColor = Color.Transparent;
            // Position the item randomly on the screen
            itemPictureBox.Left = randNum.Next(10, this.ClientSize.Width - itemPictureBox.Width - 10);
            itemPictureBox.Top = randNum.Next(60, this.ClientSize.Height - itemPictureBox.Height - 10);

            // Add the item to the controls
            this.Controls.Add(itemPictureBox);
            itemPictureBox.BringToFront();
        }
    }
}
