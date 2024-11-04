using RAID2D.Shared.Enums;
using System.Drawing;

namespace RAID2D.Shared.Models;

public class GameState
{
    public string ConnectionID { get; set; } = "";
    public Point Location { get; set; }
    public Direction Direction { get; set; }

    public GameState(Point location, Direction direction)
    {
        this.Location = location;
        this.Direction = direction;
    }

    public override string ToString()
    {
        return $"ConnectionID={ConnectionID}, Location={Location}, Direction={Direction}";
    }
}
