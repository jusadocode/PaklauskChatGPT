using RAID2D.Client.Entities.Spawners;

namespace RAID2D.Client.Tests.Entities.Spawners;
public class DayEntitySpawnerTests
{
    private readonly DayEntitySpawner _testClass;

    public DayEntitySpawnerTests()
    {
        _testClass = new DayEntitySpawner();
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