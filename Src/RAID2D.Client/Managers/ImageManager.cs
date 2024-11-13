using RAID2D.Shared.Enums;

namespace RAID2D.Client.Managers;

public static class ImageManager
{
    public static Bitmap GetImageFromDirection(string name, double x, double y)
    {
        return !Constants.DirectionalImages.TryGetValue(name, out var images)
            ? throw new NotImplementedException($"No images found for {name}")
            : Math.Abs(y) > Math.Abs(x)
                ? y < 0 ? images.Up : images.Down
                : x < 0 ? images.Left : images.Right;
    }

    public static Bitmap GetImageFromDirection(string name, Direction direction)
    {
        return !Constants.DirectionalImages.TryGetValue(name, out var images)
            ? throw new NotImplementedException($"No images found for {name}")
            : direction switch
            {
                Direction.Up => images.Up,
                Direction.Down => images.Down,
                Direction.Left => images.Left,
                Direction.Right => images.Right,
                _ => throw new NotImplementedException($"No image found for {direction}")
            };
    }

    // Code below is not used, but could get implemented in the future if needed
    /*
    private static readonly Dictionary<float, Image> rotatedImages = [];
    private static void PreRenderImages(Image originalImage)
    {
        for (float angle = 0; angle < 360; angle += 10)
            rotatedImages[angle] = RotateImage(originalImage, angle);
    }
    private static void GetRotatedImage(PictureBox box, double directionX, double directionY)
    {
        double angle = Math.Atan2(directionY, directionX) * (180 / Math.PI);
        angle = (angle + 360) % 360;

        float closestAngle = (float)(Math.Round((float)angle / 10) * 10);
        box.Image = rotatedImages[closestAngle];
    }

    private static void RotateImage(PictureBox box, double directionX, double directionY)
    {
        double angle = Math.Atan2(directionY, directionX) * (180 / Math.PI);
        box.Image = RotateImage(Assets.EnemyZombieRight, (float)angle);
    }

    private static Bitmap RotateImage(Image image, float angle)
    {
        Bitmap rotatedBmp = new(image.Width, image.Height);
        rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics g = Graphics.FromImage(rotatedBmp))
        {
            g.TranslateTransform((float)rotatedBmp.Width / 2, (float)rotatedBmp.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)rotatedBmp.Width / 2, -(float)rotatedBmp.Height / 2);

            g.DrawImage(image, new Point(0, 0));
        }

        return rotatedBmp;
    }
    */
}
