using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAID2D
{
    public class FollowPlayerMovement: IMovementStrategy
    {
        private PictureBox player;
        private Control playArea;

        public FollowPlayerMovement(PictureBox player, Control area)
        {
            this.player = player;
            this.playArea = area;
        }

        public void Move(PictureBox zombie, int speed)
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
