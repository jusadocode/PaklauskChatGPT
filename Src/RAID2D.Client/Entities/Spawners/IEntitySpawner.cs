using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;

namespace RAID2D.Client.Entities.Spawners;

public interface IEntitySpawner
{
    public IEnemy CreateEnemy();
    public IAnimal CreateAnimal();
}

