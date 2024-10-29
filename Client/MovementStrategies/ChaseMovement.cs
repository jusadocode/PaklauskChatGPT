using Client.Utils;

namespace Client.MovementStrategies;

/// <summary>
/// Chase movement strategy to chase a certain character
/// </summary>
/// <param name="characterToChase"></param>
/// <param name="speed">The speed at which the character will move</param>"
public class ChaseMovement(PictureBox characterToChase, int speed) : IMovementStrategy
{
    public int Speed { get; } = speed;

    void IMovementStrategy.Move(PictureBox character)
    {
        Size delta = (Size)characterToChase.Location - (Size)character.Location;
        double distance = Util.EuclideanDistance(character.Location, characterToChase.Location);

        if (distance > 0)
        {
            double directionX = delta.Width / distance;
            double directionY = delta.Height / distance;

            Point newLocation = new(
                (int)(character.Location.X + (directionX * this.Speed)),
                (int)(character.Location.Y + (directionY * this.Speed))
            );

            newLocation = Util.ClampToBounds(newLocation, character.Size);

            character.Location = newLocation;

            character.Image = Util.GetImageFromDirection(character.Name, directionX, directionY);
        }
    }
}
