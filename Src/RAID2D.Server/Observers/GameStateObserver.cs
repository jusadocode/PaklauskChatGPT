using Microsoft.AspNetCore.SignalR;
using RAID2D.Server.Hubs;
using RAID2D.Shared;
using RAID2D.Shared.Models;

namespace RAID2D.Server.Observers;

public class GameStateObserver : IObserver
{
    public string ConnectionID { get; private set; }

    private readonly IHubContext<GameHub> context;

    private readonly ISubject subject;

    public GameStateObserver(ISubject subject, string connectionID, IHubContext<GameHub> hubContext)
    {
        this.subject = subject;
        this.ConnectionID = connectionID;
        this.context = hubContext;
    }

    public async Task Update()
    {
        if (this.subject is GameStateSubject gameStateSubject)
        {
            GameState gameState = gameStateSubject.GetState();
            if (gameState.ConnectionID == this.ConnectionID)
                return;

            //Console.WriteLine($"Sending GameState:\"{gameState}\" to ClientID={this.ConnectionID}");

            await this.context.Clients.Client(this.ConnectionID).SendAsync(SharedConstants.ReceiveGameStateUpdate, gameState);
        }
        else
        {
            Console.WriteLine("Subject is not GameStateSubject");
        }
    }
}
