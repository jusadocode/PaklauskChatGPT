using Client.UI;
using Client.Utils;

namespace Client.Entities.Enemies;

public class Creeper : IEnemy
{
    public PictureBox? PictureBox { get; private set; }

    PictureBox IEnemy.Create()
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
                Tag = Constants.EnemyTag,
                Name = Constants.CreeperName,
                Image = Assets.ZombieUp,
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
