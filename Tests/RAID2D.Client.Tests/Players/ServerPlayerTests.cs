using RAID2D.Client.Players;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Players;
public class ServerPlayerTests
{
    private readonly ServerPlayer _testClass;

    public ServerPlayerTests()
    {
        _testClass = new ServerPlayer();
    }

    [Fact]
    public void CanCallCreate()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Left);

        // Act
        _testClass.Create(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallCreateWithNullGameState()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Create(default(GameState)));
    }

    [Fact]
    public void CanCallUpdate()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Right);

        // Act
        _testClass.Update(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallUpdateWithNullGameState()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Update(default(GameState)));
    }

    [Fact]
    public void CanGetPictureBox()
    {
        // Assert
        Assert.IsType<PictureBox>(_testClass.PictureBox);

        throw new NotImplementedException("Create or modify test");
    }
}