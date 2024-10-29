namespace RAID2D.Client.UI;

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

    private Button addAmmoButton = new();
    private Button addHealthButton = new();
    private Button spawnEntitiesButton = new();

    public void Initialize(Label fps, Label ammo, Label kills, Label cash, ProgressBar health, Size res)
    {
        fpsLabel = fps;
        ammoLabel = ammo;
        killsLabel = kills;
        cashLabel = cash;
        healthBar = health;
        Resolution = res;
    }

    public void CreateDevUI(Player player, Action<uint>? onSpawnEntitiesButtonClick, Action<Button>? onButtonCreate)
    {
#if DEBUG
        addAmmoButton = new Button
        {
            Text = "Add 9999 Ammo",
            Top = 10,
            TabStop = false,
            AutoSize = true,
        };

        addHealthButton = new Button
        {
            Text = "Add 9999 Health",
            Top = 10,
            TabStop = false,
            AutoSize = true,
        };

        spawnEntitiesButton = new Button
        {
            Text = "Spawn 10 Entities",
            Top = 10,
            TabStop = false,
            AutoSize = true,
        };

        addAmmoButton.Click += (s, e) => player.PickupAmmo(9999);
        addHealthButton.Click += (s, e) => player.SetMaxHealth(9999);
        spawnEntitiesButton.Click += (s, e) => onSpawnEntitiesButtonClick?.Invoke(10);

        onButtonCreate?.Invoke(addHealthButton);
        onButtonCreate?.Invoke(addAmmoButton);
        onButtonCreate?.Invoke(spawnEntitiesButton);

        addAmmoButton.Left = Resolution.Width - addAmmoButton.Width - 20;
        addHealthButton.Left = addAmmoButton.Left - addHealthButton.Width - 10;
        spawnEntitiesButton.Left = addHealthButton.Left - spawnEntitiesButton.Width - 10;
#endif
    }

    public void UpdateFPS(double fps)
    {
        fpsLabel.Text = $"FPS: {fps:F0}";
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
