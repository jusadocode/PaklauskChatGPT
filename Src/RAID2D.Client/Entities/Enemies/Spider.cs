using RAID2D.Client.Entities.Enemies.Prototype;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Enemies;

public class Spider : IEnemy, IPrototype
{
    public PictureBox PictureBox { get; private set; } = new();

    public PictureBox Create()
    {
        PictureBox = new()
        {
            Tag = Constants.EnemyTag,
            Name = Constants.EnemySpiderName,
            Image = Assets.EnemySpider,
            Location = Rand.LocationOnScreen(Constants.EnemySize),
            Size = Constants.EnemySize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }

    public IPrototype ShallowClone()
    {
        return (IPrototype)this.MemberwiseClone();
    }

    public IPrototype DeepClone()
    {
        var cloned = (Spider)this.MemberwiseClone();

        cloned.PictureBox = new PictureBox
        {
            Tag = this.PictureBox.Tag,
            Name = this.PictureBox.Name,
            Image = (Image)this.PictureBox.Image.Clone(),
            Location = this.PictureBox.Location,
            Size = this.PictureBox.Size,
            SizeMode = this.PictureBox.SizeMode
        };

        return cloned;
    }
}

