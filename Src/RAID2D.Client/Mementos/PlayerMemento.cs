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
    public uint Cash { get; }

    public PlayerMemento(Point position, int health, uint ammo, uint kills, uint cash)
    {
        this.Position = position;
        this.Health = health;
        this.Ammo = ammo;
        this.Kills = kills;
        this.Cash = cash;
    }
}
