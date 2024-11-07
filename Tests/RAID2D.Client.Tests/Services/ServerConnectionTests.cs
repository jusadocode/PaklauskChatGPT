using RAID2D.Client.Services;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Drawing;

namespace RAID2D.Client.Tests.Services;
public class ServerConnectionTests
{
    private readonly ServerConnection _testClass;

    public ServerConnectionTests()
    {
        _testClass = new ServerConnection();
    }

    [Fact]
    public void CanCallIsConnected()
    {
        // Act
        var result = _testClass.IsConnected();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallConnect()
    {
        // Arrange
        var serverUrl = "TestValue1483570297";

        // Act
        _testClass.Connect(serverUrl);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotCallConnectWithInvalidServerUrl(string value)
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Connect(value));
    }

    [Fact]
    public void CanCallSetCallbacks()
    {
        // Arrange
        Action<GameState> onGameStateReceive = x => { };

        // Act
        _testClass.SetCallbacks(onGameStateReceive);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallSetCallbacksWithNullOnGameStateReceive()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetCallbacks(default(Action<GameState>)));
    }

    [Fact]
    public async Task CanCallDisconnectAsync()
    {
        // Act
        await _testClass.DisconnectAsync();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public async Task CanCallSendGameStateAsync()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Up);

        // Act
        await _testClass.SendGameStateAsync(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public async Task CannotCallSendGameStateAsyncWithNullGameState()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.SendGameStateAsync(default(GameState)));
    }

    [Fact]
    public void CanCallReceiveGameState()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Right);

        // Act
        _testClass.ReceiveGameState(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallReceiveGameStateWithNullGameState()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.ReceiveGameState(default(GameState)));
    }
}