using RAID2D;
using System.Windows.Forms;
using System;

public class WanderMovement : IMovementStrategy
{
    private Random random;
    private int directionTime;
    private Control _area;
    private int currentTime;
    private int speed;

    public WanderMovement(Control area, int speed)
    {
        random = new Random();
        directionTime = random.Next(50, 200); // Change direction every 50 to 200 ticks
        _area = area;
        this.speed = speed;
    }

    public void Move(PictureBox animal)
    {
        currentTime++;

        if (currentTime >= directionTime)
        {
            int direction = random.Next(0, 4); 

            switch (direction)
            {
                case 0: // Up
                    if (animal.Top > 0) animal.Top -= speed;
                    else
                        changeDirection();
                    break;
                case 1: // Down
                    if (animal.Top < animal.Parent.ClientSize.Height - animal.Height) animal.Top += speed;
                    else
                        changeDirection();
                    break;
                case 2: // Left
                    if (animal.Left > 0) animal.Left -= speed;
                    else
                        changeDirection();
                    break;
                case 3: // Right
                    if (animal.Left < animal.Parent.ClientSize.Width - animal.Width) animal.Left += speed;
                    else
                        changeDirection();
                    break;
            }

            currentTime = 0;
        }   changeDirection();
    }

    private void changeDirection()
    {
        directionTime = random.Next(50, 200);
    }
}
