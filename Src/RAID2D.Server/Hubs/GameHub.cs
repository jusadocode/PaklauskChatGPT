using Microsoft.AspNetCore.SignalR;
using RAID2D.Server.Observers;
using RAID2D.Shared.Models;

namespace RAID2D.Server.Hubs;

public class GameHub : Hub
{
    private readonly GameStateSubject gameStateSubject;
    private readonly IHubContext<GameHub> hubContext;

    public GameHub(GameStateSubject gameStateSubject, IHubContext<GameHub> hubContext)
    {
        this.gameStateSubject = gameStateSubject;
        this.hubContext = hubContext;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");

        var observer = new GameStateObserver(gameStateSubject, Context.ConnectionId, hubContext);
        gameStateSubject.Attach(observer);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId} {(exception != null ? $"error: {exception.Message}" : "")}");

        var observer = gameStateSubject.Observers.FirstOrDefault(o => o is GameStateObserver obs && obs.ConnectionID == Context.ConnectionId);
        if (observer != null)
            gameStateSubject.Detach(observer);

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendGameStateUpdate(GameState gameState)
    {
        //Console.WriteLine($"Received GameState:\"{gameState}\" from ClientID={Context.ConnectionId}.");

        gameState.ConnectionID = Context.ConnectionId;
        gameStateSubject.SetState(gameState);

        await gameStateSubject.NotifyAll();
    }
}
