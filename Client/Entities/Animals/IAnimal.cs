namespace Client.Entities.Animals;

public interface IAnimal
{
    public PictureBox? PictureBox { get; }

    public PictureBox Create();
}
