namespace RAID2D.Client.Drops.Spawners;

public class DropSpawner : IDropSpawner
{
    IDroppableItem IDropSpawner.CreateDrop(string dropType, Point? location, string? animalName)
    {
        switch (dropType)
        {
            case Constants.DropAmmoTag:
                return new AmmoDrop();
            case Constants.DropAnimalTag:
                return new AnimalDrop(
                    location ?? throw new ArgumentException("Location cannot be null"),
                    animalName ?? throw new ArgumentException("Animal name cannot be null")
                    );
            case Constants.DropMedicalTag:
                return new MedicalDrop();
            case Constants.DropValuableTag:
                return new ValuableDrop(location ?? throw new ArgumentException("Location cannot be null"));

            default:
                throw new ArgumentException("Invalid drop type");
        }
    }
}
