using RAID2D.Client.Utils;
using System.Drawing;

namespace RAID2D.Client.Tests.Utils;
public static class RandTests
{
    [Fact]
    public static void CanCallNextWithMinValueAndMaxValue()
    {
        // Arrange
        var minValue = 1638949004;
        var maxValue = 1022940697;

        // Act
        var result = Rand.Next(minValue, maxValue);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallNextWithMaxValue()
    {
        // Arrange
        var maxValue = 1967369048;

        // Act
        var result = Rand.Next(maxValue);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallNextDouble()
    {
        // Act
        var result = Rand.NextDouble();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallLocationOnScreen()
    {
        // Arrange
        var sizeOfControl = new Size();

        // Act
        var result = Rand.LocationOnScreen(sizeOfControl);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void LocationOnScreenPerformsMapping()
    {
        // Arrange
        var sizeOfControl = new Size();

        // Act
        var result = Rand.LocationOnScreen(sizeOfControl);

        // Assert
        Assert.Equal(sizeOfControl.IsEmpty, result.IsEmpty);
    }
}