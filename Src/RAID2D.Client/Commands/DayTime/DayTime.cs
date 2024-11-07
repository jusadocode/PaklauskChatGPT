namespace RAID2D.Client.Commands.DayTime;

enum TimeOfDay
{
    Day,
    Night,

    None
}

/// <summary>
/// The day time changer class. (Receiver)
/// </summary>
public class DayTime
{
    private double currentHour = Constants.MiddleOfDayHour;
    private Control backgroundControl = new();
    private TimeOfDay timeOfDay = TimeOfDay.None;

    private Action? onDayStart;
    private Action? onNightStart;

    public void Initialize(Control control, Action onDayStart, Action onNightStart)
    {
        backgroundControl = control;
        this.onDayStart = onDayStart;
        this.onNightStart = onNightStart;
    }

    public void SetDay()
    {
        backgroundControl.BackColor = Constants.DayColor;
        currentHour = Constants.MiddleOfDayHour - (Constants.MiddleOfDayHour / 2);
        timeOfDay = TimeOfDay.Day;
    }

    public void SetNight()
    {
        backgroundControl.BackColor = Constants.NightColor;
        currentHour = Constants.EndOfDayHour - (Constants.MiddleOfDayHour / 2);
        timeOfDay = TimeOfDay.Night;
    }

    public void Update(double deltaTime)
    {
        currentHour = (currentHour + (deltaTime * Constants.HourIncrementRate)) % Constants.EndOfDayHour;

        if (IsDay() && timeOfDay != TimeOfDay.Day)
            onDayStart?.Invoke();
        else if (!IsDay() && timeOfDay != TimeOfDay.Night)
            onNightStart?.Invoke();
    }

    private bool IsDay()
    {
        double lowerBound = Constants.MiddleOfDayHour - (Constants.MiddleOfDayHour / 2);
        double upperBound = Constants.MiddleOfDayHour + (Constants.MiddleOfDayHour / 2);

        return currentHour > lowerBound && currentHour <= upperBound;
    }
}
