using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ControlledMovement : IMovementStrategy
    {
        private bool goLeft, goRight, goUp, goDown;
        private Control area;
        private int speed;

        public ControlledMovement(bool goLeft, bool goRight, bool goUp, bool goDown, int speed, Control area)
        {
            this.area = area;
            this.speed = speed;
        }

        public void Move(PictureBox player)
        {
            if (goLeft && player.Left > 0) player.Left -= speed;
            if (goRight && player.Left + player.Width < area.ClientSize.Width) player.Left += speed;
            if (goUp && player.Top > 45) player.Top -= speed;
            if (goDown && player.Top + player.Height < area.ClientSize.Height) player.Top += speed;
        }

    }
}
