namespace RAID2D.Client.Iterators;

public interface IAggregate<T>
{
    IIterator<T> GetIterator();
}