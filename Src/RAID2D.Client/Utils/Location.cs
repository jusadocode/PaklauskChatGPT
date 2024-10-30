using RAID2D.Client.UI;

namespace RAID2D.Client.Utils;

public static class Location
{
    public static Point MiddleOfScreen()
    {
        return (Point)(GUI.GetInstance().Resolution / 2);
    }

    public static Point MiddleOfScreen(Size sizeOfControl)
    {
        return MiddleOfScreen() - (sizeOfControl / 2);
    }

    public static Point MiddleOfScreen(Control control)
    {
        return MiddleOfScreen(control.Size);
    }

    public static Point ClampToBounds(Point pointOfControl, Size sizeOfControl)
    {
        GUI UI = GUI.GetInstance();
        int offset = Constants.FormBounds;

        return new Point(
            Math.Clamp(pointOfControl.X, offset, UI.Resolution.Width - offset - sizeOfControl.Width),
            Math.Clamp(pointOfControl.Y, offset, UI.Resolution.Height - offset - sizeOfControl.Height)
        );
    }

    public static double Distance(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }
}
