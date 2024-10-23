using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public class MedicalItem : IGameObject
    {
        public string name;
        public int healingValue;
        public int dropChance;
        public Image itemImage;
        public PictureBox pictureBox;

        public MedicalItem(string Name, int HealingValue, int Dropchance, Image Image)
        {
            this.name = Name;
            this.healingValue = HealingValue;
            this.dropChance = Dropchance;
            this.itemImage = Image;


        }

        public PictureBox CreatePictureBox(Point location)
        {
            Random random = new Random();

            PictureBox itemPictureBox = new PictureBox
            {
                Image = itemImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "medical",
                Size = new Size(50, 50),
                Name = name,
            };


            itemPictureBox.Left = location.X + random.Next(100, 500);
            itemPictureBox.Top = location.Y + random.Next(250, 782);
            itemPictureBox.BringToFront();
            pictureBox = itemPictureBox;
            return itemPictureBox;
        }
    }


}