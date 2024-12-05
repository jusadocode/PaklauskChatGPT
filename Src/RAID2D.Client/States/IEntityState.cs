using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.States;
public interface IEntityState
{
    IMovementStrategy Handle(PictureBox entity, Player player);
}


