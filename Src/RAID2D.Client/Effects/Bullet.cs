using RAID2D.Client.UI;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.Effects;

public class Bullet
{
    public PictureBox PictureBox { get; set; }

    public void Create(Direction direction, Point location, Action<PictureBox> onBulletExpired)
    {
        PictureBox = new()
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
                    PictureBox.Left -= deltaX;
                    break;
                case Direction.Right:
                    PictureBox.Left += deltaX;
                    break;
                case Direction.Up:
                    PictureBox.Top -= deltaX;
                    break;
                case Direction.Down:
                    PictureBox.Top += deltaX;
                    break;
            }

            if (PictureBox.Left < Constants.FormBounds ||
                PictureBox.Left > UI.Resolution.Width - Constants.FormBounds ||
                PictureBox.Top < Constants.FormBounds ||
                PictureBox.Top > UI.Resolution.Height - Constants.FormBounds)
            {
                onBulletExpired(PictureBox);

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        };
    }
}
