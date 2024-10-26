using Client.Enums;
using Client.UI;
using Client.Utils;

namespace Client.Movement;

public class WanderMovement(int speed) : IMovementStrategy
{
    private int directionTime = Rand.Next(200, 1000);
    private int currentTime;
    private Direction currentDirection = (Direction)Rand.Next(0, 4);

    public void Move(PictureBox animal)
    {
        UIManager UI = UIManager.GetInstance();
        currentTime++;

        if (currentTime >= directionTime)
            ChangeDirection();

        switch (currentDirection)
        {
            case Direction.Up:
                if (animal.Top > 0)
                    animal.Top -= speed;
                break;
            case Direction.Down:
                if (animal.Top < UI.Resolution.Height - animal.Height)
                    animal.Top += speed;
                break;
            case Direction.Left:
                if (animal.Left > 0)
                    animal.Left -= speed;
                break;
            case Direction.Right:
                if (animal.Left < UI.Resolution.Width - animal.Width)
                    animal.Left += speed;
                break;
        }
    }

    private void ChangeDirection()
    {
        currentDirection = (Direction)Rand.Next(0, 4);
        currentTime = 0;
        directionTime = Rand.Next(50, 200); // Reset the time for the new direction
    }
}
