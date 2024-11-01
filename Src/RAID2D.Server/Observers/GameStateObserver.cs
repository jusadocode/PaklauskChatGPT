namespace RAID2D.Server.Observers;

using Microsoft.AspNetCore.SignalR;
using RAID2D.Server.Hubs;
using RAID2D.Shared;
using RAID2D.Shared.Models;

public class GameStateObserver : IObserver
{
    public string ID { get; private set; }
    public IHubContext<GameHub> HubContext { get; private set; }

    public GameStateObserver(string id, IHubContext<GameHub> hubContext)
    {
        this.ID = id;
        this.HubContext = hubContext;
    }

    public async Task Update(GameState gameState)
    {
        Console.WriteLine($"Observer {this.ID} received update");

        await this.HubContext.Clients.Client(this.ID).SendAsync(SharedConstants.ReceiveGameStateUpdate, gameState);
    }
}
