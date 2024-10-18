using RAID2D.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RAID2D;

namespace RAID2D
{
    public interface IGameObject
    {
        PictureBox CreatePictureBox(Point location);
    }
}
