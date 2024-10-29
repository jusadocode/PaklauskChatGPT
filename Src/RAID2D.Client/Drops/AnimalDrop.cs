using RAID2D.Client.Utils;

namespace RAID2D.Client.Drops;

public class AnimalDrop(Point location, string animalName) : IDroppableItem
{
    public PictureBox PictureBox { get; private set; } = new();
    public Point Location { get; private set; } = location;
    public string AnimalName { get; private set; } = animalName;

    PictureBox IDroppableItem.Create()
    {
        AnimalDropData data = DropManager.GetRandomAnimalDropDataByAnimalName(AnimalName);

        PictureBox = new()
        {
            Tag = Constants.DropAnimalTag,
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
