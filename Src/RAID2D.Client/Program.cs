global using RAID2D.Client.Properties;
global using Timer = System.Windows.Forms.Timer;

[STAThread]
static void Main()
{
    ApplicationConfiguration.Initialize();
    RAID2D.Client.Utils.ConsoleManager.SpawnConsole();
    Application.Run(new Client.MainForm());
}

Main();
