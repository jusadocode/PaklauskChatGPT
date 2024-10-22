using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAID2D
{
    public class FleeMovement : IMovementStrategy
    {
        private PictureBox player;
        private int fleeDistance = 200; // Distance threshold for fleeing

        public FleeMovement(PictureBox player)
        {
            this.player = player;
        }

        public void Move(PictureBox entity, int speed)
        {
            double distance = Math.Sqrt(Math.Pow(entity.Left - player.Left, 2) + Math.Pow(entity.Top - player.Top, 2));

            if (distance < fleeDistance)
            {
                if (entity.Left < player.Left)
                {
                    entity.Left -= speed; // Move left
                }
                else
                {
                    entity.Left += speed; // Move right
                }

                if (entity.Top < player.Top)
                {
                    entity.Top -= speed; // Move up
                }
                else
                {
                    entity.Top += speed; // Move down
                }
            }
        }
    }
}
