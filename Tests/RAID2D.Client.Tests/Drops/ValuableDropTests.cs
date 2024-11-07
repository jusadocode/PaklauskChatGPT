using RAID2D.Client.Drops;
using System.Drawing;

namespace RAID2D.Client.Tests.Drops;
public class ValuableDropTests
{
    private readonly ValuableDrop _testClass;
    private Point _location;

    public ValuableDropTests()
    {
        _location = new Point();
        _testClass = new ValuableDrop(_location);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new ValuableDrop(_location);

        // Assert
        Assert.NotNull(instance);
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
}