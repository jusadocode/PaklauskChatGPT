using Client.Utils;

namespace Client.Drops;

public class AmmoDrop : IDroppableItem
{
    public PictureBox PictureBox { get; private set; } = new();
    public Point Location { get; private set; } = Rand.LocationOnScreen(Constants.DropSize);

    PictureBox IDroppableItem.Create()
    {
        AmmoDropData data = DropManager.GetRandomAmmoDropData();

        PictureBox = new()
        {
            Tag = Constants.DropAmmoTag,
            Name = data.Name,
            Image = data.Image,
            Location = this.Location,
            Size = Constants.DropSize,
            SizeMode = Constants.SizeMode,
        };

        Console.WriteLine($"Spawned {data.Name} at {PictureBox.Location}");

        return PictureBox;
    }
}
