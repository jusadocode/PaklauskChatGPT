using Client.Utils;

namespace Client.Entities.Enemies;

public class Creeper : IEnemy
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IEnemy.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.EnemyTag,
            Name = Constants.CreeperName,
            Image = Assets.ZombieUp,
            Location = Rand.LocationOnScreen(Constants.EnemySize),
            Size = Constants.EnemySize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
