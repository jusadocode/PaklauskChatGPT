using System;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D
{
    public class ValuableItem : IGameObject
    {
        private string name;
        public int value;
        private int dropchance;
        private Image itemImage;
        public PictureBox pictureBox;

        // Constructor
        public ValuableItem(string name, int value, int dropchance, Image image)
        {
            this.name = name;
            this.value = value;
            this.dropchance = dropchance;
            this.itemImage = image;

           
        }

        // Method to create and return the PictureBox
        public PictureBox CreatePictureBox(Point location)
        {
           
            PictureBox itemPictureBox = new PictureBox
            {
                Image = itemImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "valuable",
                Size = new Size(50, 50),
                Name = name, 
            };

            itemPictureBox.Location = location;
            itemPictureBox.Left = location.X;
            itemPictureBox.Top = location.Y;
            itemPictureBox.BringToFront();
            pictureBox = itemPictureBox;
            return itemPictureBox;
        }
    }
}
