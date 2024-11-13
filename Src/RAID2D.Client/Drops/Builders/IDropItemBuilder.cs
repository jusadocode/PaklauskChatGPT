namespace RAID2D.Client.Drops.Builders;

public interface IDropItemBuilder
{
    IDropItemBuilder SetTag(IDroppableItem tag);
    IDropItemBuilder SetName(string name);
    IDropItemBuilder SetImage(Image image);
    IDropItemBuilder SetLocation(Point location);
    IDropItemBuilder SetSize(Size size);
    IDropItemBuilder SetSizeMode(PictureBoxSizeMode sizeMode);
    PictureBox Build();
}
