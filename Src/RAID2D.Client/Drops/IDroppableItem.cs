namespace RAID2D.Client.Drops;

public interface IDroppableItem
{
    Point Location { get; }
    string Name { get; }
    Image Image { get; }
    Size Size { get; }
}
