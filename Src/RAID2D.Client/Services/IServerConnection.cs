using RAID2D.Shared.Models;

namespace RAID2D.Client.Services;

public interface IServerConnection
{
    bool IsConnected();
    void Connect(string serverUrl);
    void SetCallbacks(Action<GameState> onGameStateReceive);
    Task DisconnectAsync();
    Task SendGameStateAsync(GameState gameState);
}
