using RAID2D.Client.Effects;
using RAID2D.Client.MovementStrategies;
using RAID2D.Client.UI;
using RAID2D.Client.Utils;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.Players;

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
            Location = Location.MiddleOfScreen(Constants.PlayerSize),
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

        GUI UI = GUI.GetInstance();

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
            PictureBox.Image = Assets.PlayerDead;
        else if (IsLowHealth() && !lowHealthTriggered)
        {
            lowHealthTriggered = true;
            OnLowHealth?.Invoke();
        }
        else if (!IsLowHealth())
        {
            lowHealthTriggered = false;
        }

        GUI.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void PickupHealable(uint health)
    {
        Health += (int)health;
        Health = Health > MaxHealth ? MaxHealth : Health;
        GUI.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void SetMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        GUI.GetInstance().UpdateHealth(MaxHealth, Health);
    }

    public void PickupValuable(uint cash)
    {
        Cash += cash;
        GUI.GetInstance().UpdateCash(Cash);
    }

    public void PickupAmmo(uint ammo)
    {
        Ammo += ammo;
        GUI.GetInstance().UpdateAmmo(Ammo);
    }

    public void RegisterKill(Point hitmarkLocation, Action<PictureBox> onHitmarkerCreation, Action<PictureBox> onHitmarkerExpired)
    {
        Kills++;
        Hitmarker hitmark = new();
        hitmark.CreatePictureBox(hitmarkLocation, onHitmarkerExpired);
        onHitmarkerCreation(hitmark.PictureBox);
        GUI.GetInstance().UpdateKills(Kills);
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
        GUI.GetInstance().UpdateAmmo(Ammo);

        if (Ammo == 0)
            OnEmptyMagazine?.Invoke();

        Bullet.Create(Direction, this.PictureBox.Location + (this.PictureBox.Size / 2), onBulletCreated, onBulletExpired);
    }

    public double DistanceTo(Control control)
    {
        return Location.Distance(PictureBox.Location, control.Location);
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
