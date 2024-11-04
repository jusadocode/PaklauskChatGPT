namespace RAID2D.Client.Drops.Spawners;

public class DropSpawner : IDropSpawner
{
    IDroppableItem IDropSpawner.CreateDrop(string dropType, Point? location, string? animalName)
    {
        return dropType switch
        {
            Constants.DropAmmoTag => new AmmoDrop(),
            Constants.DropAnimalTag => new AnimalDrop(
                                location ?? throw new ArgumentException("Location cannot be null"),
                                animalName ?? throw new ArgumentException("Animal name cannot be null")
                                ),
            Constants.DropMedicalTag => new MedicalDrop(),
            Constants.DropValuableTag => new ValuableDrop(location ?? throw new ArgumentException("Location cannot be null")),
            _ => throw new ArgumentException("Invalid drop type"),
        };
    }
}
