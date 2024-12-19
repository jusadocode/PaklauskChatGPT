using RAID2D.Client.Effects;

namespace RAID2D.Client.Iterators;

public class BulletIterator : IIterator<Bullet>
{
    private readonly List<Bullet> _bullets;
    private int _position = 0;

    public BulletIterator(List<Bullet> bullets)
    {
        _bullets = bullets;
    }

    public bool HasNext() => _position < _bullets.Count;

    public Bullet Next() => _bullets[_position++];

    public Bullet Current => _bullets[_position];
}