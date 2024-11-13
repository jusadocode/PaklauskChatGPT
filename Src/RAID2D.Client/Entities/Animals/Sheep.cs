using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Animals;

public class Sheep : IAnimal
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IAnimal.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.AnimalTag,
            Name = Constants.AnimalSheepName,
            Image = Assets.AnimalSheep,
            Location = Rand.LocationOnScreen(Constants.AnimalSize),
            Size = Constants.AnimalSize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
