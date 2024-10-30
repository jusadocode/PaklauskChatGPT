using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Animals;

public class Boar : IAnimal
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IAnimal.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.AnimalTag,
            Name = Constants.AnimalBoarName,
            Image = Assets.AnimalBoar,
            Location = Rand.LocationOnScreen(Constants.AnimalSize),
            Size = Constants.AnimalSize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
