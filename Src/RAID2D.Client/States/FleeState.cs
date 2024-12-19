using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;

namespace RAID2D.Client.States;

public class FleeState : IEntityState
{
    IMovementStrategy? fleeStrategy = null;

    public IMovementStrategy Handle(PictureBox entity, Player player)
    {
        fleeStrategy ??= new FleeMovement(player.PictureBox, Constants.AnimalSpeed);

        fleeStrategy.Move(entity);
        return fleeStrategy;
    }
}

