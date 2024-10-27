using Client.Utils;

namespace Client.Entities.Animals;

public class Boar : IAnimal
{
    public PictureBox? PictureBox { get; private set; }

    PictureBox IAnimal.Create()
    {
        if (PictureBox is not null)
        {
            return PictureBox;
        }
        else
        {
            PictureBox = new()
            {
                Tag = Constants.AnimalTag,
                Name = Constants.BoarName,
                Image = Assets.AnimalBoar,
                Location = Rand.LocationOnScreen(),
                Size = Constants.AnimalSize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }
}
