namespace Client.Movement;

public class FollowPlayerMovement(PictureBox player, Control area, int speed) : IMovementStrategy
{
    public void Move(PictureBox zombie)
    {
        if (zombie.Left > player.Left)
        {
            zombie.Left -= speed;
            zombie.Image = Assets.zleft;
        }
        else if (zombie.Left < player.Left)
        {
            zombie.Left += speed;
            zombie.Image = Assets.zright;
        }

        if (zombie.Top > player.Top)
        {
            zombie.Top -= speed;
            zombie.Image = Assets.zup;
        }
        else if (zombie.Top < player.Top)
        {
            zombie.Top += speed;
            zombie.Image = Assets.zdown;
        }
    }
}
