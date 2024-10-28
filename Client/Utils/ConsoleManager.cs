using System.Runtime.InteropServices;

namespace Client.Utils;

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
