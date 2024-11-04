using RAID2D.Client.Managers;

namespace RAID2D.Client.Drops;

public class AnimalDrop : IDroppableItem
{
    public Point Location { get; private set; }
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public Size Size => Constants.DropSize;
    public string AnimalName { get; private set; }

    public AnimalDrop(Point location, string animalName)
    {
        Location = location;
        AnimalName = animalName;
        var data = DropManager.GetRandomAnimalDropDataByAnimalName(animalName);
        Name = data.Name;
        Image = data.Image;
    }
}
