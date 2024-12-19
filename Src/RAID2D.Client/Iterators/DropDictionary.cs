using RAID2D.Client.Drops;

namespace RAID2D.Client.Iterators;

public class DropDictionary : IAggregate<IDroppableItem>
{
    private readonly Dictionary<PictureBox, IDroppableItem> _drops = [];

    public void Add(IDroppableItem drop)
    {
        if (!_drops.TryAdd(drop.PictureBox, drop))
        {
            throw new InvalidOperationException("The key already exists in the dictionary.");
        }
    }

    public void Remove(PictureBox box) => _drops.Remove(box);

    public IIterator<IDroppableItem> GetIterator() => new DropIterator(_drops);
}
