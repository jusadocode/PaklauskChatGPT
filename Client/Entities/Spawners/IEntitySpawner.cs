using Client.Entities.Animals;
using Client.Entities.Enemies;

namespace Client.Entities.Spawners;

public interface IEntitySpawner
{
    public List<IEnemy> CreateEnemies(uint count);
    public List<IAnimal> CreateAnimals(uint count);
}

