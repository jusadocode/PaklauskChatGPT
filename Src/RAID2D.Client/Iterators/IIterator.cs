namespace RAID2D.Client.Iterators;

public interface IIterator<T>
{
    bool HasNext();
    T Next();
    T Current { get; }
}
