namespace Client.Movement;

public class ControlledMovement(bool goLeft, bool goRight, bool goUp, bool goDown, int speed, Control area) : IMovementStrategy
{
    private readonly bool goLeft, goRight, goUp, goDown;

    public void Move(PictureBox player)
    {
        if (goLeft && player.Left > 0)
            player.Left -= speed;
        if (goRight && player.Left + player.Width < area.ClientSize.Width)
            player.Left += speed;
        if (goUp && player.Top > 45)
            player.Top -= speed;
        if (goDown && player.Top + player.Height < area.ClientSize.Height)
            player.Top += speed;
    }
}
