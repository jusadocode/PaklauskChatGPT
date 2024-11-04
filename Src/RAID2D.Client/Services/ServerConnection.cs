using Microsoft.AspNetCore.SignalR.Client;
using RAID2D.Shared;
using RAID2D.Shared.Models;

namespace RAID2D.Client.Services;

public class ServerConnection
{
    private HubConnection? connection;
    private Action<GameState>? onGameStateReceive;

    public bool IsConnected()
    {
        return connection != null && connection.State == HubConnectionState.Connected;
    }

    public async void Connect(string serverUrl)
    {
        try
        {
            connection = new HubConnectionBuilder()
                .WithUrl(serverUrl)
                .Build();

            if (onGameStateReceive == null)
                throw new InvalidOperationException("onGameStateReceive callback is not set.");

            connection.On<GameState>(SharedConstants.ReceiveGameStateUpdate, ReceiveGameState);

            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize connection: {ex.Message}");
            connection = null;
            return;
        }

        Console.WriteLine("Connected to the server successfully.");
    }

    public void SetCallbacks(Action<GameState> onGameStateReceive)
    {
        if (IsConnected())
            throw new InvalidOperationException("Cannot set callbacks while connected to the server.");

        this.onGameStateReceive = onGameStateReceive;
    }

    public async Task DisconnectAsync()
    {
        if (!IsConnected())
        {
            Console.WriteLine("Cannot disconnect from server, Connection is not initialized.");
            return;
        }

        try
        {
            await connection!.StopAsync();
            Console.WriteLine("Disconnected from the server successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to disconnect from the server: {ex.Message}");
        }
    }

    public async Task SendGameStateAsync(GameState gameState)
    {
        if (!IsConnected())
        {
            Console.WriteLine("Cannot send game state, Connection is not established.");
            return;
        }

        try
        {
            //Console.WriteLine("Sent game state to the server.");

            await connection!.InvokeAsync(SharedConstants.SendGameStateUpdate, gameState);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send game state to the server: {ex.Message}");
        }
    }

    public void ReceiveGameState(GameState gameState)
    {
        try
        {
            onGameStateReceive?.Invoke(gameState);
        }
        catch (Exception e)
        {
            string message = $"Unhandled Exception:\n\n{e.Message}\n\nStack Trace:\n{e.StackTrace}";
            MessageBox.Show(message, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
