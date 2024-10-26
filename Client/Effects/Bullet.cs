using Client.Enums;
using Client.UI;

namespace Client.Effects;

public class Bullet
{
    private Direction direction = Direction.Up;
    private PictureBox bullet = new();
    private readonly Timer bulletTimer = new();
    private DateTime lastUpdateTime = DateTime.Now;

    public PictureBox Create(Direction direction, Point location)
    {
        this.direction = direction;
        this.bullet = new PictureBox
        {
            BackColor = Constants.BulletColor,
            Size = Constants.BulletSize,
            Tag = Constants.BulletTag,
            Location = location
        };

        bulletTimer.Interval = Constants.BulletSpeed;
        bulletTimer.Tick += BulletTimerEvent;
        bulletTimer.Start();

        return bullet;
    }

    private void BulletTimerEvent(object? sender, EventArgs e)
    {
        UIManager UI = UIManager.GetInstance();
        DateTime now = DateTime.Now;
        double deltaTime = (now - lastUpdateTime).TotalSeconds;
        lastUpdateTime = now;

        int deltaX = (int)(Constants.BulletSpeed * deltaTime * 100); // 100 is an arbitrary factor for scaling speed

        switch (this.direction)
        {
            case Direction.Left:
                bullet.Left -= deltaX;
                break;
            case Direction.Right:
                bullet.Left += deltaX;
                break;
            case Direction.Up:
                bullet.Top -= deltaX;
                break;
            case Direction.Down:
                bullet.Top += deltaX;
                break;
        }

        if (bullet.Left < Constants.FormBounds ||
            bullet.Left > UI.Resolution.Width - Constants.FormBounds ||
            bullet.Top < Constants.FormBounds ||
            bullet.Top > UI.Resolution.Height - Constants.FormBounds)
        {
            bulletTimer.Dispose();
            bullet.Dispose();
        }
    }
}
