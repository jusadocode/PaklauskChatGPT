using RAID2D.Client.Entities;

namespace RAID2D.Client.Iterators;

public class EntityIterator : IIterator<IEntity>
{
    private readonly List<IEntity> _entities;
    private int _position = 0;

    public EntityIterator(List<IEntity> entities)
    {
        _entities = entities;
    }

    public bool HasNext() => _position < _entities.Count;

    public IEntity Next() => _entities[_position++];

    public IEntity Current => _entities[_position];
}
