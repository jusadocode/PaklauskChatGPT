using RAID2D.Client.Drops;
using System.Drawing;

namespace RAID2D.Client.Tests.Drops;
public class MedicalDropTests
{
    private readonly MedicalDrop _testClass;

    public MedicalDropTests()
    {
        _testClass = new MedicalDrop();
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new MedicalDrop();

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanGetLocation()
    {
        // Assert
        Assert.IsType<Point>(_testClass.Location);

        throw new NotImplementedException("Create or modify test");
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