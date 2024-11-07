using RAID2D.Client.MovementStrategies;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.MovementStrategies;
public class FleeMovementTests
{
    private readonly FleeMovement _testClass;

    public FleeMovementTests()
    {
        _testClass = new FleeMovement(new PictureBox(), 20);
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