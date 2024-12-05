using RAID2D.Client.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
