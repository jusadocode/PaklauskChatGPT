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

    private Label? ammoLabel;
    private Label? killsLabel;
    private Label? cashLabel;
    private ProgressBar? healthBar;

    public void Initialize(Label ammo, Label kills, Label cash, ProgressBar health, Size res)
    {
        ammoLabel = ammo;
        killsLabel = kills;
        cashLabel = cash;
        healthBar = health;
        Resolution = res;
    }

    public void UpdateHealth(int health)
    {
        healthBar!.Value = Math.Clamp(health, 0, 100);
    }

    public void UpdateUI(int ammo, int kills, int cash)
    {
        ammoLabel!.Text = $"Ammo: {ammo}";
        killsLabel!.Text = $"Kills: {kills}";
        cashLabel!.Text = $"Cash: {cash}$";
    }
}
