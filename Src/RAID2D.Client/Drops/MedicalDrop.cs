using RAID2D.Client.Managers;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Drops;

public class MedicalDrop : IDroppableItem
{
    public Point Location { get; private set; }
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public Size Size => Constants.DropSize;

    public MedicalDrop()
    {
        Location = Rand.LocationOnScreen(Constants.DropSize);
        var data = DropManager.GetRandomMedicalDropData();
        Name = data.Name;
        Image = data.Image;
    }
}
