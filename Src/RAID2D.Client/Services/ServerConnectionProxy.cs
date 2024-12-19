using RAID2D.Shared.Models;

namespace RAID2D.Client.Services;

public class ServerConnectionProxy : IServerConnection
{
    private readonly ServerConnection realConnection;
    private bool isAuthorized = false;

    public ServerConnectionProxy()
    {
        realConnection = new ServerConnection();
    }

    public void Authorize(string token)
    {
        isAuthorized = token == "valid_token"; // Simplified for demo

        Console.WriteLine(isAuthorized ? "Proxy: Authorization successful." : "Proxy: Authorization failed.");
    }

    public bool IsConnected()
    {
        return realConnection.IsConnected();
    }

    public void Connect(string serverUrl)
    {
        if (!isAuthorized)
        {
            Console.WriteLine("Proxy: Access denied - Unauthorized connection attempt.");
            return;
        }

        Console.WriteLine("Proxy: Forwarding connection request...");
        realConnection.Connect(serverUrl);
    }

    public void SetCallbacks(Action<GameState> onGameStateReceive)
    {
        if (IsConnected())
        {
            throw new InvalidOperationException("Cannot set callbacks while connected to the server.");
        }

        Console.WriteLine("Proxy: Setting callbacks...");
        realConnection.SetCallbacks(onGameStateReceive);
    }

    public async Task DisconnectAsync()
    {
        if (!IsConnected())
        {
            Console.WriteLine("Proxy: Cannot disconnect; no active connection.");
            return;
        }

        Console.WriteLine("Proxy: Forwarding disconnect request...");
        await realConnection.DisconnectAsync();
    }

    public async Task SendGameStateAsync(GameState gameState)
    {
        if (!isAuthorized)
        {
            Console.WriteLine("Proxy: Access denied - Unauthorized send game state attempt.");
            return;
        }

        if (!IsConnected())
        {
            Console.WriteLine("Proxy: Cannot send game state; connection is not established.");
            return;
        }

        //Console.WriteLine("Proxy forwarding game state...");
        await realConnection.SendGameStateAsync(gameState);
    }

    public void ReceiveGameState(GameState gameState)
    {
        if (!isAuthorized)
        {
            Console.WriteLine("Proxy: Access denied - Unauthorized receive game state attempt.");
            return;
        }

        realConnection.ReceiveGameState(gameState);
    }
}
