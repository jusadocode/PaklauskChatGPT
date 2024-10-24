using Client.Entities.Animals;
using Client.Entities.Enemies;

namespace Client.Entities.Spawners;

public class NightEntitySpawner : IEntitySpawner
{
    public List<IEnemy> CreateEnemies(uint count)
    {
        List<IEnemy> enemies = [];

        for (uint i = 0; i < count; i++)
        {
            IEnemy enemy = new Zombie();
            enemy.Create();
            enemies.Add(enemy);
        }

        return enemies;
    }
    public List<IAnimal> CreateAnimals(uint count)
    {
        List<IAnimal> animals = [];

        for (uint i = 0; i < count; i++)
        {
            IAnimal animal = new Boar();
            animal.Create();
            animals.Add(animal);
        }

        return animals;
    }
}

