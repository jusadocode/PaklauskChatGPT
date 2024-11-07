using RAID2D.Client.Entities.Enemies;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Entities.Enemies;
public class CreeperTests
{
    private readonly Creeper _testClass;

    public CreeperTests()
    {
        _testClass = new Creeper();
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new Creeper();

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanCallCreate()
    {
        // Act
        var result = _testClass.Create();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallShallowClone()
    {
        // Act
        var result = _testClass.ShallowClone();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallDeepClone()
    {
        // Act
        var result = _testClass.DeepClone();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetPictureBox()
    {
        // Assert
        Assert.IsType<PictureBox>(_testClass.PictureBox);

        throw new NotImplementedException("Create or modify test");
    }
}