namespace Client.Drops;

public class ValuableItem(string name, uint value, int dropchance, Image image) : IGameObject
{
    public string name = name;
    public uint value = value;
    public int dropChance = dropchance;
    public Image itemImage = image;
    public PictureBox pictureBox;

    // Method to create and return the PictureBox
    public PictureBox Create(Point location)
    {

        PictureBox itemPictureBox = new()
        {
            Image = itemImage,
            SizeMode = Constants.SizeMode,
            Tag = "valuable",
            Size = Constants.DropSize,
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