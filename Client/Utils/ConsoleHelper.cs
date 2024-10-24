using System.Runtime.InteropServices;

namespace Client.Utils;

public static class ConsoleHelper
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();
}
