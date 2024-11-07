using RAID2D.Client.Commands.DayTime;

namespace RAID2D.Client.Tests.Commands.DayTime;
public class SetDayCommandTests
{
    private readonly SetDayCommand _testClass;
    private readonly DayTimeUnit _dayTimeUnit = new DayTimeUnit();

    public SetDayCommandTests()
    {
        _testClass = new SetDayCommand(_dayTimeUnit);
    }

    [Fact]
    public void CanCallExecute()
    {
        // Act
        _testClass.Execute();

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