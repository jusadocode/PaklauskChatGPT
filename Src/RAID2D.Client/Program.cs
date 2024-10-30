global using RAID2D.Client.Properties;
global using Timer = System.Windows.Forms.Timer;
using RAID2D.Client.Managers;

[STAThread]
static void Main()
{
    ApplicationConfiguration.Initialize();
    ConsoleManager.SpawnConsole();
    Application.Run(new Client.MainForm());
}

Main();
