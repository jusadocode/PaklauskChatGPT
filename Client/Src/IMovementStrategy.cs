using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    internal interface IMovementStrategy
    {
        void Move(PictureBox character);
    }
}
