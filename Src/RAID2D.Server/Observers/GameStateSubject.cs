using RAID2D.Shared.Models;

namespace RAID2D.Server.Observers;

public class GameStateSubject : ISubject
{
    public List<IObserver> Observers { get; set; } = [];

    public void Attach(IObserver observer)
    {
        this.Observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this.Observers.Remove(observer);
    }

    public async Task NotifyAll(GameState gameState)
    {
        foreach (var observer in this.Observers)
        {
            await observer.Update(gameState);
        }
    }
}
