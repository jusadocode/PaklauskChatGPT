using RAID2D.Client.Effects;

namespace RAID2D.Client.Iterators;

public class BulletIterator : IIterator<Bullet>
{
    private LinkedListNode<Bullet>? _currentNode;

    public BulletIterator(LinkedList<Bullet> bullets)
    {
        _currentNode = bullets.First;
    }

    public bool HasNext() => _currentNode != null;

    public Bullet Next()
    {
        if (_currentNode == null)
            throw new InvalidOperationException("No more elements in the iterator.");

        var bullet = _currentNode.Value;
        _currentNode = _currentNode.Next;
        return bullet;
    }

    public Bullet Current
    {
        get
        {
            return _currentNode == null ? throw new InvalidOperationException("Iterator is not at a valid position.") : _currentNode.Value;
        }
    }
}