using Microsoft.AspNetCore.SignalR;
using RAID2D.Server.Observers;
using RAID2D.Shared.Models;

namespace RAID2D.Server.Hubs;

public class GameHub : Hub
{
    private readonly GameStateSubject positionSubject;
    private readonly IHubContext<GameHub> hubContext;

    public GameHub(GameStateSubject positionSubject, IHubContext<GameHub> hubContext)
    {
        this.positionSubject = positionSubject;
        this.hubContext = hubContext;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");

        var observer = new GameStateObserver(Context.ConnectionId, hubContext);
        positionSubject.Attach(observer);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId} {(exception != null ? $"error: {exception.Message}" : "")}");

        var observer = positionSubject.Observers.FirstOrDefault(o => o is GameStateObserver obs && obs.ID == Context.ConnectionId);
        if (observer != null)
            positionSubject.Detach(observer);

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendGameStateUpdate(GameState gameState)
    {
        Console.WriteLine($"Received game state from Client.");

        await positionSubject.NotifyAll(gameState);
    }
}
