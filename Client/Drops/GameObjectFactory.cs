namespace Client.Drops;

public static class GameObjectFactory
{
    private static readonly Random random = new();
    private static readonly Dictionary<string, IGameObject> valuableItems = new()
    {
        { "gold", new ValuableItem("gold", 100, 10, Assets.DropGold) },
        { "rolex", new ValuableItem("rolex", 60, 20, Assets.DropRolex) },
        { "parcel_box", new ValuableItem("parcel_box", 20, 35, Assets.DropParcelBox) },
        { "cigarettes", new ValuableItem("cigarettes", 20, 35, Assets.DropCigarettes) }
    };
    private static readonly Dictionary<string, IGameObject> medicalItems = new()
    {
        { "small_medkit", new MedicalItem("small_medkit", 20, 90, Assets.DropMedkitSmall) },
        { "large_medkit", new MedicalItem("large_medkit", 50, 90, Assets.DropMedkitLarge) },
        { "health_potion", new MedicalItem("health_potion", 100, 90, Assets.DropMedkitLarge) }
    };

    public static IGameObject CreateGameObject(string objectType, Point location)
    {
        switch (objectType.ToLower())
        {
            case "valuable":
                IGameObject valuableItem = valuableItems.ElementAt(random.Next(valuableItems.Count)).Value;
                _ = valuableItem.CreatePictureBox(location);
                return valuableItem;
            case "medical":
                IGameObject medicalItem = medicalItems.ElementAt(random.Next(medicalItems.Count)).Value;
                _ = medicalItem.CreatePictureBox(location);
                return medicalItem;

            default:
                throw new ArgumentException("Invalid object type");
        }
    }
}
