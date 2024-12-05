using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.Mementos;
public class PlayerMemento
{
    public Point Position { get; }
    public int Health { get; }
    public uint Ammo { get; }
    public uint Kills { get; }

    public PlayerMemento(Point position, int health, uint ammo, uint kills)
    {
        Position = position;
        Health = health;
        Ammo = ammo;
        Kills = kills;
    }
}
