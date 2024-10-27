using Client.UI;

namespace Client.Utils;

public static class Util
{
    public static double EuclideanDistance(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }

    public static Point MiddleOfScreen()
    {
        return (Point)(UIManager.GetInstance().Resolution / 2);
    }

    public static Point MiddleOfScreen(Control control)
    {
        return MiddleOfScreen(control.Size);
    }

    public static Point MiddleOfScreen(Size size)
    {
        return MiddleOfScreen() - (size / 2);
    }
}
