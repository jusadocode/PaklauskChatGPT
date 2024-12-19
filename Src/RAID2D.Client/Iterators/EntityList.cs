using RAID2D.Client.Entities;

namespace RAID2D.Client.Iterators;

public class EntityList : IAggregate<IEntity>
{
    private readonly List<IEntity> _entities = [];

    public void Add(IEntity entity) => _entities.Add(entity);
    public void Remove(PictureBox box) 
    {
        foreach (IEntity entity in _entities)
        {
            if (entity.PictureBox ==  box)
                _entities.Remove(entity);

            break;
        }
    }

    public IIterator<IEntity> GetIterator() => new EntityIterator(_entities);
}
