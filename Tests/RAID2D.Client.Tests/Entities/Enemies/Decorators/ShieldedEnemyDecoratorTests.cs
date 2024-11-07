using NSubstitute;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Decorators;

namespace RAID2D.Client.Tests.Entities.Enemies.Decorators;
public class ShieldedEnemyDecoratorTests
{
    private readonly ShieldedEnemyDecorator _testClass;
    private readonly IEnemy _baseEnemy;

    public ShieldedEnemyDecoratorTests()
    {
        _baseEnemy = Substitute.For<IEnemy>();
        _testClass = new ShieldedEnemyDecorator(_baseEnemy);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new ShieldedEnemyDecorator(_baseEnemy);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CannotConstructWithNullBaseEnemy()
    {
        Assert.Throws<ArgumentNullException>(() => new ShieldedEnemyDecorator(default(IEnemy)));
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