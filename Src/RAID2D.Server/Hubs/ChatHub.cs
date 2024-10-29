using Microsoft.AspNetCore.SignalR;

namespace RAID2D.Server.Hubs;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        Console.WriteLine($"Client connected: {Context.ConnectionId}");

        await Clients.Caller.SendAsync("ReceiveMessage", "SERVER", "Client successfully connected");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId} {(exception != null ? $"error: {exception.Message}" : "")}");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string user, string message)
    {
        Console.WriteLine($"Received message from Client: \"{user}: {message}\"");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
