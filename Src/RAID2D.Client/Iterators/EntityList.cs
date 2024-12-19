using RAID2D.Client.Entities;

namespace RAID2D.Client.Iterators;

public class EntityList : IAggregate<IEntity>
{
    private readonly List<IEntity> _entities = [];

    public void Add(IEntity entity) => _entities.Add(entity);
    public void Remove(PictureBox box)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            if (_entities[i].PictureBox == box)
            {
                _entities.RemoveAt(i);
                break;
            }
        }
    }

    public IIterator<IEntity> GetIterator() => new EntityIterator(_entities);
}
