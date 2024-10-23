namespace Client
{
    public class ValuableItem : IGameObject
    {
        public string name;
        public int value;
        public int dropChance;
        public Image itemImage;
        public PictureBox pictureBox;

        // Constructor
        public ValuableItem(string name, int value, int dropchance, Image image)
        {
            this.name = name;
            this.value = value;
            this.dropChance = dropchance;
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