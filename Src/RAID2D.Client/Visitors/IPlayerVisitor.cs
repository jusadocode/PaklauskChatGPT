using RAID2D.Client.Players;

namespace RAID2D.Client.Visitors;
public interface IPlayerVisitor
{
    void Visit(ServerPlayer player);
    void Visit(Player player);
}
