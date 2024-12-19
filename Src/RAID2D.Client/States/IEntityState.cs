using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;

namespace RAID2D.Client.States;
public interface IEntityState
{
    IMovementStrategy Handle(PictureBox entity, Player player);
}

