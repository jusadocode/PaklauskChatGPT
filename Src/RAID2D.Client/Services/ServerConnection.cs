using Microsoft.AspNetCore.SignalR.Client;

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

        SetupMessageHandlers();
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

    private void SetupMessageHandlers()
    {
        if (connection == null)
        {
            Console.WriteLine("Connection is not initialized.");
            return;
        }

        connection.Closed += async (error) =>
        {
            Console.WriteLine("Connection closed. Attempting to reconnect...");
            await Task.Delay(2000);
            await ConnectAsync();
        };

        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"Received message from server: \"{user}: {message}\"");
        });
    }

    public async Task SendMessageAsync(string user, string message)
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            Console.WriteLine("Cannot send a message, Connection is not established.");
            return;
        }

        try
        {
            await connection.InvokeAsync("SendMessage", user, message);
            Console.WriteLine($"Sent message to the server: \"{user}: {message}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send message to the server: {ex.Message}");
        }
    }
}
