using RAID2D.Client.Drops;
using RAID2D.Client.Effects;

namespace RAID2D.Client.Iterators;

public class BulletList : IAggregate<Bullet>
{
    private readonly LinkedList<Bullet> _bullets = [];

    public void Add(Bullet bullet) => _bullets.AddFirst(bullet);
    public void Remove(PictureBox box)
    {
        for (var node = _bullets.First; node != null;)
        {
            var next = node.Next;
            if (node.Value.PictureBox == box)
            {
                _bullets.Remove(node);
            }
            node = next;
        }
    }

    public IIterator<Bullet> GetIterator() => new BulletIterator(_bullets);
}
