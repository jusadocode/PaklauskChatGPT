using Client.Enums;

namespace Client.Movement;

public class WanderMovement : IMovementStrategy
{
    private readonly Random random;
    private int directionTime;
    private readonly Control area;
    private int currentTime;
    private readonly int speed;
    private Direction currentDirection;

    public WanderMovement(Control area, int speed)
    {
        random = new Random();
        directionTime = random.Next(200, 1000); // Change direction every 50 to 200 ticks
        this.area = area;
        this.speed = speed;
        currentDirection = (Direction)random.Next(0, 4); // Random initial direction
    }

    public void Move(PictureBox animal)
    {
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
                if (animal.Top < area.ClientSize.Height - animal.Height)
                    animal.Top += speed;
                break;
            case Direction.Left:
                if (animal.Left > 0)
                    animal.Left -= speed;
                break;
            case Direction.Right:
                if (animal.Left < area.ClientSize.Width - animal.Width)
                    animal.Left += speed;
                break;
        }
    }

    private void ChangeDirection()
    {
        currentDirection = (Direction)random.Next(0, 4);
        currentTime = 0;
        directionTime = random.Next(50, 200); // Reset the time for the new direction
    }
}
