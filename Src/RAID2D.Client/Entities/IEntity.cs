namespace RAID2D.Client.Entities;

public interface IEntity
{
    public PictureBox PictureBox { get; }
    public PictureBox Create();
}
