global using Client.Properties;
global using Timer = System.Windows.Forms.Timer;

[STAThread]
static void Main()
{
    ApplicationConfiguration.Initialize();
    Client.Utils.ConsoleHelper.AllocConsole();
    Application.Run(new Client.MainForm());
}

Main();
