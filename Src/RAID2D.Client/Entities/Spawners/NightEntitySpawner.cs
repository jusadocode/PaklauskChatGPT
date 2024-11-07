using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;

namespace RAID2D.Client.Entities.Spawners;

public class NightEntitySpawner : IEntitySpawner
{
    private Zombie? zombiePrototype;

    public IEnemy CreateEnemy()
    {
        zombiePrototype ??= new Zombie();

        IPrototype clonedZombie = zombiePrototype.DeepClone();
        return clonedZombie as IEnemy ?? throw new InvalidOperationException("This should not happen");
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Boar();
        animal.Create();

        return animal;
    }
}
