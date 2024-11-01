using RAID2D.Client.Managers;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Drops;

public class AmmoDrop : IDroppableItem
{
    public Point Location { get; private set; }
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public Size Size => Constants.DropSize;

    public AmmoDrop()
    {
        Location = Rand.LocationOnScreen(Constants.DropSize);
        var data = DropManager.GetRandomAmmoDropData();
        Name = data.Name;
        Image = data.Image;
    }
}
