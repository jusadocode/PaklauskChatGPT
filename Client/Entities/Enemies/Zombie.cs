using Client.UI;
using Client.Utils;

namespace Client.Entities.Enemies;

public class Zombie : IEnemy
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
            PictureBox = new()
            {
                Tag = Constants.EnemyTag,
                Name = Constants.ZombieName,
                Image = Assets.ZombieUp,
                Location = Rand.LocationOnScreen(),
                Size = Constants.EnemySize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }
}

