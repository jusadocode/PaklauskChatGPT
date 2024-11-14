namespace RAID2D.Client.Drops.Builders;

public class MedicalDropBuilder : IDropItemBuilder
{
    private readonly PictureBox pictureBox;
    private Timer pulseTimer;
    private int pulseRadius = 10;  // Initial pulse radius
    private readonly int maxRadius = 90;    // Maximum pulse radius

    public MedicalDropBuilder()
    {
        pictureBox = new PictureBox();
    }

    public IDropItemBuilder SetTag(IDroppableItem drop)
    {
        pictureBox.Tag = GetTagForDropType(drop);
        return this;
    }

    public IDropItemBuilder SetName(string name)
    {
        pictureBox.Name = name;
        return this;
    }

    public IDropItemBuilder SetImage(Image image)
    {
        pictureBox.Image = image;
        return this;
    }

    public IDropItemBuilder SetLocation(Point location)
    {
        pictureBox.Location = location;
        return this;
    }

    public IDropItemBuilder SetSize(Size size)
    {
        pictureBox.Size = size;
        return this;
    }

    public IDropItemBuilder SetSizeMode(PictureBoxSizeMode sizeMode)
    {
        pictureBox.SizeMode = sizeMode;
        return this;
    }

    private void ApplyPulsingAura()
    {

        pulseTimer = new Timer { Interval = 100 };
        pulseTimer.Tick += (sender, e) =>
        {
            if (pulseRadius >= maxRadius)
            {
                pulseRadius = 10;
            }
            else
            {
                pulseRadius += 5;
            }

            pictureBox.Invalidate();
        };

        pictureBox.Paint += (sender, e) =>
        {

            using Brush brush = new SolidBrush(Color.FromArgb(50, Color.Green));
            e.Graphics.FillEllipse(brush, (pictureBox.Width / 2) - (pulseRadius / 2), (pictureBox.Height / 2) - (pulseRadius / 2), pulseRadius, pulseRadius);
        };

        pulseTimer.Start();
    }

    public PictureBox Build()
    {
        ApplyPulsingAura();
        return pictureBox;
    }

    private string GetTagForDropType(IDroppableItem dropItem)
    {
        return dropItem switch
        {
            AmmoDrop => Constants.DropAmmoTag,
            AnimalDrop => Constants.DropAnimalTag,
            MedicalDrop => Constants.DropMedicalTag,
            ValuableDrop => Constants.DropValuableTag,
            _ => throw new ArgumentException("Unknown drop type")
        };
    }
}