using RAID2D.Client.Entities.Spawners;
using System.Windows.Forms;

namespace RAID2D.Client.Managers;

public static class DayTimeManager
{
    private static Control backgroundControl = new();
    private static double currentHour = Constants.MiddleOfDayHour;
    private static Color previousColor = Constants.DayColor;

    public static void Initialize(Control control)
    {
        backgroundControl = control;
        backgroundControl.BackColor = Constants.DayColor;
    }

    public static void Update(double deltaTime)
    {
        currentHour = (currentHour + (deltaTime * Constants.HourIncrementRate)) % Constants.EndOfDayHour;

        double lerpFactor = (currentHour <= Constants.MiddleOfDayHour)
            ? currentHour / Constants.MiddleOfDayHour
            : 1.0f - ((currentHour - Constants.MiddleOfDayHour) / Constants.MiddleOfDayHour);

        int r = (int)(Constants.NightColor.R + ((Constants.DayColor.R - Constants.NightColor.R) * lerpFactor));
        int g = (int)(Constants.NightColor.G + ((Constants.DayColor.G - Constants.NightColor.G) * lerpFactor));
        int b = (int)(Constants.NightColor.B + ((Constants.DayColor.B - Constants.NightColor.B) * lerpFactor));

        Color newColor = Color.FromArgb(0xFF, r, g, b);

        if (previousColor != newColor)
        {
            backgroundControl.BackColor = newColor;
            previousColor = newColor;
        }
    }

    public static bool IsDay()
    {
        double lowerBound = Constants.MiddleOfDayHour - (Constants.MiddleOfDayHour / 2);
        double upperBound = Constants.MiddleOfDayHour + (Constants.MiddleOfDayHour / 2);

        return currentHour > lowerBound && currentHour <= upperBound;
    }
}
