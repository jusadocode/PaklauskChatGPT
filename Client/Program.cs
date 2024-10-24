global using Client.Properties;
global using Timer = System.Windows.Forms.Timer;

[STAThread]
static void Init()
{
    ApplicationConfiguration.Initialize();
    Client.Utils.ConsoleHelper.AllocConsole();
    Application.Run(new Client.MainForm());
}

Init();
