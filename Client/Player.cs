using Client.Enums;
using Client.UI;
using Client.Utils;

namespace Client;

public class Player(
    uint health = Constants.PlayerMaxHealth,
    uint maxHealth = Constants.PlayerMaxHealth,
    uint speed = Constants.PlayerSpeed,
    uint ammo = Constants.PlayerInitialAmmo,
    uint cash = 0,
    uint kills = 0,
    Direction direction = Direction.Up)
{
    public uint Health { get; private set; } = health;
    public uint MaxHealth { get; private set; } = maxHealth; // TODO: Implement this in calculations below (and also in UIManager)
    public uint Speed { get; private set; } = speed;
    public uint Cash { get; private set; } = cash;
    public uint Ammo { get; private set; } = ammo;
    public uint Kills { get; private set; } = kills;
    public Direction Direction { get; private set; } = direction;

    public PictureBox? PictureBox { get; private set; } = null;

    public event Action? OnDeath;
    public event Action? OnEmptyMagazine;
    public event Action? OnLowHealth;

    public bool IntersectsWith(Control control) => PictureBox?.Bounds.IntersectsWith(control.Bounds) ?? false;
    public bool IsDead() => Health == 0;

    public PictureBox Create()
    {
        if (PictureBox is not null)
        {
            return PictureBox;
        }
        else
        {
            PictureBox = new()
            {
                Tag = Constants.PlayerTag,
                Name = Constants.PlayerTag,
                Image = Assets.PlayerUp,
                Location = Util.MiddleOfScreen(Constants.PlayerSize),
                Size = Constants.PlayerSize,
                SizeMode = Constants.SizeMode,
            };

            Console.WriteLine($"Created Player at {PictureBox.Location}");

            return PictureBox;
        }
    }

    public void Respawn()
    {
        if (PictureBox is null)
            throw new InvalidOperationException("Player PictureBox is null!");

        this.PictureBox.Location = (Point)(UIManager.GetInstance().Resolution / 2);
        this.PictureBox.Image = Assets.PlayerUp;

        Health = 100;
        Cash = 0;
        Ammo = 10;
        Kills = 0;

        UIManager.GetInstance().UpdateHealth(Health);
        UIManager.GetInstance().UpdateCash(Cash);
        UIManager.GetInstance().UpdateAmmo(Ammo);
        UIManager.GetInstance().UpdateKills(Kills);
    }

    public void TakeDamage(uint damage)
    {
        if (PictureBox is null)
            throw new InvalidOperationException("Player PictureBox is null!");

        if (damage >= Health)
        {
            Health = 0;
            PictureBox.Image = Assets.PlayerDead;
            OnDeath?.Invoke();
        }
        else
        {
            Health -= damage;
        }

        if (Health == Constants.PlayerLowHealthLimit)
            OnLowHealth?.Invoke();

        UIManager.GetInstance().UpdateHealth(Health);
    }

    public void PickupHealable(uint health)
    {
        Health += health;
        Health = Health > 100 ? 100 : Health;
        UIManager.GetInstance().UpdateHealth(Health);
    }

    public void PicupValuable(uint cash)
    {
        Cash += cash;
        UIManager.GetInstance().UpdateCash(Cash);
    }

    public void PickupAmmo(uint ammo)
    {
        Ammo += ammo;
        UIManager.GetInstance().UpdateAmmo(Ammo);
    }

    public void GetKill()
    {
        Kills++;
        UIManager.GetInstance().UpdateKills(Kills);
    }

    public void Move(Keys key)
    {
        foreach (var (direction, keys) in Constants.MovementKeysMap)
        {
            if (keys.Contains(key))
            {
                Move(direction);
                break;
            }
        }
    }

    private void Move(Direction direction)
    {
        if (PictureBox is null)
            throw new InvalidOperationException("Player PictureBox is null!");

        Direction = direction;

        PictureBox.Image = direction switch
        {
            Direction.Up => Assets.PlayerUp,
            Direction.Down => Assets.PlayerDown,
            Direction.Left => Assets.PlayerLeft,
            Direction.Right => Assets.PlayerRight,
            _ => throw new NotImplementedException()
        };

        PictureBox.Location += direction switch
        {
            Direction.Up => new Size(0, -5),
            Direction.Down => new Size(0, 5),
            Direction.Left => new Size(-5, 0),
            Direction.Right => new Size(5, 0),
            _ => throw new NotImplementedException()
        };
    }

    public PictureBox ShootBullet()
    {
        if (PictureBox is null)
            throw new InvalidOperationException("Player PictureBox is null!");

        if (Ammo == 0)
            throw new InvalidOperationException("No ammo left!");

        Ammo--;
        UIManager.GetInstance().UpdateAmmo(Ammo);

        if (Ammo == 0)
            OnEmptyMagazine?.Invoke();

        return new Bullet().Create(Direction, this.PictureBox.Location + (this.PictureBox.Size / 2));
    }

    public double DistanceTo(Control control)
    {
        if (PictureBox is null)
            throw new InvalidOperationException("Player PictureBox is null!");

        return Util.EuclideanDistance(PictureBox.Location, control.Location);
    }
}
