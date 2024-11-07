using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using RAID2D.Server.Hubs;
using RAID2D.Server.Observers;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Drawing;

namespace RAID2D.Server.Tests.Hubs;
public class GameHubTests
{
    private readonly GameHub _testClass;
    private GameStateSubject _gameStateSubject;
    private readonly IHubContext<GameHub> _hubContext;

    public GameHubTests()
    {
        _gameStateSubject = new GameStateSubject();
        _hubContext = Substitute.For<IHubContext<GameHub>>();
        _testClass = new GameHub(_gameStateSubject, _hubContext);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new GameHub(_gameStateSubject, _hubContext);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CannotConstructWithNullGameStateSubject()
    {
        Assert.Throws<ArgumentNullException>(() => new GameHub(default(GameStateSubject), _hubContext));
    }

    [Fact]
    public void CannotConstructWithNullHubContext()
    {
        Assert.Throws<ArgumentNullException>(() => new GameHub(_gameStateSubject, default(IHubContext<GameHub>)));
    }

    [Fact]
    public async Task CanCallOnConnectedAsync()
    {
        // Act
        await _testClass.OnConnectedAsync();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public async Task CanCallOnDisconnectedAsync()
    {
        // Arrange
        var exception = new Exception();

        // Act
        await _testClass.OnDisconnectedAsync(exception);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public async Task CanCallSendGameStateUpdate()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Left);

        // Act
        await _testClass.SendGameStateUpdate(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public async Task CannotCallSendGameStateUpdateWithNullGameState()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.SendGameStateUpdate(default(GameState)));
    }
}