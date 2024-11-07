using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;

namespace RAID2D.Client.Entities.Spawners;

public class DayEntitySpawner : IEntitySpawner
{
    private Creeper? creeperPrototype;

    public IEnemy CreateEnemy()
    {
        creeperPrototype ??= new Creeper();

        IPrototype clonedCreeper = creeperPrototype.DeepClone();
        return clonedCreeper as IEnemy ?? throw new InvalidOperationException("This should not happen");
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = new Goat();
        animal.Create();

        return animal;
    }
}
