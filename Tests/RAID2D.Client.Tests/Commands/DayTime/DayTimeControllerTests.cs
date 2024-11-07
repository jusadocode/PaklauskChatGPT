using NSubstitute;
using RAID2D.Client.Commands;
using RAID2D.Client.Commands.DayTime;

namespace RAID2D.Client.Tests.Commands.DayTime;
public class DayTimeControllerTests
{
    private readonly DayTimeController _testClass;

    public DayTimeControllerTests()
    {
        _testClass = new DayTimeController();
    }

    [Fact]
    public void CanCallSetCommand()
    {
        // Arrange
        var command = Substitute.For<ICommand>();

        // Act
        _testClass.SetCommand(command);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallSetCommandWithNullCommand()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.SetCommand(default(ICommand)));
    }

    [Fact]
    public void CanCallRun()
    {
        // Act
        _testClass.Run();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUndo()
    {
        // Act
        _testClass.Undo();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}