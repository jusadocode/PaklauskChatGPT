using Client.Utils;

namespace Client.Drops;

public class MedicalItem(string name, uint healingValue, Image image) : IGameObject
{
    public uint HealingAmount { get; private set; } = healingValue;

    public Image Image { get; private set; } = image;
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IGameObject.Create(Point location)
    {
        PictureBox PictureBox = new()
        {
            Image = this.Image,
            SizeMode = Constants.SizeMode,
            Tag = "medical",
            Size = Constants.DropSize,
            Name = name,
            Left = location.X + Rand.Next(100, 500),
            Top = location.Y + Rand.Next(250, 782)
        };

        return PictureBox;
    }
}
