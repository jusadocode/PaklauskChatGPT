using RAID2D.Client.MovementStrategies;
using RAID2D.Client.Players;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.MovementStrategies;
public class PlayerMovementTests
{
    private readonly PlayerMovement _testClass;
    private readonly Player player = new Player();

    public PlayerMovementTests()
    {
        _testClass = new PlayerMovement(player, 20);
    }

    [Fact]
    public void CanCallMove()
    {
        // Arrange
        var character = new PictureBox();

        // Act
        ((IMovementStrategy)_testClass).Move(character);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallMoveWithNullCharacter()
    {
        Assert.Throws<ArgumentNullException>(() => ((IMovementStrategy)_testClass).Move(default(PictureBox)));
    }

    [Fact]
    public void CanGetSpeed()
    {
        // Assert
        Assert.IsType<int>(_testClass.Speed);

        throw new NotImplementedException("Create or modify test");
    }
}