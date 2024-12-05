namespace RAID2D.Client.Drops.Builders;

public class ValuableDropBuilder : IDropItemBuilder
{
    private readonly PictureBox pictureBox;
    private Timer glowTimer;
    private int glowAlpha = 0;
    private bool increasingGlow = true;

    public ValuableDropBuilder()
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

    private void ApplyGlow()
    {

        glowTimer = new Timer { Interval = 50 };
        glowTimer.Tick += (sender, e) =>
        {
            if (increasingGlow)
            {
                glowAlpha += 5;
                if (glowAlpha >= 255)
                {
                    increasingGlow = false;
                }
            }
            else
            {
                glowAlpha -= 5;
                if (glowAlpha <= 0)
                {
                    increasingGlow = true;
                }
            }

            pictureBox.Invalidate();
        };

        pictureBox.Paint += (sender, e) =>
        {

            using Brush brush = new SolidBrush(Color.FromArgb(glowAlpha, Color.Yellow));
            e.Graphics.FillEllipse(brush, -5, -5, pictureBox.Width + 10, pictureBox.Height + 10);
        };

        glowTimer.Start();
    }

    public PictureBox Build()
    {
        ApplyGlow();

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

