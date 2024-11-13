namespace RAID2D.Client.Entities.Enemies.Decorators;

public class ShieldedEnemyDecorator : EnemyDecorator
{
    public ShieldedEnemyDecorator(IEnemy baseEnemy) : base(baseEnemy)
    {
        UpdateAppearance();
    }

    public override void UpdateAppearance()
    {
        AddShieldIcon();
        this.PictureBox.Tag += Constants.ShieldedEnemyTag;
    }

    private void AddShieldIcon()
    {
        //PictureBox.Image = CombineImages(PictureBox.Image, Assets.Shield); // Use a proper shield icon
        this.PictureBox.BorderStyle = BorderStyle.FixedSingle; // Use a proper shield icon
    }

    /*
    private Image CombineImages(Image baseImage, Image overlay)
    {
        Bitmap combined = new(baseImage.Width, baseImage.Height);
        using (Graphics g = Graphics.FromImage(combined))
        {
            g.Clear(Color.Transparent);
            g.DrawImage(baseImage, 0, 0);
            g.DrawImage(overlay, baseImage.Width - overlay.Width, 0);
        }

        return combined;
    }
    */
}
