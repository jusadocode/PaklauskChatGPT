using RAID2D.Client.Managers;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Drops;

public class MedicalDrop : IDroppableItem
{
    public PictureBox PictureBox { get; private set; } = new();
    public Point Location { get; private set; } = Rand.LocationOnScreen(Constants.DropSize);

    PictureBox IDroppableItem.Create()
    {
        MedicalDropData data = DropManager.GetRandomMedicalDropData();

        PictureBox = new()
        {
            Tag = Constants.DropMedicalTag,
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
