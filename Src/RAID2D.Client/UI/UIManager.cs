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

    private Label fpsLabel = new();
    private Label ammoLabel = new();
    private Label killsLabel = new();
    private Label cashLabel = new();
    private ProgressBar healthBar = new();
    public Size Resolution = new(1920, 1080);

    public void InitializeLabels(Label fps, Label ammo, Label kills, Label cash, ProgressBar health)
    {
        fpsLabel = fps;
        ammoLabel = ammo;
        killsLabel = kills;
        cashLabel = cash;
        healthBar = health;
    }

    public void InitializeResolution(Size resolution)
    {
        Resolution = resolution;
    }

    public void CreateDevUI(Player player, Action<uint>? onSpawnEntitiesButtonClick, Action<Button>? onButtonCreate)
    {
#if DEBUG
        Button addAmmoButton = CreateDevButton("Add 9999 Ammo", (s, e) => player.PickupAmmo(9999));
        Button addHealthButton = CreateDevButton("Add 9999 Health", (s, e) => player.SetMaxHealth(9999));
        Button spawnEntitiesButton = CreateDevButton("Spawn 10 Entities", (s, e) => onSpawnEntitiesButtonClick?.Invoke(10));

        onButtonCreate?.Invoke(addHealthButton);
        onButtonCreate?.Invoke(addAmmoButton);
        onButtonCreate?.Invoke(spawnEntitiesButton);

        addAmmoButton.Left = Resolution.Width - addAmmoButton.Width - 10;
        addHealthButton.Left = addAmmoButton.Left - addHealthButton.Width - 10;
        spawnEntitiesButton.Left = addHealthButton.Left - spawnEntitiesButton.Width - 10;
#endif
    }

    private Button CreateDevButton(string text, EventHandler? clickHandler)
    {
        var button = new Button
        {
            Text = text,
            Top = 10,
            TabStop = false,
            AutoSize = true,
        };

        button.Click += clickHandler;
        return button;
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
