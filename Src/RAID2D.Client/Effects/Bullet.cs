using RAID2D.Client.UI;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.Effects;

public static class Bullet
{
    public static void Create(Direction direction, Point location, Action<PictureBox> onBulletCreated, Action<PictureBox> onBulletExpired)
    {
        PictureBox picture = new()
        {
            BackColor = Constants.BulletColor,
            Size = Constants.BulletSize,
            Tag = Constants.BulletTag,
            Location = location
        };

        DateTime lastUpdateTime = DateTime.Now;

        Timer? timer = new()
        {
            Enabled = true,
            Interval = Constants.BulletSpeed
        };
        timer.Tick += (s, e) =>
        {
            GUI UI = GUI.GetInstance();
            DateTime now = DateTime.Now;
            double deltaTime = (now - lastUpdateTime).TotalSeconds;
            lastUpdateTime = now;

            int deltaX = (int)(Constants.BulletSpeed * deltaTime * 100);

            switch (direction)
            {
                case Direction.Left:
                    picture.Left -= deltaX;
                    break;
                case Direction.Right:
                    picture.Left += deltaX;
                    break;
                case Direction.Up:
                    picture.Top -= deltaX;
                    break;
                case Direction.Down:
                    picture.Top += deltaX;
                    break;
            }

            if (picture.Left < Constants.FormBounds ||
                picture.Left > UI.Resolution.Width - Constants.FormBounds ||
                picture.Top < Constants.FormBounds ||
                picture.Top > UI.Resolution.Height - Constants.FormBounds)
            {
                onBulletExpired(picture);

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        };

        onBulletCreated(picture);
    }
}
