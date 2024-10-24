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
            UIManager UI = UIManager.GetInstance();

            PictureBox = new()
            {
                Tag = Constants.AnimalTag,
                Name = Constants.GoatName,
                Image = Assets.AnimalGoat,
                Location = new(
                    Rand.Next(0 + Constants.Margin, UI.Resolution.Width - Constants.Margin),
                    Rand.Next(0 + Constants.Margin, UI.Resolution.Height - Constants.Margin)
                ),
                Size = Constants.EntitySize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }
}
