namespace RAID2D.Client.Drops.Builders;

public class AnimalDropBuilder : IDropItemBuilder
{
    private PictureBox pictureBox;

    public AnimalDropBuilder()
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

    public PictureBox Build()
    {
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
