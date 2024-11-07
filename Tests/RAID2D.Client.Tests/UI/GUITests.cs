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
        // Act
        var result = GUI.GetInstance();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallBindElements()
    {
        // Arrange
        var fps = new Label();
        var ammo = new Label();
        var kills = new Label();
        var cash = new Label();
        var health = new ProgressBar();

        // Act
        _testClass.BindElements(fps, ammo, kills, cash, health);

        // Assert
        throw new NotImplementedException("Create or modify test");
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
        // Arrange
        Action<string> onConnectClick = x => { };
        Action onDisconnectClick = () => { };
        Action onQuitClick = () => { };
        Action<Panel> onPanelCreate = x => { };

        // Act
        _testClass.CreatePauseMenu(onConnectClick, onDisconnectClick, onQuitClick, onPanelCreate);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallCreateDevButtons()
    {
        // Arrange
        var player = new Player(388690451, 1559467632, 1593578414, (uint)105457404, (uint)1894067367, (uint)1804847295, Direction.Down);
        var server = new ServerConnection();
        Action onSpawnEntitiesClick = () => { };
        Action onUndoClick = () => { };
        Action<Button> onButtonCreate = x => { };

        // Act
        _testClass.CreateDevButtons(player, server, onSpawnEntitiesClick, onUndoClick, onButtonCreate);

        // Assert
        throw new NotImplementedException("Create or modify test");
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
        // Arrange
        var resolution = new Size();

        // Act
        _testClass.SetResolution(resolution);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUpdateFPS()
    {
        // Arrange
        var fps = 1198792480.05;

        // Act
        _testClass.UpdateFPS(fps);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallUpdateHealth()
    {
        // Arrange
        var maxHealth = 1997177572;
        var currentHealth = 1958449232;

        // Act
        _testClass.UpdateHealth(maxHealth, currentHealth);

        // Assert
        throw new NotImplementedException("Create or modify test");
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
        // Arrange
        var isVisible = true;

        // Act
        _testClass.SetPauseMenuVisibility(isVisible);

        // Assert
        throw new NotImplementedException("Create or modify test");
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