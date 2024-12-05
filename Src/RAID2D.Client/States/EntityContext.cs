using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.States;
public class EntityContext
{
    private IEntityState _currentState;
    public IMovementStrategy MovementStrategy { get; private set; }

    public EntityContext()
    {
        _currentState = new IdleState();
        MovementStrategy = new WanderMovement(Constants.AnimalSpeed / 2); 
    }

    public void SetState(IEntityState newState)
    {
        _currentState = newState;
    }

    public void UpdateState(PictureBox entity, Player player)
    {
       
        MovementStrategy = _currentState.Handle(entity, player);
    }
}



