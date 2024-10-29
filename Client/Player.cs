using Client.Effects;
using Client.Enums;
using Client.MovementStrategies;
using Client.UI;
using Client.Utils;

namespace Client;

public class Player(
    int health = Constants.PlayerMaxHealth,
    int maxHealth = Constants.PlayerMaxHealth,
    int speed = Constants.PlayerSpeed,
    uint ammo = Constants.PlayerInitialAmmo,
    uint cash = 0,
    uint kills = 0,
    Direction direction = Direction.Up)
{
    public int Health { get; private set; } = health;
    public int MaxHealth { get; private set; } = maxHealth;
    public int Speed { get; private set; } = speed;
    public uint Ammo { get; private set; } = ammo;
    public uint Cash { get; private set; } = cash;
    public uint Kills { get; private set; } = kills;
    public Direction Direction { get; set; } = direction;
    public PictureBox PictureBox { get; private set; } = new();

    public event Action? OnEmptyMagazine;
    public event Action? OnLowHealth;
    private IMovementStrategy? MovementStrategy;

    private bool lowHealthTriggered = false;

    public PictureBox Create()
    {
        MovementStrategy = new PlayerMovement(this, Speed);

        PictureBox = new()
        {
            Tag = Constants.PlayerTag,
            Name = Constants.PlayerName,
            Image = Assets.PlayerUp,
            Location = Util.MiddleOfScreen(Constants.PlayerSize),
            Size = Constants.PlayerSize,
            SizeMode = Constants.SizeMode,
        };

        Console.WriteLine($"Spawned Player at {PictureBox.Location}");

        return PictureBox;
    }

    public PictureBox Respawn()
    {
        PictureBox newPlayer = this.Create();

        Health = Constants.PlayerMaxHealth;
        MaxHealth = Constants.PlayerMaxHealth;
        Speed = Constants.PlayerSpeed;
        Ammo = Constants.PlayerInitialAmmo;
        Cash = 0;
        Kills = 0;

        UIManager UI = UIManager.GetInstance();

        UI.UpdateHealth(MaxHealth, Health);
        UI.UpdateCash(Cash);
        UI.UpdateAmmo(Ammo);
        UI.UpdateKills(Kills);

        return newPlayer;
    }

    public void TakeDamage(uint damage)
    {
        Health = (int)Math.Max(0, Health - damage);

        if (IsDead())
        {
            PictureBox.Image = Assets.PlayerDead;
        }
        else if (IsLowHealth() && !lowHealthTriggered)
        {
            lowHealthTriggered = true;
            OnLowHealth?.Invoke();
        }
        else if (!IsLowHealth())
        {
            lowHealthTriggered = false;
        }

        UIManager.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void PickupHealable(uint health)
    {
        Health += (int)health;
        Health = Health > MaxHealth ? MaxHealth : Health;
        UIManager.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void SetMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        UIManager.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void PickupValuable(uint cash)
    {
        Cash += cash;
        UIManager.GetInstance().UpdateCash(Cash);
    }

    public void PickupAmmo(uint ammo)
    {
        Ammo += ammo;
        UIManager.GetInstance().UpdateAmmo(Ammo);
    }

    public void RegisterKill(Point hitmarkLocation, Action<PictureBox> onHitmarkerCreation, Action<PictureBox> onHitmarkerExpired)
    {
        Kills++;
        Hitmarker hitmark = new();
        hitmark.CreatePictureBox(hitmarkLocation, onHitmarkerExpired);
        onHitmarkerCreation(hitmark.PictureBox);
        UIManager.GetInstance().UpdateKills(Kills);
    }

    public void Move()
    {
        MovementStrategy?.Move(this.PictureBox);
    }

    public void ShootBullet(Action<PictureBox> onBulletCreated, Action<PictureBox> onBulletExpired)
    {
        if (Ammo == 0)
            return;

        Ammo--;
        UIManager.GetInstance().UpdateAmmo(Ammo);

        if (Ammo == 0)
            OnEmptyMagazine?.Invoke();

        Bullet.Create(Direction, this.PictureBox.Location + (this.PictureBox.Size / 2), onBulletCreated, onBulletExpired);
    }

    public double DistanceTo(Control control)
    {
        return Util.EuclideanDistance(PictureBox.Location, control.Location);
    }

    public bool IntersectsWith(Control control)
    {
        return PictureBox.Bounds.IntersectsWith(control.Bounds);
    }

    public bool IsDead()
    {
        return Health == 0;
    }

    public bool IsLowHealth()
    {
        return Health is > 0 and <= (int)Constants.PlayerLowHealthLimit;
    }
}
