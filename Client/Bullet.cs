using Timer = System.Windows.Forms.Timer;

namespace Client
{
    class Bullet
    {
        public Direction BulletDirection { get; set; }
        public int BulletLeft { get; set; }
        public int BulletTop { get; set; }

        private readonly int speed = 30;
        private readonly PictureBox bullet = new PictureBox();
        private readonly Timer bulletTimer = new Timer();

        int formWidth = 100; // Values upadate at makeBullet
        int formHeight = 100;

        private DateTime lastUpdateTime = DateTime.Now;

        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = BulletLeft;
            bullet.Top = BulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            formWidth = form.ClientSize.Width;
            formHeight = form.ClientSize.Height;

            bulletTimer.Interval = speed;
            bulletTimer.Tick += BulletTimerEvent;
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - lastUpdateTime).TotalSeconds;
            lastUpdateTime = now;

            int deltaX = (int)(speed * deltaTime * 100); // 100 is an arbitrary factor for scaling speed

            switch (BulletDirection)
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

            if (bullet.Left < 10 || bullet.Left > formWidth - 10 || bullet.Top < 10 || bullet.Top > formHeight - 10)
            {
                bulletTimer.Stop();
                Dispose();
            }
        }

        public void Dispose()
        {
            bulletTimer?.Dispose();
            bullet?.Dispose();
        }
    }
}
