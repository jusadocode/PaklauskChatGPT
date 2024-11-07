using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;

namespace RAID2D.Client.Entities.Spawners;

public class DayEntitySpawner : IEntitySpawner
{
    private readonly IPrototype creeperPrototype = new Creeper();
    public IEnemy CreateEnemy()
    {
        IPrototype clonedCreeper = creeperPrototype.DeepClone();
        return clonedCreeper as IEnemy;
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Goat();
        animal.Create();

        return animal;
    }
}
