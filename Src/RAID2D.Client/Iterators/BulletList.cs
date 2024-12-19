using RAID2D.Client.Drops;
using RAID2D.Client.Effects;

namespace RAID2D.Client.Iterators;

public class BulletList : IAggregate<Bullet>
{
    private readonly List<Bullet> _bullets = [];

    public void Add(Bullet bullet) => _bullets.Add(bullet);
    public void Remove(PictureBox box)
    {
        foreach (Bullet bullet in _bullets)
        {
            if (bullet.PictureBox == box)
                _bullets.Remove(bullet);

            break;
        }
    }

    public IIterator<Bullet> GetIterator() => new BulletIterator(_bullets);
}
