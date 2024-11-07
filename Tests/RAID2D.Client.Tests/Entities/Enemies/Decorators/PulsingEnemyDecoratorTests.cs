using NSubstitute;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Decorators;

namespace RAID2D.Client.Tests.Entities.Enemies.Decorators;
public class PulsingEnemyDecoratorTests
{
    private readonly PulsingEnemyDecorator _testClass;
    private readonly IEnemy _enemy;

    public PulsingEnemyDecoratorTests()
    {
        _enemy = Substitute.For<IEnemy>();
        _testClass = new PulsingEnemyDecorator(_enemy);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new PulsingEnemyDecorator(_enemy);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CannotConstructWithNullEnemy()
    {
        Assert.Throws<ArgumentNullException>(() => new PulsingEnemyDecorator(default(IEnemy)));
    }

    [Fact]
    public void CanCallUpdateAppearance()
    {
        // Act
        _testClass.UpdateAppearance();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}