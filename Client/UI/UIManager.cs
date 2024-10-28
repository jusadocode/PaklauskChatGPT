namespace Client.UI;

public class UIManager
{
    private static UIManager? _instance = null; // Singleton instance
    private static readonly object _lock = new(); // Lock object for thread safety

    private UIManager() { } // Private constructor to prevent instantiation from outside

    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new UIManager();
            }
        }

        return _instance;
    }

    public Size Resolution { get; private set; }

    private Label fpsLabel = new();
    private Label ammoLabel = new();
    private Label killsLabel = new();
    private Label cashLabel = new();
    private ProgressBar healthBar = new();

    public void Initialize(Label fps, Label ammo, Label kills, Label cash, ProgressBar health, Size res)
    {
        fpsLabel = fps;
        ammoLabel = ammo;
        killsLabel = kills;
        cashLabel = cash;
        healthBar = health;
        Resolution = res;
    }

    public void UpdateFPS(double fps)
    {
        fpsLabel.Text = $"FPS: {(fps):F0}";
    }

    public void UpdateHealth(int maxHealth, int currentHealth)
    {
        healthBar.Minimum = 0;
        healthBar.Maximum = maxHealth;
        healthBar.Value = currentHealth;
    }

    public void UpdateAmmo(uint ammo)
    {
        ammoLabel.Text = $"Ammo: {ammo}";
    }

    public void UpdateKills(uint kills)
    {
        killsLabel.Text = $"Kills: {kills}";
    }

    public void UpdateCash(uint cash)
    {
        cashLabel.Text = $"Cash: {cash}$";
    }
}
