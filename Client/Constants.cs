using Client.Drops;
using Client.Enums;

namespace Client;

public static class Constants
{
    public const int BulletSpeed = 10;
    public const int PlayerSpeed = 10;
    public const int ZombieSpeed = 3;

    public const uint FormBounds = 10; // Fake wall around the form for collision detection

    public const uint MiddleOfDay = 50;
    public const uint EndOfDay = 100;
    public const int DayTimeUpdateInterval = 100; // 1000 second irl = 1 hour in game

    public const uint EnemyDamage = 1;

    public const uint PlayerMaxHealth = 100;
    public const uint PlayerInitialAmmo = 10;
    public const uint PlayerLowHealthLimit = 20;

    public const uint EnemyFleeRadius = 500;
    public const uint AnimalFleeRadius = 300;

    public const PictureBoxSizeMode SizeMode = PictureBoxSizeMode.Zoom;

    public const string PlayerTag = "Player";
    public const string AnimalTag = "animal";
    public const string EnemyTag = "enemy";
    public const string MedicalTag = "medical";
    public const string BulletTag = "bullet";
    public const string HitmarkerTag = "Hitmarker";
    public const string AmmoDropTag = "AmmoDrop";

    public const string ZombieName = "zombie";
    public const string CreeperName = "creeper";
    public const string BoarName = "boar";
    public const string GoatName = "goat";

    public static readonly Size PlayerSize = new(80, 80);
    public static readonly Size EnemySize = new(75, 75);
    public static readonly Size AnimalSize = new(75, 75);
    public static readonly Size DropSize = new(50, 50);
    public static readonly Size HitmarkerSize = new(20, 20);
    public static readonly Size BulletSize = new(5, 5);

    public static readonly Color BulletColor = Color.White;
    public static readonly Color DayColor = Color.FromArgb(0xFF, 0x8F, 0xBC, 0x8F); // Light green
    public static readonly Color NightColor = Color.FromArgb(0xFF, 0x2F, 0x4F, 0x2F); // Dark green

    public static readonly Dictionary<Direction, HashSet<Keys>> MovementKeysMap = new()
    {
        { Direction.Up, new HashSet<Keys> { Keys.W, Keys.Up } },
        { Direction.Down, new HashSet<Keys> { Keys.S, Keys.Down } },
        { Direction.Left, new HashSet<Keys> { Keys.A, Keys.Left } },
        { Direction.Right, new HashSet<Keys> { Keys.D, Keys.Right } }
    };

    public static readonly Dictionary<string, ValuableItem> ValuableDrops = new()
    {
        { "gold", new ValuableItem("gold", 100, 10, Assets.DropGold) },
        { "rolex", new ValuableItem("rolex", 60, 20, Assets.DropRolex) },
        { "parcel_box", new ValuableItem("parcel_box", 20, 35, Assets.DropParcelBox) },
        { "cigarettes", new ValuableItem("cigarettes", 20, 35, Assets.DropCigarettes) }
    };

    public static readonly Dictionary<string, MedicalItem> MedicalDrops = new()
    {
        { "small_medkit", new MedicalItem("small_medkit", 20, 90, Assets.DropMedkitSmall) },
        { "large_medkit", new MedicalItem("large_medkit", 50, 90, Assets.DropMedkitLarge) },
        { "health_potion", new MedicalItem("health_potion", 100, 90, Assets.DropMedkitLarge) }
    };

    public static readonly Dictionary<string, AnimalDrop> AnimalDrops = new()
    {
        {"pork", new AnimalDrop("pork", 100, 10, Assets.DropMeat)},
    };

}
