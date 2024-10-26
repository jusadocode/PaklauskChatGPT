using Client.UI;
using Client.Utils;

namespace Client.Entities.Animals;

public class Goat : IAnimal
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
                Name = Constants.GoatName,
                Image = Assets.AnimalGoat,
                Location = Rand.LocationOnScreen(),
                Size = Constants.AnimalSize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }
}
