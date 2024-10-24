namespace Client.Entities.Enemies;

public interface IEnemy
{
    public PictureBox? PictureBox { get; }
    public PictureBox Create();
}

