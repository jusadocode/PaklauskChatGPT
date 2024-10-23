namespace Client
{
    public class FollowPlayerMovement : IMovementStrategy
    {
        private PictureBox player;
        private Control playArea;
        private int speed;

        public FollowPlayerMovement(PictureBox player, Control area, int speed)
        {
            this.player = player;
            playArea = area;
            this.speed = speed;
        }

        public void Move(PictureBox zombie)
        {
            if (zombie.Left > player.Left)
            {
                zombie.Left -= speed;
                zombie.Image = Properties.Resources.zleft;
            }
            else if (zombie.Left < player.Left)
            {
                zombie.Left += speed;
                zombie.Image = Properties.Resources.zright;
            }

            if (zombie.Top > player.Top)
            {
                zombie.Top -= speed;
                zombie.Image = Properties.Resources.zup;
            }
            else if (zombie.Top < player.Top)
            {
                zombie.Top += speed;
                zombie.Image = Properties.Resources.zdown;
            }
        }
    }

}
