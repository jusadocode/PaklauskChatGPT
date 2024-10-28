namespace Client.Drops;

public interface IDroppableItem
{
    public Point Location { get; }
    public PictureBox PictureBox { get; }
    public PictureBox Create();
}
