using Client.Entities.Animals;
using Client.UI;
using Client.Utils;

namespace Client.Drops;

public class AmmoDrop
{
    public PictureBox? PictureBox { get; private set; }

    public PictureBox Create()
    {
        if (PictureBox is not null)
        {
            return PictureBox;
        }
        else
        {
            PictureBox = new()
            {
                Tag = Constants.AmmoDropTag,
                Name = Constants.AmmoDropTag,
                Image = Assets.DropAmmo,
                Location = Rand.LocationOnScreen(),
                Size = Constants.DropSize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }

}
