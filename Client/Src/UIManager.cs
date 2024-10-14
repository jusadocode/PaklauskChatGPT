using System;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace Client
{
    public sealed class UIManager
    {
        // Singleton instance
        private static readonly UIManager instance = new UIManager();

        // Static constructor to initialize the singleton instance
        static UIManager() { }

        // Private constructor to prevent instantiation from outside
        private UIManager() { }

        // Public method to access the singleton instance
        public static UIManager Instance => instance;

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
           //healthBar.Value = Math.Clamp(health, 0, 100);
           healthBar.Value = 50;
        }

        public void UpdateUI(int ammo, int score, int value)
        {
            ammoLabel.Text = "Ammo: " + ammo;
            scoreLabel.Text = "Kills: " + score;
            valueLabel.Text = "Value: " + value + "$";
        }
    }
}
