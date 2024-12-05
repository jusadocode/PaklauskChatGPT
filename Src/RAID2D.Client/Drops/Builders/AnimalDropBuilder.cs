namespace RAID2D.Client.Drops.Builders;

public class AnimalDropBuilder : IDropItemBuilder
{
    private readonly PictureBox pictureBox;
    private Timer bounceTimer;
    private bool isBouncingUp = true;
    private readonly Random randomMovementTimer;
    private Timer movementTimer;

    public AnimalDropBuilder()
    {
        pictureBox = new PictureBox();
        randomMovementTimer = new Random();
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

    private void ApplyBounce()
    {
        bounceTimer = new Timer { Interval = 300 };
        bounceTimer.Tick += (sender, e) =>
        {
            if (isBouncingUp)
            {
                pictureBox.Top -= 5;
            }
            else
            {
                pictureBox.Top += 5;
            }

            isBouncingUp = !isBouncingUp;
        };
        bounceTimer.Start();

        movementTimer = new Timer { Interval = 500 };
        movementTimer.Tick += (sender, e) =>
        {
            int direction = randomMovementTimer.Next(1, 5);
            switch (direction)
            {
                case 1:
                    pictureBox.Left += 5;
                    break;
                case 2:
                    pictureBox.Left -= 5;
                    break;
                case 3:
                    pictureBox.Top += 5;
                    break;
                case 4:
                    pictureBox.Top -= 5;
                    break;
            }
        };
        movementTimer.Start();
    }

    public PictureBox Build()
    {
        ApplyBounce();
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

