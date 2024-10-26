namespace Client.Drops;

public class MedicalItem(string Name, uint HealingValue, int Dropchance, Image Image) : IGameObject
{
    public string name = Name;
    public uint healingValue = HealingValue;
    public int dropChance = Dropchance;
    public Image itemImage = Image;
    public PictureBox pictureBox;

    PictureBox IGameObject.Create(Point location)
    {
        Random random = new();

        PictureBox itemPictureBox = new()
        {
            Image = itemImage,
            SizeMode = Constants.SizeMode,
            Tag = "medical",
            Size = Constants.DropSize,
            Name = name,
            Left = location.X + random.Next(100, 500),
            Top = location.Y + random.Next(250, 782)
        };
        itemPictureBox.BringToFront();
        pictureBox = itemPictureBox;
        return itemPictureBox;
    }
}
