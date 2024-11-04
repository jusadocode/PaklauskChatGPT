using RAID2D.Client.Managers;

namespace RAID2D.Client.Drops;

public class ValuableDrop : IDroppableItem
{
    public Point Location { get; private set; }
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public Size Size => Constants.DropSize;

    public ValuableDrop(Point location)
    {
        Location = location;
        var data = DropManager.GetRandomValuableDropData();
        Name = data.Name;
        Image = data.Image;
    }
}
