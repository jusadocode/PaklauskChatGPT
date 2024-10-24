namespace Client.Drops;

public class MedicalItem(string Name, int HealingValue, int Dropchance, Image Image) : IGameObject
{
    public string name = Name;
    public int healingValue = HealingValue;
    public int dropChance = Dropchance;
    public Image itemImage = Image;
    public PictureBox pictureBox;

    public PictureBox CreatePictureBox(Point location)
    {
        Random random = new();

        PictureBox itemPictureBox = new()
        {
            Image = itemImage,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Tag = "medical",
            Size = new Size(50, 50),
            Name = name,
            Left = location.X + random.Next(100, 500),
            Top = location.Y + random.Next(250, 782)
        };
        itemPictureBox.BringToFront();
        pictureBox = itemPictureBox;
        return itemPictureBox;
    }
}