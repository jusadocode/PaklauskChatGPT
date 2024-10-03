using Timer = System.Windows.Forms.Timer;

namespace Client
{
    class Bullet
    {
        public string direction;
        public int bulletLeft;
        public int bulletTop;

        private int speed = 30;
        private PictureBox bullet = new PictureBox();
        private Timer bulletTimer = new Timer();


        int formWidth = 100; // Values upadate at makeBullet
        int formHeight = 100;

        private DateTime lastUpdateTime = DateTime.Now;



        public void MakeBullet(Form form)
        {

            bullet.BackColor = Color.White;
            bullet.Size = new Size(5,5);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            formWidth = form.ClientSize.Width;
            formHeight = form.ClientSize.Height;

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();

        }

        private void BulletTimerEvent(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            double deltaTime = (now - lastUpdateTime).TotalSeconds; 
            lastUpdateTime = now;

            int deltaX = (int)(speed * deltaTime * 100);  // 100 is an arbitrary factor for scaling speed


            if (direction == "left")
            {
                bullet.Left -= deltaX;
            }

            if (direction == "right")
            {
                bullet.Left += deltaX;
            }

            if (direction == "up")
            {
                bullet.Top -= deltaX;
            }

            if (direction == "down")
            {
                bullet.Top += deltaX;
            }


            if (bullet.Left < 10 || bullet.Left > formWidth - 10 || bullet.Top < 10 || bullet.Top > formHeight - 10)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }



        }



    }
}
