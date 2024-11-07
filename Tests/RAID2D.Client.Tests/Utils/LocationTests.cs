using RAID2D.Client.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Utils;
public static class LocationTests
{
    [Fact]
    public static void CanCallMiddleOfScreenWithNoParameters()
    {
        // Act
        var result = Location.MiddleOfScreen();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallMiddleOfScreenWithSizeOfControl()
    {
        // Arrange
        var sizeOfControl = new Size();

        // Act
        var result = Location.MiddleOfScreen(sizeOfControl);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void MiddleOfScreenWithSizeOfControlPerformsMapping()
    {
        // Arrange
        var sizeOfControl = new Size();

        // Act
        var result = Location.MiddleOfScreen(sizeOfControl);

        // Assert
        Assert.Equal(sizeOfControl.IsEmpty, result.IsEmpty);
    }

    [Fact]
    public static void CanCallMiddleOfScreenWithControl()
    {
        // Arrange
        var control = new Control();

        // Act
        var result = Location.MiddleOfScreen(control);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CannotCallMiddleOfScreenWithControlWithNullControl()
    {
        Assert.Throws<ArgumentNullException>(() => Location.MiddleOfScreen(default(Control)));
    }

    [Fact]
    public static void CanCallClampToBounds()
    {
        // Arrange
        var pointOfControl = new Point();
        var sizeOfControl = new Size();

        // Act
        var result = Location.ClampToBounds(pointOfControl, sizeOfControl);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void ClampToBoundsPerformsMapping()
    {
        // Arrange
        var pointOfControl = new Point();
        var sizeOfControl = new Size();

        // Act
        var result = Location.ClampToBounds(pointOfControl, sizeOfControl);

        // Assert
        Assert.Equal(sizeOfControl.IsEmpty, result.IsEmpty);
    }

    [Fact]
    public static void CanCallDistance()
    {
        // Arrange
        var point1 = new Point();
        var point2 = new Point();

        // Act
        var result = Location.Distance(point1, point2);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}