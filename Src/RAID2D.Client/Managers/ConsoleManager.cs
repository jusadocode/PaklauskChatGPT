using System.Runtime.InteropServices;

namespace RAID2D.Client.Managers;

public static class ConsoleManager
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();

    public static void SpawnConsole()
    {
#if DEBUG
        AllocConsole();
#endif
    }
}
