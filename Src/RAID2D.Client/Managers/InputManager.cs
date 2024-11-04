using RAID2D.Shared.Enums;
using System.Runtime.InteropServices;

namespace RAID2D.Client.Managers;

public static class InputManager
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(Keys key);

    public static bool IsKeyDown(Keys key)
    {
        return (GetAsyncKeyState(key) & 0x8000) != 0;
    }

    public static bool IsKeyDownOnce(Keys key)
    {
        return (GetAsyncKeyState(key) & 0x0001) != 0;
    }

    public static readonly Dictionary<Direction, HashSet<Keys>> MovementKeysMap = new()
    {
        { Direction.Up, new HashSet<Keys> { Keys.W, Keys.Up } },
        { Direction.Down, new HashSet<Keys> { Keys.S, Keys.Down } },
        { Direction.Left, new HashSet<Keys> { Keys.A, Keys.Left } },
        { Direction.Right, new HashSet<Keys> { Keys.D, Keys.Right } }
    };
}
