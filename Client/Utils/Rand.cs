using Client.UI;

namespace Client.Utils;

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

    //public static uint NextUint(uint minValue, uint maxValue)
    //{
    //    return _random.Next(minValue, maxValue);
    //}

    public static double NextDouble()
    {
        return _random.NextDouble();
    }

    public static Point LocationOnScreen()
    {
        return new Point(
            Next((int)Constants.FormBounds, UIManager.GetInstance().Resolution.Width - (int)Constants.FormBounds),
            Next((int)Constants.FormBounds, UIManager.GetInstance().Resolution.Height - (int)Constants.FormBounds)
        );
    }
}