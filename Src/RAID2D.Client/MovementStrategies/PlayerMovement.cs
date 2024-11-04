using RAID2D.Client.Managers;
using RAID2D.Client.Players;
using RAID2D.Client.Utils;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.MovementStrategies;

public class PlayerMovement(Player player, int speed) : IMovementStrategy
{
    public int Speed { get; } = speed;

    void IMovementStrategy.Move(PictureBox character)
    {
        foreach (var (direction, keys) in InputManager.MovementKeysMap)
        {
            if (keys.Any(InputManager.IsKeyDown))
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
                newLocation = Location.ClampToBounds(newLocation, character.Size);

                character.Location = newLocation;

                character.Image = ImageManager.GetImageFromDirection(character.Name, displacement.Width, displacement.Height);
            }
        }
    }
}
