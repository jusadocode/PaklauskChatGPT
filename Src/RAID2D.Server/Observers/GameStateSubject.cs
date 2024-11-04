using RAID2D.Shared.Models;

namespace RAID2D.Server.Observers;

public class GameStateSubject : ISubject
{
    public List<IObserver> Observers { get; set; } = [];

    private GameState? gameState;

    public void Attach(IObserver observer)
    {
        this.Observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this.Observers.Remove(observer);
    }

    public async Task NotifyAll()
    {
        if (this.gameState == null)
            throw new InvalidOperationException("GameState is null");

        foreach (var observer in this.Observers)
        {
            await observer.Update();
        }
    }

    public void SetState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public GameState GetState()
    {
        return this.gameState ?? throw new InvalidOperationException("GameState is null");
    }
}
