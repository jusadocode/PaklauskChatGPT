using Client.Utils;

namespace Client.Entities.Enemies;

public class Zombie : IEnemy
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IEnemy.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.EnemyTag,
            Name = Constants.ZombieName,
            Image = Assets.ZombieUp,
            Location = Rand.LocationOnScreen(Constants.EnemySize),
            Size = Constants.EnemySize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}

