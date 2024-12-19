﻿using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;

namespace RAID2D.Client.States;
public class IdleState : IEntityState
{
    public IMovementStrategy Handle(PictureBox entity, Player player)
    {
        IMovementStrategy newStrategy = new WanderMovement(Constants.AnimalSpeed / 2);
        newStrategy.Move(entity);
        return newStrategy;
    }
}

