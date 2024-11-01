using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Drops.Builders;

public class DropItemBuilder
{
    private PictureBox pictureBox;

    public DropItemBuilder()
    {
        pictureBox = new PictureBox();
    }

    public DropItemBuilder SetTag(IDroppableItem drop)
    {
        pictureBox.Tag = GetTagForDropType(drop);
        return this; 
    }

    public DropItemBuilder SetName(string name)
    {
        pictureBox.Name = name;
        return this; 
    }

    public DropItemBuilder SetImage(Image image)
    {
        pictureBox.Image = image;
        return this; 
    }

    public DropItemBuilder SetLocation(Point location)
    {
        pictureBox.Location = location;
        return this; 
    }

    public DropItemBuilder SetSize(Size size)
    {
        pictureBox.Size = size;
        return this; 
    }

    public DropItemBuilder SetSizeMode(PictureBoxSizeMode sizeMode)
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
