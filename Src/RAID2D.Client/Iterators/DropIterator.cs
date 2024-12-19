using RAID2D.Client.Drops;

namespace RAID2D.Client.Iterators;

public class DropIterator : IIterator<IDroppableItem>
{
    private readonly List<IDroppableItem> _drops;
    private int _position = 0;

    public DropIterator(List<IDroppableItem> drops)
    {
        _drops = drops;
    }

    public bool HasNext() => _position < _drops.Count;

    public IDroppableItem Next() => _drops[_position++];

    public IDroppableItem Current => _drops[_position];
}
