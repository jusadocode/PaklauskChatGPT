using RAID2D.Client.Managers;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Managers;
public static class InputManagerTests
{
    [Fact]
    public static void CanCallIsKeyDown()
    {
        // Arrange
        var key = Keys.Oem3;

        // Act
        var result = InputManager.IsKeyDown(key);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallIsKeyDownOnce()
    {
        // Arrange
        var key = Keys.F3;

        // Act
        var result = InputManager.IsKeyDownOnce(key);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}