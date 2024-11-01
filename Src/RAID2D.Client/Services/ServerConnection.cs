using Microsoft.AspNetCore.SignalR.Client;
using RAID2D.Shared;
using RAID2D.Shared.Models;

namespace RAID2D.Client.Services;

public class ServerConnection
{
    private HubConnection? connection;

    public ServerConnection() { }

    public async void InitializeConnection(string serverUrl)
    {
        try
        {
            connection = new HubConnectionBuilder()
                .WithUrl(serverUrl)
                .Build();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize connection: {ex.Message}");
            connection = null;
            return;
        }

        connection.Closed += async (error) =>
        {
            Console.WriteLine("Connection closed. Attempting to reconnect...");
            await Task.Delay(2000);
            await ConnectAsync();
        };

        connection.On<GameState>(SharedConstants.ReceiveGameStateUpdate, HandleReceivedGameState);

        await ConnectAsync();
    }

    public async Task ConnectAsync()
    {
        if (connection == null)
        {
            Console.WriteLine("Cannot connect to the server, connection is not initialized.");
            return;
        }

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the server successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the server: {ex.Message}");
        }
    }

    public async Task DisconnectAsync()
    {
        if (connection == null)
        {
            Console.WriteLine("Cannot disconnect from server, Connection is not initialized.");
            return;
        }

        try
        {
            await connection.StopAsync();
            Console.WriteLine("Disconnected from the server successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to disconnect from the server: {ex.Message}");
        }
    }

    public async Task SendGameStateAsync(GameState gameState)
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            Console.WriteLine("Cannot send game state, Connection is not established.");
            return;
        }
        try
        {
            await connection.InvokeAsync(SharedConstants.SendGameStateUpdate, gameState);
            Console.WriteLine("Sent game state to the server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send game state to the server: {ex.Message}");
        }
    }

    public void SendDevState()
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            Console.WriteLine("Cannot send dev state, Connection is not established.");
            return;
        }

        var gameState = new GameState
        {
            PlayerId = "dev",
            PositionX = 123,
            PositionY = 456,
        };

        SendGameStateAsync(gameState);
    }

    private void HandleReceivedGameState(GameState gameState)
    {
        Console.WriteLine("Received game state from the server.");
    }
}
