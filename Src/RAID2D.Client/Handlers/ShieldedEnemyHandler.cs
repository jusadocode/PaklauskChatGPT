using RAID2D.Client.Players;

namespace RAID2D.Client.Handlers;

public class ShieldedEnemyHandler : InteractionHandler
{
    public override void Handle(PictureBox entity, Player player)
    {
        if (IsShieldedEnemy(entity))
        {
            player.TakeDamage(Constants.EnemyDamage);
        }
        else
        {
            NextHandler?.Handle(entity, player);
        }
    }
    private static bool IsShieldedEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.ShieldedEnemyTag);
}
