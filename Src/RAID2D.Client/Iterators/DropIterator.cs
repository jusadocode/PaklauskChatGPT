using RAID2D.Client.Drops;

namespace RAID2D.Client.Iterators;

public class DropIterator : IIterator<IDroppableItem>
{
    private readonly Dictionary<PictureBox, IDroppableItem> _drops;
    private readonly IEnumerator<KeyValuePair<PictureBox, IDroppableItem>> _enumerator;
    private bool _hasMoreElements;

    public DropIterator(Dictionary<PictureBox, IDroppableItem> drops)
    {
        _drops = drops;
        _enumerator = _drops.GetEnumerator();
        _hasMoreElements = _enumerator.MoveNext();
    }

    public bool HasNext() => _hasMoreElements;

    public IDroppableItem Next()
    {
        if (!_hasMoreElements)
        {
            throw new InvalidOperationException("No more elements in the iterator.");
        }

        var currentItem = _enumerator.Current.Value;
        _hasMoreElements = _enumerator.MoveNext();
        return currentItem;
    }

    public IDroppableItem Current
    {
        get
        {
            return !_hasMoreElements ? throw new InvalidOperationException("Iterator is not at a valid position.") : _enumerator.Current.Value;
        }
    }
}
