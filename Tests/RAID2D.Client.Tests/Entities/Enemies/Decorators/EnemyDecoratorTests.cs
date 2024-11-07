using NSubstitute;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities.Enemies.Decorators;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Entities.Enemies.Decorators;
public class EnemyDecoratorTests
{
    private class TestEnemyDecorator : EnemyDecorator
    {
        public TestEnemyDecorator(IEnemy enemy) : base(enemy)
        {
        }

        public override void UpdateAppearance()
        {
        }
    }

    private readonly TestEnemyDecorator _testClass;
    private readonly IEnemy _enemy;

    public EnemyDecoratorTests()
    {
        _enemy = Substitute.For<IEnemy>();
        _testClass = new TestEnemyDecorator(_enemy);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new TestEnemyDecorator(_enemy);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void CannotConstructWithNullEnemy()
    {
        Assert.Throws<ArgumentNullException>(() => new TestEnemyDecorator(default(IEnemy)));
    }

    [Fact]
    public void CanCallCreate()
    {
        // Arrange
        _enemy.Create().Returns(new PictureBox());

        // Act
        var result = _testClass.Create();

        // Assert
        _enemy.Received().Create();

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetPictureBox()
    {
        // Arrange
        var expectedValue = new PictureBox();
        _enemy.PictureBox.Returns(expectedValue);

        // Assert
        Assert.Same(expectedValue, _testClass.PictureBox);
    }
}