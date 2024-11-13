using RAID2D.Client.Utils;

namespace RAID2D.Client.Managers;

public static class DropManager
{
    private static T GetRandomDropData<T>(Dictionary<string, T> dropMap, string? animalName = null) where T : IDropData
    {
        Dictionary<string, T> filteredMap = dropMap;

        if (typeof(T) == typeof(AnimalDropData) && animalName != null)
        {
            filteredMap = dropMap
                .Where(entry => entry.Value is AnimalDropData animalData && animalData.AnimalName == animalName)
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            if (filteredMap.Count == 0)
                throw new InvalidOperationException($"No drops found for animal name '{animalName}'.");
        }

        var totalChance = filteredMap.Values.Sum(dropData => dropData.DropChance);

        uint randomValue = (uint)Rand.Next(0, (int)totalChance);

        foreach (var entry in filteredMap)
        {
            if (randomValue < entry.Value.DropChance)
                return entry.Value;

            randomValue -= entry.Value.DropChance;
        }

        throw new InvalidOperationException("No drop was randomly genereated (this shouldnt happen)");
    }

    // Name getters
    public static AmmoDropData GetRandomAmmoDropData() => GetRandomDropData(AmmoDropsMap);
    public static AnimalDropData GetRandomAnimalDropDataByAnimalName(string animalName) => GetRandomDropData(AnimalDropsMap, animalName);
    public static MedicalDropData GetRandomMedicalDropData() => GetRandomDropData(MedicalDropsMap);
    public static ValuableDropData GetRandomValuableDropData() => GetRandomDropData(ValuableDropsMap);

    // Drop Data getters
    public static AmmoDropData GetAmmoDropData(string dropName) => AmmoDropsMap[dropName];
    public static AnimalDropData GetAnimalDropData(string dropName) => AnimalDropsMap[dropName];
    public static MedicalDropData GetMedicalDropData(string dropName) => MedicalDropsMap[dropName];
    public static ValuableDropData GetValuableDropData(string dropName) => ValuableDropsMap[dropName];

    // Constants
    private static readonly Dictionary<string, AmmoDropData> AmmoDropsMap = new()
    {
        { Constants.AmmoBoxName, new AmmoDropData(Constants.AmmoBoxName, 5, 100, Assets.DropAmmo) }
    };

    private static readonly Dictionary<string, AnimalDropData> AnimalDropsMap = new()
    {
        { Constants.AnimalBoarMeatName, new AnimalDropData(Constants.AnimalBoarMeatName, Constants.AnimalBoarName, 100, 100, Assets.DropBoarMeat)},
        { Constants.AnimalGoatMeatName, new AnimalDropData(Constants.AnimalGoatMeatName, Constants.AnimalGoatName, 100, 100, Assets.DropGoatMeat)},
        { Constants.AnimalSheepMeatName, new AnimalDropData(Constants.AnimalSheepMeatName, Constants.AnimalSheepName, 100, 100, Assets.DropGoatMeat)}, // TODO: Change to sheep meat
        { Constants.AnimalCowMeatName, new AnimalDropData(Constants.AnimalCowMeatName, Constants.AnimalCowName, 100, 100, Assets.DropGoatMeat)} // TODO: Change to cow meat
    };

    private static readonly Dictionary<string, MedicalDropData> MedicalDropsMap = new()
    {
        { Constants.MedicalHealthPotionName, new MedicalDropData(Constants.MedicalHealthPotionName, 100, 10, Assets.DropHealthPotion) },
        { Constants.MedicalMedkitSmallName,  new MedicalDropData(Constants.MedicalMedkitSmallName,   50, 30, Assets.DropMedkitSmall) },
        { Constants.MedicalMedkitLargeName,  new MedicalDropData(Constants.MedicalMedkitLargeName,   20, 60, Assets.DropMedkitLarge) },
    };

    private static readonly Dictionary<string, ValuableDropData> ValuableDropsMap = new()
    {
        { Constants.ValuableGoldName,       new ValuableDropData(Constants.ValuableGoldName,      100, 10, Assets.DropGold) },
        { Constants.ValuableRolexName,      new ValuableDropData(Constants.ValuableRolexName,      60, 20, Assets.DropRolex) },
        { Constants.ValuableParcelBoxName,  new ValuableDropData(Constants.ValuableParcelBoxName,  20, 35, Assets.DropParcelBox) },
        { Constants.ValuableCigarettesName, new ValuableDropData(Constants.ValuableCigarettesName, 20, 35, Assets.DropCigarettes) }
    };
}

public interface IDropData { string Name { get; } uint DropChance { get; } }

public record AmmoDropData(string Name, uint AmmoAmount, uint DropChance, Image Image) : IDropData;
public record AnimalDropData(string Name, string AnimalName, uint HealthAmount, uint DropChance, Image Image) : IDropData;
public record MedicalDropData(string Name, uint HealthAmount, uint DropChance, Image Image) : IDropData;
public record ValuableDropData(string Name, uint CashAmount, uint DropChance, Image Image) : IDropData;
