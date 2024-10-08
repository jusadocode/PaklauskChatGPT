﻿using RAID2D.Resources;
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

        private Dictionary<string, ValuableItem> valuableItems = new Dictionary<string, ValuableItem>
        {
            { "gold", new ValuableItem("gold", 100, 10, Properties.Resources.gold) },
            { "rolex", new ValuableItem("rolex", 60, 20, Properties.Resources.rolex) },
            { "parcel_box", new ValuableItem("parcel_box", 20, 35, Properties.Resources.parcel_box) },
            { "cigarettes", new ValuableItem("cigarettes", 20, 35, Properties.Resources.cigarettes) }
        };


        private Dictionary<string, MedicalItem> medicalItems = new Dictionary<string, MedicalItem>
        {
            { "small_medkit", new MedicalItem("small_medkit", 20, 50, Properties.Resources.small_medkit) },
            { "large_medkit", new MedicalItem("large_medkit", 50, 30, Properties.Resources.large_medkit) },
            { "health_potion", new MedicalItem("health_potion", 100, 20, Properties.Resources.health_potion) }
        };


        public Form1()
        {
            InitializeComponent();
            RestartGame();

            if(instance == null)
                instance = this;
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

                // Player taking medical item
                if (x is PictureBox && (string)x.Tag == "medical" && player.Bounds.IntersectsWith(x.Bounds))
                {
                    if (medicalItems.TryGetValue(x.Name, out MedicalItem item))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();

                        if (item.name == "health_potion")
                            playerHealth = 100;
                        else
                            playerHealth += item.healthSize;
                    }
                }


                // Zombie movement and collision with player
                if (x is PictureBox && (string)x.Tag == "zombie")
                {

                    // Damage player if zombie touches
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;

                        if (playerHealth == 20)
                            SpawnRandomMedicalItem();
                    }

                    // Move zombie towards player
                    if (x.Left > player.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }

                }


                // Bullet collision with zombie
                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            // Random chance to drop valuable item (20% chance)
                            int dropChance = randNum.Next(0, 100); // Generates a number between 0 and 99
                            if (dropChance < 20) // 20% chance
                            {
                                DropValuableItem(x.Location); // Call function to spawn the item at the zombie's location
                            }

                            // Remove bullet and zombie
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombies();
                        }
                    }
                }




            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if (gameOver == true)
            {
                return;
            }

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
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            if (e.KeyCode == Keys.Space && ammo > 0 && gameOver == false)
            {
                ammo--;
                ShootBullet(facing);


                if (ammo < 1)
                {
                    DropAmmo();
                }
            }

            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }

        }

        private void valueLabel_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void healthBar_Click(object sender, EventArgs e)
        {

        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2);
            shootBullet.bulletTop = player.Top + (player.Height / 2);
            shootBullet.MakeBullet(this);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randNum.Next(0, 900);
            zombie.Top = randNum.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();

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

        private void DropValuableItem(Point location)
        {
            // Calculate the total chance based on the values in the dictionary
            int totalChance = valuableItems.Values.Sum(item => item.spawnChance); // Sum of all drop chances
            int randomValue = randNum.Next(0, totalChance); // Generate a random number between 0 and the total chance

            int cumulativeChance = 0;
            ValuableItem selectedItem = null;

            // Loop through the dictionary to find the one to drop based on cumulative probability
            foreach (var itemPair in valuableItems)
            {
                ValuableItem item = itemPair.Value;
                cumulativeChance += item.spawnChance;

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
                    Image = selectedItem.image,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = "valuable",
                    Size = new Size(50, 50),
                    Name = selectedItem.name // Using the Name property to identify the item
                };

                int offsetX = randNum.Next(-30, 30); // Offset between -30 to +30
                int offsetY = randNum.Next(-30, 30); // Offset between -30 to +30

                itemPictureBox.Left = Math.Max(10, Math.Min(location.X + offsetX, this.ClientSize.Width - itemPictureBox.Width - 10));
                itemPictureBox.Top = Math.Max(60, Math.Min(location.Y + offsetY, this.ClientSize.Height - itemPictureBox.Height - 10));

                this.Controls.Add(itemPictureBox);

                itemPictureBox.BringToFront();
                player.BringToFront();
            }
        }

        private void SpawnRandomMedicalItem()
        {
            // Randomly select a medical item from the dictionary
            var randomItemKey = medicalItems.Keys.ElementAt(randNum.Next(0, medicalItems.Count));
            MedicalItem selectedMedicalItem = medicalItems[randomItemKey];

            PictureBox itemPictureBox = new PictureBox
            {
                Image = selectedMedicalItem.image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "medical",
                Size = new Size(50, 50),
                Name = selectedMedicalItem.name
            };

            // Position the item randomly on the screen
            itemPictureBox.Left = randNum.Next(10, this.ClientSize.Width - itemPictureBox.Width - 10);
            itemPictureBox.Top = randNum.Next(60, this.ClientSize.Height - itemPictureBox.Height - 10);

            // Add the item to the controls
            this.Controls.Add(itemPictureBox);
            itemPictureBox.BringToFront();
        }




        private void RestartGame()
        {
            player.Image = Properties.Resources.up;

            // Remove zombies on restart
            foreach (PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);
            }

            zombiesList.Clear();

            // Spawn initial zombies
            for (int i = 0; i < 3; i++)
            {
                MakeZombies();
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
