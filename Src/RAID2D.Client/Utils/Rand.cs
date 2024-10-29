using RAID2D.Client.UI;

namespace RAID2D.Client.Utils;

/// <summary>
/// Wrapper for Random class, with some additional methods
/// </summary>
public static class Rand
{
    private static readonly Random _random = new();

    public static int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    public static int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }

    //public static uint NextUint(uint minValue, uint maxValue)
    //{
    //    return _random.Next(minValue, maxValue);
    //}

    public static double NextDouble()
    {
        return _random.NextDouble();
    }

    public static Point LocationOnScreen(Size sizeOfControl)
    {
        int x = Next(Constants.FormBounds, UIManager.GetInstance().Resolution.Width - Constants.FormBounds - sizeOfControl.Width);
        int y = Next(Constants.FormBounds, UIManager.GetInstance().Resolution.Height - Constants.FormBounds - sizeOfControl.Height);

        return new Point(x, y);
    }
}