using RAID2D.Client.Commands.DayTime;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Commands.DayTime;
public class DayTimeUnitTests
{
    private readonly DayTimeUnit _testClass;

    public DayTimeUnitTests()
    {
        _testClass = new DayTimeUnit();
    }

    [Fact]
    public void CanCallInitialize()
    {
        // Arrange
        var control = new Control();
        Action onDayStart = () => { };
        Action onNightStart = () => { };

        // Act
        _testClass.Initialize(control, onDayStart, onNightStart);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallInitializeWithNullControl()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Initialize(default(Control), () => { }, () => { }));
    }

    [Fact]
    public void CannotCallInitializeWithNullOnDayStart()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Initialize(new Control(), default(Action), () => { }));
    }

    [Fact]
    public void CannotCallInitializeWithNullOnNightStart()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.Initialize(new Control(), () => { }, default(Action)));
    }

    [Fact]
    public void CanCallSetDay()
    {
        // Act
        _testClass.SetDay();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetNight()
    {
        // Act
        _testClass.SetNight();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUpdate()
    {
        // Arrange
        var deltaTime = 1249414221.33;

        // Act
        _testClass.Update(deltaTime);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }
}