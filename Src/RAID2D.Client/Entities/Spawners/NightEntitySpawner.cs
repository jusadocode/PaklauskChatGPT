using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;

namespace RAID2D.Client.Entities.Spawners;

public class NightEntitySpawner : IEntitySpawner
{
    public IEnemy CreateEnemy()
    {
        IEnemy enemy = new Zombie();
        enemy.Create();

        return enemy;
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Boar();
        animal.Create();

        return animal;
    }
}
