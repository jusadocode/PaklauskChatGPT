using RAID2D.Client.Drops;
using System.Drawing;

namespace RAID2D.Client.Tests.Drops;
public class AnimalDropTests
{
    private readonly AnimalDrop _testClass;
    private Point _location;
    private string _animalName;

    public AnimalDropTests()
    {
        _location = new Point();
        _animalName = "TestValue220102774";
        _testClass = new AnimalDrop(_location, _animalName);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new AnimalDrop(_location, _animalName);

        // Assert
        Assert.NotNull(instance);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotConstructWithInvalidAnimalName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => new AnimalDrop(_location, value));
    }

    [Fact]
    public void LocationIsInitializedCorrectly()
    {
        Assert.Equal(_location, _testClass.Location);
    }

    [Fact]
    public void CanGetName()
    {
        // Assert
        Assert.IsType<string>(_testClass.Name);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetImage()
    {
        // Assert
        Assert.IsType<Image>(_testClass.Image);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetSize()
    {
        // Assert
        Assert.IsType<Size>(_testClass.Size);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void AnimalNameIsInitializedCorrectly()
    {
        Assert.Equal(_animalName, _testClass.AnimalName);
    }
}