using NSubstitute;
using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Builders;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Drops.Builders;
public class ValuableDropBuilderTests
{
    private readonly ValuableDropBuilder _testClass;

    public ValuableDropBuilderTests()
    {
        _testClass = new ValuableDropBuilder();
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new ValuableDropBuilder();

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CanCallSetTag()
    {
        // Arrange
        var drop = Substitute.For<IDroppableItem>();

        // Act
        var result = _testClass.SetTag(drop);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallSetTagWithNullDrop()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetTag(default(IDroppableItem)));
    }

    [Fact]
    public void CanCallSetName()
    {
        // Arrange
        var name = "TestValue2047556208";

        // Act
        var result = _testClass.SetName(name);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotCallSetNameWithInvalidName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetName(value));
    }

    [Fact]
    public void CanCallSetImage()
    {
        // Arrange
        Image image = new Bitmap(1, 1);

        // Act
        var result = _testClass.SetImage(image);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallSetImageWithNullImage()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetImage(default(Image)));
    }

    [Fact]
    public void CanCallSetLocation()
    {
        // Arrange
        var location = new Point();

        // Act
        var result = _testClass.SetLocation(location);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetSize()
    {
        // Arrange
        var size = new Size();

        // Act
        var result = _testClass.SetSize(size);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetSizeMode()
    {
        // Arrange
        var sizeMode = PictureBoxSizeMode.Normal;

        // Act
        var result = _testClass.SetSizeMode(sizeMode);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallBuild()
    {
        // Act
        var result = _testClass.Build();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}