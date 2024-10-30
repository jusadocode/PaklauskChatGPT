using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Enemies;

public class Creeper : IEnemy
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IEnemy.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.EnemyTag,
            Name = Constants.EnemyCreeperName,
            Image = Assets.EnemyCreeper,
            Location = Rand.LocationOnScreen(Constants.EnemySize),
            Size = Constants.EnemySize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
