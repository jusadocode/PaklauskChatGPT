namespace RAID2D.Client.Utils;

public static class Constants
{
    public const int BulletSpeed = 10;
    public const int PlayerSpeed = 7;
    public const int EnemySpeed = 4;
    public const int AnimalSpeed = 4;

    public const int FormBounds = 35; // Fake wall around the form for collision detection

    public const uint MiddleOfDay = 12;
    public const uint EndOfDay = 24;

    public const int GameTimerInterval = 20; // milliseconds
    public const int DayTimeUpdateInterval = 1000;
    public const int HitmarkDuration = 200;

    public const uint EnemyDamage = 1;

    public const int PlayerMaxHealth = 100;
    public const uint PlayerInitialAmmo = 10;
    public const uint PlayerLowHealthLimit = 20;

    public const uint EnemyFleeRadius = 500;
    public const uint AnimalFleeRadius = 300;

    public const uint AnimalCount = 3;
    public const uint EnemyCount = 3;

    public const PictureBoxSizeMode SizeMode = PictureBoxSizeMode.Zoom;

    public const string PlayerTag = "Player";
    public const string AnimalTag = "Animal";
    public const string EnemyTag = "Enemy";
    public const string BulletTag = "Bullet";
    public const string HitmarkerTag = "Hitmarker";
    public const string DropAmmoTag = "DropAmmo";
    public const string DropAnimalTag = "DropAnimal";
    public const string DropMedicalTag = "DropMedical";
    public const string DropValuableTag = "DropValuable";

    public const string PlayerName = "Player";
    public const string EnemyZombieName = "Zombie";
    public const string EnemyCreeperName = "Creeper";
    public const string AnimalBoarName = "Boar";
    public const string AnimalGoatName = "Goat";
    public const string ValuableGoldName = "ValuableGold";
    public const string ValuableRolexName = "ValuableRolex";
    public const string ValuableParcelBoxName = "ValuableParcelBox";
    public const string ValuableCigarettesName = "ValuableCigarettes";
    public const string MedicalMedkitSmallName = "MedicalMedkitSmall";
    public const string MedicalMedkitLargeName = "MedicalMedkitLarge";
    public const string MedicalHealthPotionName = "MedicalHealthPotion";
    public const string AnimalBoarMeatName = "AnimalBoarMeat";
    public const string AnimalGoatMeatName = "AnimalGoatMeat";
    public const string AmmoBoxName = "AmmoBox";

    public static readonly Size PlayerSize = new(90, 90);
    public static readonly Size EnemySize = new(75, 75);
    public static readonly Size AnimalSize = new(75, 75);
    public static readonly Size DropSize = new(50, 50);
    public static readonly Size HitmarkerSize = new(20, 20);
    public static readonly Size BulletSize = new(5, 5);

    public static readonly Color BulletColor = Color.White;
    public static readonly Color DayColor = Color.FromArgb(0xFF, 0x8F, 0xBC, 0x8F); // Light green
    public static readonly Color NightColor = Color.FromArgb(0xFF, 0x2F, 0x4F, 0x2F); // Dark green

    public static readonly Dictionary<string, (Bitmap Up, Bitmap Down, Bitmap Left, Bitmap Right)> EntityImages = new()
    {
        { PlayerName, (Assets.PlayerUp, Assets.PlayerDown, Assets.PlayerLeft, Assets.PlayerRight) },
        { EnemyZombieName, (Assets.EnemyZombieUp, Assets.EnemyZombieDown, Assets.EnemyZombieLeft, Assets.EnemyZombieRight) },
        { EnemyCreeperName, (Assets.EnemyCreeper, Assets.EnemyCreeper, Assets.EnemyCreeper, Assets.EnemyCreeper) },
        { AnimalBoarName, (Assets.AnimalBoar, Assets.AnimalBoar, Assets.AnimalBoar, Assets.AnimalBoar) },
        { AnimalGoatName, (Assets.AnimalGoat, Assets.AnimalGoat, Assets.AnimalGoat, Assets.AnimalGoat) }
    };
}
