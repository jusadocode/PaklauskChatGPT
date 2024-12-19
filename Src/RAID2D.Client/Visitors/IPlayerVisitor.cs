using RAID2D.Client.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.Visitors;
public interface IPlayerVisitor
{
    void Visit(ServerPlayer player);
    void Visit(Player player);
}
