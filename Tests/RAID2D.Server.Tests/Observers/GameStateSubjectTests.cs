
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using NSubstitute;
using RAID2D.Server.Observers;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using Xunit;

namespace RAID2D.Server.Tests.Observers;
public class GameStateSubjectTests
{
    private readonly GameStateSubject _testClass;

    public GameStateSubjectTests()
    {
        _testClass = new GameStateSubject();
    }

    [Fact]
    public void CanCallAttach()
    {
        // Arrange
        var observer = Substitute.For<IObserver>();

        // Act
        _testClass.Attach(observer);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallAttachWithNullObserver()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Attach(default(IObserver)));
    }

    [Fact]
    public void CanCallDetach()
    {
        // Arrange
        var observer = Substitute.For<IObserver>();

        // Act
        _testClass.Detach(observer);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallDetachWithNullObserver()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Detach(default(IObserver)));
    }

    [Fact]
    public async Task CanCallNotifyAll()
    {
        // Act
        await _testClass.NotifyAll();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetState()
    {
        // Arrange
        var gameState = new GameState(new Point(), Direction.Right);

        // Act
        _testClass.SetState(gameState);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallSetStateWithNullGameState()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetState(default(GameState)));
    }

    [Fact]
    public void CanCallGetState()
    {
        // Act
        var result = _testClass.GetState();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanSetAndGetObservers()
    {
        // Arrange
        var testValue = new List<IObserver>();

        // Act
        _testClass.Observers = testValue;

        // Assert
        Assert.Same(testValue, _testClass.Observers);
    }
}