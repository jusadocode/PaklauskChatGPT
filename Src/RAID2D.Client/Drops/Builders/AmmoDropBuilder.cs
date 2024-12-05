namespace RAID2D.Client.Drops.Builders;

public class AmmoDropBuilder : IDropItemBuilder
{
    private readonly PictureBox pictureBox;
    private Timer pulseTimer;
    private Timer rotateTimer;
    private float rotationAngle = 0f;  // For rotating effect
    private int pulseRadius = 10;
    private readonly int maxPulseRadius = 50;
    private bool expandingPulse = true;

    public AmmoDropBuilder()
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

    private void ApplySpecificBehavior()
    {
        // Pulse effect for the ammo drop
        pulseTimer = new Timer { Interval = 100 };  // Pulse effect every 100ms
        pulseTimer.Tick += (sender, e) =>
        {
            // Expanding and contracting pulse
            if (expandingPulse)
            {
                pulseRadius += 5;
                if (pulseRadius >= maxPulseRadius)
                    expandingPulse = false;
            }
            else
            {
                pulseRadius -= 5;
                if (pulseRadius <= 10)
                    expandingPulse = true;
            }
            pictureBox.Invalidate();  // Redraw to show the pulse
        };

        // Rotating effect for ammo
        rotateTimer = new Timer { Interval = 30 };  // Rotation effect every 30ms
        rotateTimer.Tick += (sender, e) =>
        {
            rotationAngle += 5f;  // Incremental rotation
            if (rotationAngle >= 360f)
                rotationAngle = 0f;  // Reset rotation angle to 0
            pictureBox.Invalidate();  // Redraw to show rotation
        };

        pulseTimer.Start();
        rotateTimer.Start();

        pictureBox.Paint += (sender, e) =>
        {
            // Draw a pulse effect
            using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Blue)))
            {
                e.Graphics.FillEllipse(brush, (pictureBox.Width / 2) - (pulseRadius / 2), (pictureBox.Height / 2) - (pulseRadius / 2), pulseRadius, pulseRadius);
            }

            // Draw the ammo image
            e.Graphics.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);  // Set origin to center
            e.Graphics.RotateTransform(rotationAngle);  // Apply rotation effect
            e.Graphics.DrawImage(pictureBox.Image, -pictureBox.Width / 2, -pictureBox.Height / 2);  // Draw the ammo image with rotation
            e.Graphics.ResetTransform();  // Reset transformations

            // Optional: Draw some sparks (small circles) around the ammo drop
            Random rand = new();
            for (int i = 0; i < 3; i++)  // 3 random sparks
            {
                float sparkX = rand.Next((pictureBox.Width / 2) - 15, (pictureBox.Width / 2) + 15);
                float sparkY = rand.Next((pictureBox.Height / 2) - 15, (pictureBox.Height / 2) + 15);
                using Brush sparkBrush = new SolidBrush(Color.FromArgb(255, rand.Next(150, 255), 0));  // Orange color
                e.Graphics.FillEllipse(sparkBrush, sparkX, sparkY, 4, 4);  // Small spark circles
            }
        };
    }

    public PictureBox Build()
    {
        ApplySpecificBehavior();
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
