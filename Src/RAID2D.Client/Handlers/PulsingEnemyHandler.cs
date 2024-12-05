using RAID2D.Client.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.Handlers;
public class PulsingEnemyHandler : InteractionHandler
{
    public override void Handle(PictureBox entity, Player player)
    {
        if (IsPulsingEnemy(entity))
        {
            player.TakeDamage(Constants.PulsingEnemyDamage);
            RemoveControl(entity);
        }
        else
        {
            NextHandler?.Handle(entity, player);
        }
    }
    private static bool IsPulsingEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.PulsingEnemyTag);
    private static void RemoveControl(Control control)
    {
        var parent = control.Parent;
        if (parent != null)
        {
            parent.Controls.Remove(control);
        }
    }
}
