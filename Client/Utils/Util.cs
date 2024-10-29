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

    public static Point MiddleOfScreen(Size sizeOfControl)
    {
        return MiddleOfScreen() - (sizeOfControl / 2);
    }

    public static Point ClampToBounds(Point pointOfControl, Size sizeOfControl)
    {
        UIManager UI = UIManager.GetInstance();
        int offset = Constants.FormBounds;

        return new Point(
            Math.Clamp(pointOfControl.X, offset, UI.Resolution.Width - offset - sizeOfControl.Width),
            Math.Clamp(pointOfControl.Y, offset, UI.Resolution.Height - offset - sizeOfControl.Height)
        );
    }

    public static Bitmap GetImageFromDirection(string name, double x, double y)
    {
        return !Constants.EntityImages.TryGetValue(name, out var images)
            ? throw new NotImplementedException($"No images found for {name}")
            : Math.Abs(y) > Math.Abs(x)
                ? y < 0 ? images.Up : images.Down
                : x < 0 ? images.Left : images.Right;
    }

    private static void RotateImage(PictureBox box, double directionX, double directionY) // not used, but maybe could get implemented in the future
    {
        double angle = Math.Atan2(directionY, directionX) * (180 / Math.PI);

        box.Image = RotateImage(Assets.EnemyZombieRight, (float)angle);
    }

    private static Image RotateImage(Image img, float angle)
    {
        Bitmap rotatedBmp = new(img.Width, img.Height);
        rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

        using (Graphics g = Graphics.FromImage(rotatedBmp))
        {
            g.TranslateTransform((float)rotatedBmp.Width / 2, (float)rotatedBmp.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)rotatedBmp.Width / 2, -(float)rotatedBmp.Height / 2);

            g.DrawImage(img, new Point(0, 0));
        }

        return rotatedBmp;
    }

    private static readonly Dictionary<float, Image> rotatedZombieImages = new();

    private static void PreRenderZombieImages(Image originalImage)
    {
        for (float angle = 0; angle < 360; angle += 10) // Pre-render at 10-degree increments
        {
            rotatedZombieImages[angle] = RotateImage(originalImage, angle);
        }
    }

    private static void RotateZombieImage(PictureBox zombie, double directionX, double directionY)
    {
        double angle = Math.Atan2(directionY, directionX) * (180 / Math.PI);
        angle = (angle + 360) % 360;

        // Find the closest pre-rendered image
        float closestAngle = (float)(Math.Round((float)angle / 10) * 10); // Round to the nearest 10 degrees
        zombie.Image = rotatedZombieImages[closestAngle];
    }
}
