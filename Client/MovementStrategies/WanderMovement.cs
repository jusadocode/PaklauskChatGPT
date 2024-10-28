using Client.Enums;
using Client.UI;
using Client.Utils;

namespace Client.MovementStrategies;

/// <summary>
/// Wander movement strategy for moving randomly around the screen
/// </summary>
/// <param name="speed">The speed at which the character will move</param>"
public class WanderMovement(int speed) : IMovementStrategy
{
    private int directionTime;
    private int currentTime;
    private Direction currentDirection;

    public int Speed { get; } = speed;

    void IMovementStrategy.Move(PictureBox character)
    {
        currentTime++;

        if (currentTime >= directionTime)
            ChangeDirection();

        Size displacement = currentDirection switch
        {
            Direction.Up => new Size(0, -this.Speed),
            Direction.Down => new Size(0, this.Speed),
            Direction.Left => new Size(-this.Speed, 0),
            Direction.Right => new Size(this.Speed, 0),
            _ => throw new NotImplementedException()
        };

        Point newLocation = character.Location + displacement;
        newLocation = Util.ClampToBounds(newLocation, character.Size);

        character.Location = newLocation;
    }

    private void ChangeDirection()
    {
        currentDirection = (Direction)Rand.Next(0, 4);
        currentTime = 0;
        directionTime = Rand.Next(20, 50);
    }
}
