using RAID2D.Shared.Models;

namespace RAID2D.Server.Observers;

public interface IObserver
{
    Task Update(GameState gameState);
}
