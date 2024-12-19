using RAID2D.Shared.Enums;
using System.Drawing;

namespace RAID2D.Shared.Models;

public class GameState
{
    public string ConnectionID { get; set; } = "";
    public Point Location { get; set; }
    public Direction Direction { get; set; }
    public bool IsDead { get; set; }
    public uint Kills { get; set; }
    public uint Cash { get; set; }

    public GameState(Point location, Direction direction, bool isDead, uint kills, uint cash)
    {
        this.Location = location;
        this.Direction = direction;
        this.IsDead = isDead;
        this.Kills = kills;
        this.Cash = cash;
    }

    public override string ToString()
    {
        return $"ConnectionID={ConnectionID}, Location={Location}, Direction={Direction} isDead={IsDead}, Kills={Kills}, Cash={Cash}";
    }
}
