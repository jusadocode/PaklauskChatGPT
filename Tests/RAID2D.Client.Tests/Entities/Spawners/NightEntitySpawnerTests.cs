using RAID2D.Client.Entities.Spawners;

namespace RAID2D.Client.Tests.Entities.Spawners;
public class NightEntitySpawnerTests
{
    private readonly NightEntitySpawner _testClass;

    public NightEntitySpawnerTests()
    {
        _testClass = new NightEntitySpawner();
    }

    [Fact]
    public void CanCallCreateEnemy()
    {
        // Act
        var result = _testClass.CreateEnemy();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallCreateAnimal()
    {
        // Act
        var result = _testClass.CreateAnimal();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}