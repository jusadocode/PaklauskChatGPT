using Client.Enums;
using Client.Utils;

namespace Client.MovementStrategies;

public class PlayerMovement(Player player, int speed) : IMovementStrategy
{
    public int Speed { get; } = speed;

    void IMovementStrategy.Move(PictureBox character)
    {
        foreach (var (direction, keys) in KeyManager.MovementKeysMap)
        {
            if (keys.Any(KeyManager.IsKeyDown))
            {
                player.Direction = direction;

                Size displacement = direction switch
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

                character.Image = Util.GetImageFromDirection(character.Name, displacement.Width, displacement.Height);
            }
        }
    }
}
