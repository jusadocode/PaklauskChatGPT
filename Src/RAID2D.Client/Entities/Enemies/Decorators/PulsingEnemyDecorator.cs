namespace RAID2D.Client.Entities.Enemies.Decorators;

internal class PulsingEnemyDecorator : EnemyDecorator
{
    private int pulseSpeed = 1;
    private float currentIntensity = 0f;

    public PulsingEnemyDecorator(IEnemy enemy) : base(enemy)
    {
        Timer pulseTimer = new()
        {
            Enabled = true,
            Interval = 100
        };
        pulseTimer.Tick += (sender, e) => UpdatePulseEffect();
        this.PictureBox.Tag += Constants.PulsingEnemyTag;

        UpdateAppearance();
    }

    // Updates the appearance of the enemy, applying a pulsing effect
    public override void UpdateAppearance()
    {
        Bitmap originalImage = new(this.PictureBox.Image);
        Bitmap pulsingImage = new(originalImage.Width, originalImage.Height);

        using (Graphics g = Graphics.FromImage(pulsingImage))
        {
            g.DrawImage(originalImage, 0, 0);

            using Brush redBrush = new SolidBrush(Color.FromArgb((int)(currentIntensity * 255), Color.Red));
            g.FillRectangle(redBrush, 0, 0, pulsingImage.Width, pulsingImage.Height);
        }

        this.PictureBox.Image = pulsingImage;

    }

    private void UpdatePulseEffect()
    {
        currentIntensity += pulseSpeed * 0.05f;

        if (currentIntensity is >= 1f or <= 0f)
        {
            pulseSpeed = -pulseSpeed;
        }

        UpdateAppearance();
    }
}
