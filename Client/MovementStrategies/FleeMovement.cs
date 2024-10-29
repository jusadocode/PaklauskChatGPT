﻿using Client.Utils;

namespace Client.MovementStrategies;

/// <summary>
/// Flee movement strategy for running away from a certain character.
/// </summary>
/// <param name="characterToRunAwayFrom">The character to run away from</param>
/// <param name="speed">The speed at which the character will move</param>
public class FleeMovement(PictureBox characterToRunAwayFrom, int speed) : IMovementStrategy
{
    public int Speed { get; } = speed;

    void IMovementStrategy.Move(PictureBox character)
    {
        Size delta = (Size)character.Location - (Size)characterToRunAwayFrom.Location;
        double distance = Util.EuclideanDistance(character.Location, characterToRunAwayFrom.Location);

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