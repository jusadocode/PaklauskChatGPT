using RAID2D.Client.Managers;

namespace RAID2D.Client.Drops;

public class ValuableDrop(Point location) : IDroppableItem
{
    public PictureBox PictureBox { get; private set; } = new();
    public Point Location { get; private set; } = location;

    PictureBox IDroppableItem.Create()
    {
        ValuableDropData data = DropManager.GetRandomValuableDropData();

        PictureBox = new()
        {
            Tag = Constants.DropValuableTag,
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