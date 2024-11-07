
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using RAID2D.Server.Hubs;
using RAID2D.Server.Observers;
using Xunit;

namespace RAID2D.Server.Tests.Observers;
public class GameStateObserverTests
{
    private readonly GameStateObserver _testClass;
    private readonly ISubject _subject;
    private readonly string _connectionID;
    private readonly IHubContext<GameHub> _hubContext;

    public GameStateObserverTests()
    {
        _subject = Substitute.For<ISubject>();
        _connectionID = "TestValue2138064274";
        _hubContext = Substitute.For<IHubContext<GameHub>>();
        _testClass = new GameStateObserver(_subject, _connectionID, _hubContext);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new GameStateObserver(_subject, _connectionID, _hubContext);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CannotConstructWithNullSubject()
    {
        Assert.Throws<ArgumentNullException>(() => new GameStateObserver(default(ISubject), _connectionID, _hubContext));
    }

    [Fact]
    public void CannotConstructWithNullHubContext()
    {
        Assert.Throws<ArgumentNullException>(() => new GameStateObserver(_subject, _connectionID, default(IHubContext<GameHub>)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CannotConstructWithInvalidConnectionID(string value)
    {
        Assert.Throws<ArgumentNullException>(() => new GameStateObserver(_subject, value, _hubContext));
    }

    [Fact]
    public async Task CanCallUpdate()
    {
        // Act
        await _testClass.Update();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void ConnectionIDIsInitializedCorrectly()
    {
        Assert.Equal(_connectionID, _testClass.ConnectionID);
    }
}