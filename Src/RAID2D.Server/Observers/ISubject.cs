namespace RAID2D.Server.Observers;

public interface ISubject
{
    List<IObserver> Observers { get; set; }

    void Attach(IObserver observer);
    void Detach(IObserver observer);
    Task NotifyAll();
}
