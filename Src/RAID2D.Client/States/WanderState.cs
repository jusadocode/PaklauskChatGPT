using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;

namespace RAID2D.Client.States;

public class WanderState : IEntityState
{
    IMovementStrategy? wanderStrategy = null;

    public IMovementStrategy Handle(PictureBox entity, Player player)
    {
        wanderStrategy ??= new WanderMovement(Constants.AnimalSpeed / 2);

        wanderStrategy.Move(entity);
        return wanderStrategy;
    }
}

