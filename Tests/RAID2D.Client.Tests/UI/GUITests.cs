using RAID2D.Client.Players;
using RAID2D.Client.Services;
using RAID2D.Client.UI;
using RAID2D.Shared.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.UI;
public class GUITests
{
    private readonly GUI _testClass;

    public GUITests()
    {
        _testClass = GUI.GetInstance();
    }

    [Fact]
    public void CanCallGetInstance()
    {
        var instance1 = GUI.GetInstance();
        var instance2 = GUI.GetInstance();

        Assert.NotNull(instance1);
        Assert.Same(instance1, instance2); // Check if both instances are the same (singleton)
    }

    [Fact]
    public void CanCallBindElements()
    {
        var fps = new Label();
        var ammo = new Label();
        var kills = new Label();
        var cash = new Label();
        var health = new ProgressBar();

        _testClass.BindElements(fps, ammo, kills, cash, health);

        Assert.NotNull(fps);
        Assert.NotNull(ammo);
        Assert.NotNull(kills);
        Assert.NotNull(cash);
        Assert.NotNull(health);
    }
    [Fact]
    public void CannotCallBindElementsWithNullFps()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.BindElements(default(Label), new Label(), new Label(), new Label(), new ProgressBar()));
    }

    [Fact]
    public void CannotCallBindElementsWithNullAmmo()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.BindElements(new Label(), default(Label), new Label(), new Label(), new ProgressBar()));
    }

    [Fact]
    public void CannotCallBindElementsWithNullKills()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.BindElements(new Label(), new Label(), default(Label), new Label(), new ProgressBar()));
    }

    [Fact]
    public void CannotCallBindElementsWithNullCash()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.BindElements(new Label(), new Label(), new Label(), default(Label), new ProgressBar()));
    }

    [Fact]
    public void CannotCallBindElementsWithNullHealth()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.BindElements(new Label(), new Label(), new Label(), new Label(), default(ProgressBar)));
    }

    [Fact]
    public void CanCallCreatePauseMenu()
    {
        bool panelCreated = false;
        Action<string> onConnectClick = x => { };
        Action onDisconnectClick = () => { };
        Action onQuitClick = () => { };
        Action<Panel> onPanelCreate = x => panelCreated = true;

        _testClass.CreatePauseMenu(onConnectClick, onDisconnectClick, onQuitClick, onPanelCreate);

        Assert.True(panelCreated);
        Assert.NotNull(_testClass.IsPaused()); 
    }

    [Fact]
    public void CanCallCreateDevButtons()
    {
        var player = new Player(388690451, 1559467632, 1593578414, (uint)105457404, (uint)1894067367, (uint)1804847295, Direction.Down);
        var server = new ServerConnection();
        bool buttonCreated = false;
        Action onSpawnEntitiesClick = () => { };
        Action onUndoClick = () => { };
        Action<Button> onButtonCreate = x => buttonCreated = true;

        _testClass.CreateDevButtons(player, server, onSpawnEntitiesClick, onUndoClick, onButtonCreate);

        Assert.True(buttonCreated);
    }

    [Fact]
    public void CannotCallCreateDevButtonsWithNullPlayer()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.CreateDevButtons(default(Player), new ServerConnection(), () => { }, () => { }, x => { }));
    }

    [Fact]
    public void CannotCallCreateDevButtonsWithNullServer()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.CreateDevButtons(new Player(298516336, 1157932802, 1122414845, (uint)1027502975, (uint)346378379, (uint)1838193063, Direction.Up), default(ServerConnection), () => { }, () => { }, x => { }));
    }

    [Fact]
    public void CanCallSetResolution()
    {
        var resolution = new Size(1920, 1080);

        _testClass.SetResolution(resolution);

        Assert.Equal(resolution, _testClass.Resolution);
    }

    [Fact]
    public void CanCallUpdateFPS()
    {
        var fps = 60.0;
        var fpsLabel = new Label();
        _testClass.BindElements(fpsLabel, new Label(), new Label(), new Label(), new ProgressBar());

        _testClass.UpdateFPS(fps);

        Assert.Equal("FPS: 60", fpsLabel.Text);
    }

    [Fact]
    public void CanCallUpdateHealth()
    {
        var maxHealth = 100;
        var currentHealth = 75;
        var healthBar = new ProgressBar();
        _testClass.BindElements(new Label(), new Label(), new Label(), new Label(), healthBar);

        _testClass.UpdateHealth(maxHealth, currentHealth);

        Assert.Equal(0, healthBar.Minimum);
        Assert.Equal(maxHealth, healthBar.Maximum);
        Assert.Equal(currentHealth, healthBar.Value);
    }

    [Fact]
    public void CanCallUpdateAmmo()
    {
        // Arrange
        var ammo = (uint)383425495;

        // Act
        _testClass.UpdateAmmo(ammo);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUpdateKills()
    {
        // Arrange
        var kills = (uint)911124970;

        // Act
        _testClass.UpdateKills(kills);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUpdateCash()
    {
        // Arrange
        var cash = (uint)1596881059;

        // Act
        _testClass.UpdateCash(cash);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallIsPaused()
    {
        // Act
        var result = _testClass.IsPaused();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetPauseMenuVisibility()
    {
        var isVisible = true;

        _testClass.SetPauseMenuVisibility(isVisible);

        Assert.Equal(isVisible, _testClass.IsPaused());
    }

    [Fact]
    public void CanCallTogglePauseMenuVisibility()
    {
        // Act
        _testClass.TogglePauseMenuVisibility();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetResolution()
    {
        // Assert
        Assert.IsType<Size>(_testClass.Resolution);

        throw new NotImplementedException("Create or modify test");
    }
}