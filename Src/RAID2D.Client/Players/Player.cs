using RAID2D.Client.Effects;
using RAID2D.Client.Mementos;
using RAID2D.Client.MovementStrategies;
using RAID2D.Client.UI;
using RAID2D.Client.Utils;
using RAID2D.Client.Visitors;
using RAID2D.Shared.Enums;

namespace RAID2D.Client.Players;

public class Player : IPlayerElement
{
    public int Health { get; private set; } = Constants.PlayerMaxHealth;
    public int MaxHealth { get; private set; } = Constants.PlayerMaxHealth;
    public int Speed { get; private set; } = Constants.PlayerSpeed;
    public uint Ammo { get; private set; } = Constants.PlayerInitialAmmo;
    public uint Cash { get; private set; } = 0;
    public uint Kills { get; private set; } = 0;
    public Direction Direction { get; set; } = Direction.Up;
    public PictureBox PictureBox { get; private set; } = new();

    public event Action? OnEmptyMagazine;
    public event Action? OnLowHealth;
    private IMovementStrategy? movementStrategy;

    private bool lowHealthTriggered = false;

    public PictureBox Create()
    {
        movementStrategy = new PlayerMovement(this, Speed);

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

    public PlayerMemento SaveState()
    {
        return new PlayerMemento(PictureBox.Location, Health, Ammo, Kills, Cash);
    }

    public void RestoreState(PlayerMemento memento)
    {
        PictureBox.Location = memento.Position;
        Health = memento.Health;
        Ammo = memento.Ammo;
        Kills = memento.Kills;

        GUI ui = GUI.GetInstance();
        ui.UpdateHealth(MaxHealth, Health);
        ui.UpdateAmmo(Ammo);
        ui.UpdateKills(Kills);
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

    public void RegisterKill(Point hitmarkLocation)
    {
        Kills++;
        Hitmarker hitmark = new();
        hitmark.Create(hitmarkLocation, this.RemoveControl);
        this.AddControl(hitmark.PictureBox);
        GUI.GetInstance().UpdateKills(Kills);
    }

    public void Move()
    {
        movementStrategy?.Move(this.PictureBox);
    }

    public void ShootBullet(Action<Bullet> onBulletCreated, Action<PictureBox> onBulletExpired)
    {
        if (Ammo == 0)
            return;

        Ammo--;
        GUI.GetInstance().UpdateAmmo(Ammo);

        if (Ammo == 0)
            OnEmptyMagazine?.Invoke();
        
        Bullet bullet = new();
        bullet.Create(Direction, this.PictureBox.Location + (this.PictureBox.Size / 2), onBulletExpired);
        onBulletCreated(bullet);
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

    public void AddControl(Control control)
    {
        if (this.PictureBox.Parent == null)
            return;

        this.PictureBox.Parent.Controls.Add(control);
        control.BringToFront();
        this.PictureBox.BringToFront();
    }

    public void RemoveControl(Control control)
    {
        if (this.PictureBox.Parent == null)
            return;

        this.PictureBox.Parent.Controls.Remove(control);
        control.Dispose();
    }

    public void Accept(IPlayerVisitor visitor)
    {
        visitor.Visit(this);
    }
}
