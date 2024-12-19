using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;

namespace RAID2D.Client.States;
public class ChaseState : IEntityState
{
    public IMovementStrategy Handle(PictureBox entity, Player player)
    {
        IMovementStrategy chaseStrategy = new ChaseMovement(player.PictureBox, Constants.EnemySpeed);
        chaseStrategy.Move(entity);
        return chaseStrategy;
    }
}

