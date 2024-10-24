namespace Client.Movement;

public class FollowPlayerMovement(PictureBox player, Control area, int speed) : IMovementStrategy
{
    public void Move(PictureBox zombie)
    {
        if (zombie.Left > player.Left)
        {
            zombie.Left -= speed;
            zombie.Image = Assets.ZombieLeft;
        }
        else if (zombie.Left < player.Left)
        {
            zombie.Left += speed;
            zombie.Image = Assets.ZombieRight;
        }

        if (zombie.Top > player.Top)
        {
            zombie.Top -= speed;
            zombie.Image = Assets.ZombieUp;
        }
        else if (zombie.Top < player.Top)
        {
            zombie.Top += speed;
            zombie.Image = Assets.ZombieDown;
        }
    }
}
