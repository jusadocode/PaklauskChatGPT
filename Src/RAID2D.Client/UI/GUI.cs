using RAID2D.Client.Players;
using RAID2D.Client.Services;
using RAID2D.Client.Utils;
using RAID2D.Shared.Models;

namespace RAID2D.Client.UI;

public class GUI
{
    public Size Resolution { get; private set; }

    private static GUI? _instance = null;
    private static readonly object _lock = new();

    private Label fpsLabel = new();
    private Label ammoLabel = new();
    private Label killsLabel = new();
    private Label cashLabel = new();
    private ProgressBar healthBar = new();
    private Panel pauseMenuPanel = new();
    public bool isSaveMode = true;
    private Panel scoreboardPanel = new();
    private GUI() { } // Private constructor to prevent instantiation from outside

    public static GUI GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new GUI();
            }
        }

        return _instance;
    }

    public void BindElements(Label fps, Label ammo, Label kills, Label cash, ProgressBar health)
    {
        fpsLabel = fps;
        ammoLabel = ammo;
        killsLabel = kills;
        cashLabel = cash;
        healthBar = health;
    }

    public void ToggleSaveLoadMode()
    {
        isSaveMode = !isSaveMode;
        UpdateCheckpointButtonText();
    }

    private void UpdateCheckpointButtonText()
    {
        foreach (Control control in pauseMenuPanel.Controls)
        {
            if (control is Button button)
            {
                if (button.Text.StartsWith("Save Check"))
                {
                    button.Text = "Load " + button.Text.Substring(5);
                }
                else if (button.Text.StartsWith("Load Check"))
                {
                    button.Text = "Save " + button.Text.Substring(5);
                }
            }
        }
    }

    public void CreatePauseMenu(Action<string>? onConnectClick, Action? onDisconnectClick, Action? onQuitClick, Action<int>? onCheckPointClick, Action<Panel>? onPanelCreate)
    {
        pauseMenuPanel = new Panel
        {
            Size = new Size(300, 500),
            Location = Location.MiddleOfScreen(new Size(300, 250)),
            BackColor = Color.Gray,
            Visible = false
        };

        Label pauseMenuLabel = new()
        {
            Text = "PAUSE MENU",
            Font = new Font("Arial", 16, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(pauseMenuPanel.Width / 4, 10)
        };

        TextBox serverLinkTextBox = new()
        {
            PlaceholderText = "Enter server link here",
            Text = $"{Constants.ServerUrl}",
            Width = 250,
            Location = new Point(25, 60)
        };

        Button connectButton = new()
        {
            Text = "Connect to Server",
            Width = 250,
            Location = new Point(25, 100)
        };
        connectButton.Click += (s, e) => onConnectClick?.Invoke(serverLinkTextBox.Text);

        Button disconnectButton = new()
        {
            Text = "Disconnect from Server",
            Width = 250,
            Location = new Point(25, 140)
        };
        disconnectButton.Click += (s, e) => onDisconnectClick?.Invoke();

        Button quitButton = new()
        {
            Text = "Quit Game",
            Width = 250,
            Location = new Point(25, 180)
        };
        quitButton.Click += (s, e) => onQuitClick?.Invoke();

        Button Checkpoint1 = new()
        {
            Text = isSaveMode ? "Save Checkpoint 1" : "Load Checkpoint 1",
            Width = 250,
            Location = new Point(25, 220)
        };
        Checkpoint1.Click += (s, e) => onCheckPointClick?.Invoke(0);
        Button Checkpoint2 = new()
        {
            Text = isSaveMode ? "Save Checkpoint 2" : "Load Checkpoint 2",
            Width = 250,
            Location = new Point(25, 260)
        };
        Checkpoint2.Click += (s, e) => onCheckPointClick?.Invoke(1);
        Button Checkpoint3 = new()
        {
            Text = isSaveMode ? "Save Checkpoint 3" : "Load Checkpoint 3",
            Width = 250,
            Location = new Point(25, 300)
        };
        Checkpoint3.Click += (s, e) => onCheckPointClick?.Invoke(2);

        Button toggleSaveLoadModeButton = new()
        {
            Text = "Toggle Save/Load Mode",
            Width = 250,
            Location = new Point(25, 340)
        };
        toggleSaveLoadModeButton.Click += (s, e) => ToggleSaveLoadMode();

        pauseMenuPanel.Controls.Add(pauseMenuLabel);
        pauseMenuPanel.Controls.Add(serverLinkTextBox);
        pauseMenuPanel.Controls.Add(connectButton);
        pauseMenuPanel.Controls.Add(disconnectButton);
        pauseMenuPanel.Controls.Add(quitButton);
        pauseMenuPanel.Controls.Add(Checkpoint1);
        pauseMenuPanel.Controls.Add(Checkpoint2);
        pauseMenuPanel.Controls.Add(Checkpoint3);
        pauseMenuPanel.Controls.Add(toggleSaveLoadModeButton);

        onPanelCreate?.Invoke(pauseMenuPanel);
    }

    public void CreateDevButtons(Player player, ServerConnection server, Action? onSpawnEntitiesClick, Action? onUndoClick, Action<Button>? onButtonCreate)
    {
#if DEBUG
        List<(string Text, EventHandler OnClick)> devButtons =
        [
            ("Add 9999 Ammo", (s, e) => player.PickupAmmo(9999)),
            ("Add 9999 Health", (s, e) => player.SetMaxHealth(9999)),
            ("Spawn 6 Entities", (s, e) => onSpawnEntitiesClick?.Invoke()),
            ("Send Player Data to Server", 
            async (s, e) => await server.SendGameStateAsync(
                new GameState(
                    player.PictureBox.Location, 
                    player.Direction, 
                    player.IsDead(), 
                    player.Kills, 
                    player.Cash)
                )
            ),            
            ("Undo daytime", (s, e) => onUndoClick?.Invoke())
        ];

        Button? previousButton = null;
        foreach (var (text, clickHandler) in devButtons)
        {
            var button = new Button
            {
                Text = text,
                Top = 10,
                TabStop = false,
                AutoSize = true,
            };
            button.Click += clickHandler;
            onButtonCreate?.Invoke(button);

            button.Left = previousButton != null
                ? previousButton.Left - button.Width - 10
                : Resolution.Width - button.Width - 10;

            previousButton = button;
        }
#endif
    }

    public void CreateScoreboard(Action<Panel>? onPanelCreate)
    {
        scoreboardPanel = new Panel
        {
            Size = new Size(230, 200), // Adjust size to fit additional labels
            Location = new Point(Resolution.Width - 250, 50),
            BackColor = Color.Transparent,
            Visible = true
        };

        Label scoreboardTitle = new()
        {
            Text = "Statistics",
            Font = new Font("Arial", 10, FontStyle.Bold),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(10, 10)
        };

        // Add labels for highest score and total score
        Label highestScoreLabel = new()
        {
            Text = "Highest Session Score: 0",
            Name = "HighestScoreLabel",
            Font = new Font("Arial", 10),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(10, 40)
        };

        Label totalScoreLabel = new()
        {
            Text = "Highest Session Cash: 0",
            Name = "HighestCashLabel",
            Font = new Font("Arial", 10),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(10, 60)
        };

        // Add controls to the scoreboard panel
        scoreboardPanel.Controls.Add(scoreboardTitle);
        scoreboardPanel.Controls.Add(highestScoreLabel);
        scoreboardPanel.Controls.Add(totalScoreLabel);

        onPanelCreate?.Invoke(scoreboardPanel);
    }

    public void AddPlayerKillsEntry(string playerName, uint newScore)
    {
        // Calculate the next Y position dynamically based on existing controls
        int nextY = scoreboardPanel.Controls.OfType<Label>()
                             .LastOrDefault()?.Bottom ?? 10; // Start at 10 if no labels exist

        Label newScoreLabel = new()
        {
            Text = $"{playerName}: {newScore}",
            Name = playerName,
            Font = new Font("Arial", 10),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(10, nextY + 5) // 5 pixels below the last label
        };

        scoreboardPanel.Controls.Add(newScoreLabel);
    }

    public void RemovePlayerScore(string playerName, uint newScore)
    {

        foreach (Control control in scoreboardPanel.Controls)
        {
            if (control is Label scoreLabel && scoreLabel.Name == playerName)
            {
                scoreboardPanel.Controls.Remove(scoreLabel);
                return;
            }
        }
    }

    public void UpdatePlayersKills(string playerName, uint newScore)
    {
        foreach (Control control in scoreboardPanel.Controls)
        {
            if (control is Label scoreLabel && scoreLabel.Name == playerName)
            {
                scoreLabel.Text = $"{playerName}: {newScore}";
                return;
            }
        }
    }

    public void UpdateHighestScore(int highestScore)
    {
        foreach (Control control in scoreboardPanel.Controls)
        {
            if (control is Label highestScoreLabel && highestScoreLabel.Name == "HighestScoreLabel")
            {
                highestScoreLabel.Text = $"Highest Score: {highestScore}";
                return;
            }
        }
    }

    public void UpdateHighestCash(int highestCash)
    {
        foreach (Control control in scoreboardPanel.Controls)
        {
            if (control is Label totalScoreLabel && totalScoreLabel.Name == "HighestCashLabel")
            {
                totalScoreLabel.Text = $"Highest Cash: {highestCash}";
                return;
            }
        }
    }


    public void SetScoreboardVisibility(bool isVisible)
    {
        scoreboardPanel.Visible = isVisible;
    }
    public void BindScoreboardToUI(Control parent)
    {
        parent.Controls.Add(scoreboardPanel);
        parent.Refresh();
    }

    public void SetResolution(Size resolution)
    {
        Resolution = resolution;
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

    public bool IsPaused()
    {
        return pauseMenuPanel.Visible;
    }

    public void SetPauseMenuVisibility(bool isVisible)
    {
        pauseMenuPanel.Visible = isVisible;
    }

    public void TogglePauseMenuVisibility()
    {
        pauseMenuPanel.BringToFront();
        pauseMenuPanel.Visible = !pauseMenuPanel.Visible;
    }
}
