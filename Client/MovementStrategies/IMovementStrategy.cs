namespace Client.MovementStrategies;

public interface IMovementStrategy
{
    public int Speed { get; }

    void Move(PictureBox character);
}
