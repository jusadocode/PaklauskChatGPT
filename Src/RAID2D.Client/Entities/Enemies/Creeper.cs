using System.Windows.Forms;
using RAID2D.Client.Entities.Enemies.Prototype;
using RAID2D.Client.Utils;

namespace RAID2D.Client.Entities.Enemies;
public class Creeper : IEnemy, IPrototype
{
    public PictureBox PictureBox { get; private set; } = new();
    public Creeper()
    {
        Create();
    }

    public PictureBox Create()
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


    public IPrototype ShallowClone()
    {
        return (IPrototype)this.MemberwiseClone();
    }


    public IPrototype DeepClone()
    {

        var clonedCreeper = (Creeper)this.MemberwiseClone();


        clonedCreeper.PictureBox = new PictureBox
        {
            Tag = this.PictureBox.Tag,
            Name = this.PictureBox.Name,
            Image = (Image)this.PictureBox.Image.Clone(),
            Location = this.PictureBox.Location,
            Size = this.PictureBox.Size,
            SizeMode = this.PictureBox.SizeMode
        };

        return clonedCreeper;
    }
}

