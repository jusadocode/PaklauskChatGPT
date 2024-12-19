using RAID2D.Shared;

namespace RAID2D.Client;

public static class Constants
{
    public const int BulletSpeed = 10;
    public const int PlayerSpeed = 7;
    public const int EnemySpeed = 4;
    public const int AnimalSpeed = 4;

    public const int FormBounds = 35; // Fake wall around the form for collision detection

    public const double MiddleOfDayHour = 12.0f;
    public const double EndOfDayHour = 24.0f;
    public const double HourIncrementRate = 1.0f;

    public const int GameTimerInterval = 20; // milliseconds
    public const int DayTimeUpdateInterval = 1000;
    public const int HitmarkDuration = 200;

    public const uint EnemyDamage = 1;
    public const uint PulsingEnemyDamage = 50;

    public const int PlayerMaxHealth = 100;
    public const uint PlayerInitialAmmo = 10;
    public const uint PlayerLowHealthLimit = 20;
    public const int ShieldedEnemyMaxHealth = 50;

    public const uint EnemyFleeRadius = 500;
    public const uint AnimalFleeRadius = 300;

    public const uint AnimalCount = 3;
    public const uint EnemyCount = 3;

    public const uint MutatedEnemySpawnChance = 20;

    public const PictureBoxSizeMode SizeMode = PictureBoxSizeMode.Zoom;

    public const string LocalServerBaseUrl = "http://localhost";
    public const string CloudServerBaseUrl = "http://46.101.218.250";
    public const string LocalServerPort = "8080";
    public const string CloudServerPort = "8080";
    public const string LocalServerDefaultUrl = $"{LocalServerBaseUrl}:{LocalServerPort}/{SharedConstants.ServerHub}";
    public const string CloudServerDefaultUrl = $"{CloudServerBaseUrl}:{CloudServerPort}/{SharedConstants.ServerHub}";
    public const string ServerUrl = LocalServerDefaultUrl;

    public const string PlayerTag = "Player";
    public const string ServerPlayerTag = "ServerPlayer";
    public const string AnimalTag = "Animal";
    public const string EnemyTag = "Enemy";
    public const string PulsingEnemyTag = "PulsingEnemy";
    public const string ShieldedEnemyTag = "ShieldedEnemy";
    public const string BulletTag = "Bullet";
    public const string HitmarkerTag = "Hitmarker";
    public const string DropAmmoTag = "DropAmmo";
    public const string DropAnimalTag = "DropAnimal";
    public const string DropMedicalTag = "DropMedical";
    public const string DropValuableTag = "DropValuable";

    public const string PlayerName = "Player";
    public const string ServerPlayerName = "ServerPlayer";
    public const string EnemyZombieName = "Zombie";
    public const string EnemyCreeperName = "Creeper";
    public const string EnemySpiderName = "Spider";
    public const string EnemyEndermanName = "Enderman";
    public const string AnimalBoarName = "Boar";
    public const string AnimalGoatName = "Goat";
    public const string AnimalSheepName = "Sheep";
    public const string AnimalCowName = "Cow";
    public const string ValuableGoldName = "ValuableGold";
    public const string ValuableRolexName = "ValuableRolex";
    public const string ValuableParcelBoxName = "ValuableParcelBox";
    public const string ValuableCigarettesName = "ValuableCigarettes";
    public const string MedicalMedkitSmallName = "MedicalMedkitSmall";
    public const string MedicalMedkitLargeName = "MedicalMedkitLarge";
    public const string MedicalHealthPotionName = "MedicalHealthPotion";
    public const string AnimalBoarMeatName = "AnimalBoarMeat";
    public const string AnimalGoatMeatName = "AnimalGoatMeat";
    public const string AnimalSheepMeatName = "AnimalSheepMeat";
    public const string AnimalCowMeatName = "AnimalCowMeat";
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

    public static readonly Dictionary<string, (Bitmap Up, Bitmap Down, Bitmap Left, Bitmap Right)> DirectionalImages = new()
    {
        { PlayerName, (Assets.PlayerUp, Assets.PlayerDown, Assets.PlayerLeft, Assets.PlayerRight) },
        { EnemyZombieName, (Assets.EnemyZombieUp, Assets.EnemyZombieDown, Assets.EnemyZombieLeft, Assets.EnemyZombieRight) },
        { EnemyCreeperName, (Assets.EnemyCreeper, Assets.EnemyCreeper, Assets.EnemyCreeper, Assets.EnemyCreeper) },
        { EnemySpiderName, (Assets.EnemySpider, Assets.EnemySpider, Assets.EnemySpider, Assets.EnemySpider) },
        { EnemyEndermanName, (Assets.EnemyEnderman, Assets.EnemyEnderman, Assets.EnemyEnderman, Assets.EnemyEnderman) },
        { AnimalBoarName, (Assets.AnimalBoar, Assets.AnimalBoar, Assets.AnimalBoar, Assets.AnimalBoar) },
        { AnimalGoatName, (Assets.AnimalGoat, Assets.AnimalGoat, Assets.AnimalGoat, Assets.AnimalGoat) },
        { AnimalSheepName, (Assets.AnimalSheep, Assets.AnimalSheep, Assets.AnimalSheep, Assets.AnimalSheep) },
        { AnimalCowName, (Assets.AnimalCow, Assets.AnimalCow, Assets.AnimalCow, Assets.AnimalCow) },
    };
}
