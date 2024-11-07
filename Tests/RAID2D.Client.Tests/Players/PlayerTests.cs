using RAID2D.Client.Players;
using RAID2D.Client.Utils;
using RAID2D.Shared.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Players;
public class PlayerTests
{
    private readonly Player _testClass;

    public PlayerTests()
    {
        _testClass = new Player();
    }

    [Theory]
    [InlineData(100, 20, 80)]
    [InlineData(50, 60, 0)]
    [InlineData(30, 0, 30)]
    public void TakeDamage_HealthDecreasesCorrectly(int initialHealth, uint damage, int expectedHealth)
    {

        var player = new Player();
        player.SetMaxHealth(initialHealth);

        player.TakeDamage(damage);

        Assert.Equal(expectedHealth, player.Health);
    }

    [Theory]
    [InlineData(50, 20, 70)]
    [InlineData(90, 30, 100)]
    [InlineData(100, 50, 100)]
    public void PickupHealable_HealthIncreasesCorrectly(int initialHealth, uint healthPickup, int expectedHealth)
    {

        var player = new Player();
        player.SetMaxHealth(100);
        player.TakeDamage((uint)(100 - initialHealth));

        player.PickupHealable(healthPickup);

        Assert.Equal(expectedHealth, player.Health);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(0, 10, 10)]
    [InlineData(5, 0, 5)]
    public void PickupAmmo_AmmoIncreasesCorrectly(uint initialAmmo, uint ammoPickup, uint expectedAmmo)
    {

        var player = new Player();
        player.PickupAmmo(initialAmmo);

        player.PickupAmmo(ammoPickup);

        Assert.Equal(expectedAmmo, player.Ammo - 10);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(5, 6)]
    public void RegisterKill_KillsIncrementCorrectly(uint initialKills, uint expectedKills)
    {

        var player = new Player();

        for (uint i = 0; i < initialKills; i++)
            player.RegisterKill(Point.Empty, _ => { }, _ => { });

        player.RegisterKill(Point.Empty, _ => { }, _ => { });

        Assert.Equal(expectedKills, player.Kills);
    }
    [Theory]
    [InlineData(10, true)]
    [InlineData(25, false)]
    public void IsLowHealth_ReturnsCorrectStatus(int health, bool expectedResult)
    {
        var player = new Player();
        player.SetMaxHealth(health);
        Assert.Equal(expectedResult, player.IsLowHealth());
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsDead_ReturnsCorrectStatus(int health, bool expectedResult)
    {
        var player = new Player();
        player.SetMaxHealth(health);
        Assert.Equal(expectedResult, player.IsDead());
    }
    [Fact]
    public void Respawn_ResetsPlayerAttributesToInitialValues()
    {
        var player = new Player();
        player.TakeDamage(50);
        player.PickupAmmo(10);
        player.PickupValuable(100);
        player.RegisterKill(Point.Empty, _ => { }, _ => { });
        player.Respawn();
        Assert.Equal(Constants.PlayerMaxHealth, player.Health);
        Assert.Equal(Constants.PlayerMaxHealth, player.MaxHealth);
        Assert.Equal(Constants.PlayerSpeed, player.Speed);
        Assert.Equal(Constants.PlayerInitialAmmo, player.Ammo);
        Assert.Equal(0u, player.Cash);
        Assert.Equal(0u, player.Kills);
        Assert.Equal(Constants.PlayerTag, player.PictureBox.Tag);
        Assert.Equal(Location.MiddleOfScreen(Constants.PlayerSize), player.PictureBox.Location);
    }

    [Fact]
    public void CanCallCreate()
    {
        // Act
        var result = _testClass.Create();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallRespawn()
    {
        // Act
        var result = _testClass.Respawn();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallTakeDamage()
    {
        // Arrange
        var damage = (uint)608193333;

        // Act
        _testClass.TakeDamage(damage);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallPickupHealable()
    {
        // Arrange
        var health = (uint)110157229;

        // Act
        _testClass.PickupHealable(health);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallSetMaxHealth()
    {
        // Arrange
        var maxHealth = 1841761992;

        // Act
        _testClass.SetMaxHealth(maxHealth);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallPickupValuable()
    {
        // Arrange
        var cash = (uint)915805411;

        // Act
        _testClass.PickupValuable(cash);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallPickupAmmo()
    {
        // Arrange
        var ammo = (uint)1703844346;

        // Act
        _testClass.PickupAmmo(ammo);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallRegisterKill()
    {
        // Arrange
        var hitmarkLocation = new Point();
        Action<PictureBox> onHitmarkerCreation = x => { };
        Action<PictureBox> onHitmarkerExpired = x => { };

        // Act
        _testClass.RegisterKill(hitmarkLocation, onHitmarkerCreation, onHitmarkerExpired);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallRegisterKillWithNullOnHitmarkerCreation()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.RegisterKill(new Point(), default(Action<PictureBox>), x => { }));
    }

    [Fact]
    public void CannotCallRegisterKillWithNullOnHitmarkerExpired()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.RegisterKill(new Point(), x => { }, default(Action<PictureBox>)));
    }

    [Fact]
    public void CanCallMove()
    {
        // Act
        _testClass.Move();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallShootBullet()
    {
        // Arrange
        Action<PictureBox> onBulletCreated = x => { };
        Action<PictureBox> onBulletExpired = x => { };

        // Act
        _testClass.ShootBullet(onBulletCreated, onBulletExpired);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallShootBulletWithNullOnBulletCreated()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.ShootBullet(default(Action<PictureBox>), x => { }));
    }

    [Fact]
    public void CannotCallShootBulletWithNullOnBulletExpired()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.ShootBullet(x => { }, default(Action<PictureBox>)));
    }

    [Fact]
    public void CanCallDistanceTo()
    {
        // Arrange
        var control = new Control();

        // Act
        var result = _testClass.DistanceTo(control);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallDistanceToWithNullControl()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.DistanceTo(default(Control)));
    }

    [Fact]
    public void CanCallIntersectsWith()
    {
        // Arrange
        var control = new Control();

        // Act
        var result = _testClass.IntersectsWith(control);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CannotCallIntersectsWithWithNullControl()
    {
        Assert.Throws<ArgumentNullException>(() => _testClass.IntersectsWith(default(Control)));
    }

    [Fact]
    public void CanCallIsDead()
    {
        // Act
        var result = _testClass.IsDead();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanCallIsLowHealth()
    {
        // Act
        var result = _testClass.IsLowHealth();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetHealth()
    {
        // Assert
        Assert.IsType<int>(_testClass.Health);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetMaxHealth()
    {
        // Assert
        Assert.IsType<int>(_testClass.MaxHealth);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetSpeed()
    {
        // Assert
        Assert.IsType<int>(_testClass.Speed);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetAmmo()
    {
        // Assert
        Assert.IsType<uint>(_testClass.Ammo);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetCash()
    {
        // Assert
        Assert.IsType<uint>(_testClass.Cash);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetKills()
    {
        // Assert
        Assert.IsType<uint>(_testClass.Kills);

        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanSetAndGetDirection()
    {
        // Arrange
        var testValue = Direction.Up;

        // Act
        _testClass.Direction = testValue;

        // Assert
        Assert.Equal(testValue, _testClass.Direction);
    }

    [Fact]
    public void CanGetPictureBox()
    {
        // Assert
        Assert.IsType<PictureBox>(_testClass.PictureBox);

        throw new NotImplementedException("Create or modify test");
    }
}