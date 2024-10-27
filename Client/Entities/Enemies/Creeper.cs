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
            PictureBox = new()
            {
                Tag = Constants.EnemyTag,
                Name = Constants.CreeperName,
                Image = Assets.ZombieUp,
                Location = Rand.LocationOnScreen(),
                Size = Constants.EnemySize,
                SizeMode = Constants.SizeMode,
            };

            return PictureBox;
        }
    }
}
