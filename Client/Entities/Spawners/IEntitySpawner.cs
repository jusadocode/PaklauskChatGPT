﻿using Client.Entities.Animals;
using Client.Entities.Enemies;

namespace Client.Entities.Spawners;

public interface IEntitySpawner
{
    public IEnemy CreateEnemy();
    public IAnimal CreateAnimal();
}

