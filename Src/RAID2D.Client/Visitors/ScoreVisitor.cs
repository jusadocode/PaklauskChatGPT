using RAID2D.Client.Players;

namespace RAID2D.Client.Visitors;
public class ScoreVisitor : IPlayerVisitor
{
    public int HighestScore { get; private set; } = 0;
    public int HighestCash { get; private set; } = 0;

    public void Visit(ServerPlayer player)
    {
        if (player.MaxKills > HighestScore)
        {
            HighestScore = (int)player.MaxKills;
        }

        if (player.MaxCash > HighestCash)
        {
            HighestScore = (int)player.MaxKills;
        }
    }

    public void Visit(Player player)
    {
        if (player.Cash > HighestCash)
        {
            HighestCash = (int)player.Cash;
        }

        if (player.Kills > HighestScore)
        {
            HighestScore = (int)player.Kills;
        }
    }
}
