using Client.Enums;
using Client.Utils;
using System.Numerics;

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

                character.Image = direction switch
                {
                    Direction.Up => Assets.PlayerUp,
                    Direction.Down => Assets.PlayerDown,
                    Direction.Left => Assets.PlayerLeft,
                    Direction.Right => Assets.PlayerRight,
                    _ => throw new NotImplementedException()
                };

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
            }
        }
    }
}
