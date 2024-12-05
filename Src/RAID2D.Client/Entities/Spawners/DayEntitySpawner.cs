using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Prototype;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Spawners;

public class DayEntitySpawner : IEntitySpawner
{
    private Creeper? creeperPrototype;
    private Enderman? endermanPrototype;

    public IEnemy CreateEnemy()
    {
        IPrototype? clone;

        if (Rand.Next(0, 2) == 0)
        {
            if (endermanPrototype == null)
            {
                endermanPrototype = new Enderman();
                endermanPrototype.Create();
                //clone = endermanPrototype.ShallowClone();
                clone = endermanPrototype.DeepClone();
            }
            else
            {
                clone = endermanPrototype.DeepClone();
            }
        }
        else
        {
            if (creeperPrototype == null)
            {
                creeperPrototype = new Creeper();
                creeperPrototype.Create();
                //clone = creeperPrototype.ShallowClone();
                clone = creeperPrototype.DeepClone();
            }
            else
            {
                clone = creeperPrototype.DeepClone();
            }
        }

        return clone as IEnemy ?? throw new InvalidOperationException("This should not happen");
    }

    public IAnimal CreateAnimal()
    {
        IAnimal animal = Rand.Next(0, 2) == 0 ? new Boar() : new Cow();

        animal.Create();
        return animal;
    }
}
