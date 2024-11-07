using RAID2D.Client.Drops.Spawners;
using System.Drawing;

namespace RAID2D.Client.Tests.Drops.Spawners;
public class DropSpawnerTests
{
    private readonly DropSpawner _testClass;

    public DropSpawnerTests()
    {
        _testClass = new DropSpawner();
    }

    [Fact]
    public void CanCallCreateDrop()
    {
        // Arrange
        var dropType = "TestValue1523171615";
        var location = new Point?();
        var animalName = "TestValue858138945";

        // Act
        var result = ((IDropSpawner)_testClass).CreateDrop(dropType, location, animalName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotCallCreateDropWithInvalidDropType(string value)
    {
        Assert.Throws<ArgumentNullException>(() => ((IDropSpawner)_testClass).CreateDrop(value, new Point?(), "TestValue115049779"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotCallCreateDropWithInvalidAnimalName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => ((IDropSpawner)_testClass).CreateDrop("TestValue169055020", new Point?(), value));
    }

    [Fact]
    public void CreateDropPerformsMapping()
    {
        // Arrange
        var dropType = "TestValue519802955";
        var location = new Point?();
        var animalName = "TestValue1386130801";

        // Act
        var result = ((IDropSpawner)_testClass).CreateDrop(dropType, location, animalName);

        // Assert
        Assert.Equal(location, result.Location);
    }
}