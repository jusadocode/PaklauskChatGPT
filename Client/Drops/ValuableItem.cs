namespace Client.Drops;

public class ValuableItem(string name, int value, int dropchance, Image image) : IGameObject
{
    public string name = name;
    public int value = value;
    public int dropChance = dropchance;
    public Image itemImage = image;
    public PictureBox pictureBox;

    // Method to create and return the PictureBox
    public PictureBox CreatePictureBox(Point location)
    {

        PictureBox itemPictureBox = new()
        {
            Image = itemImage,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Tag = "valuable",
            Size = new Size(50, 50),
            Name = name,
            Location = location,
            Left = location.X,
            Top = location.Y
        };
        itemPictureBox.BringToFront();
        pictureBox = itemPictureBox;
        return itemPictureBox;
    }
}