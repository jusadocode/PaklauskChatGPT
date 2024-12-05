using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Spawners;

public class NightEntitySpawner : IEntitySpawner
{
    private Zombie? zombiePrototype;
    private Spider? spiderPrototype;

    public IEnemy CreateEnemy()
    {
        IPrototype? clone;

        if (Rand.Next(0, 2) == 0)
        {
            if (spiderPrototype == null)
            {
                spiderPrototype = new Spider();
                spiderPrototype.Create();
                //clone = spiderPrototype.ShallowClone();
                clone = spiderPrototype.DeepClone();
            }
            else
            {
                clone = spiderPrototype.DeepClone();
            }
        }
        else
        {
            if (zombiePrototype == null)
            {
                zombiePrototype = new Zombie();
                zombiePrototype.Create();
                //clone = zombiePrototype.ShallowClone();
                clone = zombiePrototype.DeepClone();
            }
            else
            {
                clone = zombiePrototype.DeepClone();
            }
        }

        return clone as IEnemy ?? throw new InvalidOperationException("This should not happen");
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = Rand.Next(0, 2) == 0 ? new Goat() : new Sheep();

        animal.Create();
        return animal;
    }
}
