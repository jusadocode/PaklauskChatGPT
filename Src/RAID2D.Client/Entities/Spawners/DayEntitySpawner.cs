using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;

namespace RAID2D.Client.Entities.Spawners;

public class DayEntitySpawner : IEntitySpawner
{
    public IEnemy CreateEnemy()
    {
        IEnemy enemy = new Creeper();
        enemy.Create();

        return enemy;
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Goat();
        animal.Create();

        return animal;
    }
}
