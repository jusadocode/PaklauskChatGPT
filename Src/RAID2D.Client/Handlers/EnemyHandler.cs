using RAID2D.Client.Players;
using System;
using System.Windows.Forms;

namespace RAID2D.Client.Handlers;
public class EnemyHandler : InteractionHandler
{
    public override void Handle(PictureBox entity, Player player)
    {
        if (IsEnemy(entity))
        {
            player.TakeDamage(Constants.EnemyDamage);
        }
        else
        {
            NextHandler?.Handle(entity, player);
        }
    }
    private static bool IsEnemy(Control enemy) => enemy.Tag is string tag && (tag == Constants.EnemyTag || tag == Constants.PulsingEnemyTag || tag == Constants.ShieldedEnemyTag);
}