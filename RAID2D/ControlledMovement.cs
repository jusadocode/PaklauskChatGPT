using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAID2D
{
    public class ControlledMovement : IMovementStrategy
    {
        private bool goLeft, goRight, goUp, goDown;
        private Control area;

        public ControlledMovement(bool goLeft, bool goRight, bool goUp, bool goDown)
        {
            this.goLeft = goLeft; 
            this.goRight = goRight;
            this.goUp = goUp;
            this.goDown = goDown;
            this.area = new Control();
        }

        public void Move(PictureBox player, int speed)
        {
            if (goLeft && player.Left > 0) player.Left -= speed;
            if (goRight && player.Left + player.Width < area.ClientSize.Width) player.Left += speed;
            if (goUp && player.Top > 45) player.Top -= speed;
            if (goDown && player.Top + player.Height < area.ClientSize.Height) player.Top += speed;
        }

    }
}
