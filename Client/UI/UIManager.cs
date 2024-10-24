namespace Client.UI;

public sealed class UIManager
{
    // Singleton instance
    private static readonly UIManager instance = new();

    private UIManager() { }

    // Public method to access the singleton instance
    public static UIManager Instance
    {
        get { return instance; }
    }

    // UI Elements references
    private Label ammoLabel;
    private Label scoreLabel;
    private Label valueLabel;
    private ProgressBar healthBar;

    public void Initialize(Label ammo, Label score, Label value, ProgressBar health)
    {
        ammoLabel = ammo;
        scoreLabel = score;
        valueLabel = value;
        healthBar = health;
    }

    public void UpdateHealth(int health)
    {
        healthBar.Value = Math.Clamp(health, 0, 100);
    }

    public void UpdateUI(int ammo, int score, int value)
    {
        ammoLabel.Text = "Ammo: " + ammo;
        scoreLabel.Text = "Kills: " + score;
        valueLabel.Text = "Value: " + value + "$";
    }
}
