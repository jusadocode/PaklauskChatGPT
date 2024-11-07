using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;

namespace RAID2D.Client.Entities.Spawners;

public class NightEntitySpawner : IEntitySpawner
{
    private readonly IPrototype zombiePrototype = new Zombie();
    public IEnemy CreateEnemy()
    {
        IPrototype clonedZombie = zombiePrototype.DeepClone();
        return clonedZombie as IEnemy;
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Boar();
        animal.Create();

        return animal;
    }
}
