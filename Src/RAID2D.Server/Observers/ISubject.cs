namespace RAID2D.Server.Observers;

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    Task NotifyAll();
}
