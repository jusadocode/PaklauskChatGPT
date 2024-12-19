using RAID2D.Client.Drops;
using RAID2D.Client.Entities;

namespace RAID2D.Client.Iterators;

public class DropList : IAggregate<IDroppableItem>
{
    private readonly List<IDroppableItem> _drops = [];

    public void Add(IDroppableItem drop) => _drops.Add(drop);
    public void Remove(PictureBox box)
    {
        foreach (IDroppableItem drop in _drops)
        {
            if (drop.PictureBox == box)
                _drops.Remove(drop);

            break;
        }
    }

    public IIterator<IDroppableItem> GetIterator() => new DropIterator(_drops);
}
