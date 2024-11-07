using RAID2D.Client.Managers;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.Tests.Managers;
public static class ImageManagerTests
{
    [Fact]
    public static void CanCallGetImageFromDirectionWithNameAndXAndY()
    {
        // Arrange
        var name = "TestValue1214321747";
        var x = 707150565.99;
        var y = 1641405682.62;

        // Act
        var result = ImageManager.GetImageFromDirection(name, x, y);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetImageFromDirectionWithNameAndXAndYWithInvalidName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => ImageManager.GetImageFromDirection(value, 850222248.48, 1171354053.87));
    }

    [Fact]
    public static void CanCallGetImageFromDirectionWithNameAndDirection()
    {
        // Arrange
        var name = "TestValue801655435";
        var direction = Direction.Right;

        // Act
        var result = ImageManager.GetImageFromDirection(name, direction);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetImageFromDirectionWithNameAndDirectionWithInvalidName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => ImageManager.GetImageFromDirection(value, Direction.Down));
    }
}