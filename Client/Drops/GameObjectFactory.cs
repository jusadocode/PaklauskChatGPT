using Client.Utils;

namespace Client.Drops;

public static class GameObjectFactory
{
    public static IGameObject CreateGameObject(string objectType, Point location)
    {
        switch (objectType.ToLower())
        {
            case "valuable":
                IGameObject valuableItem = Constants.ValuableDrops.ElementAt(Rand.Next(Constants.ValuableDrops.Count)).Value;
                valuableItem.Create(location);
                return valuableItem;
            case "medical":
                IGameObject medicalItem = Constants.MedicalDrops.ElementAt(Rand.Next(Constants.MedicalDrops.Count)).Value;
                medicalItem.Create(location);
                return medicalItem;

            default:
                throw new ArgumentException("Invalid object type");
        }
    }
}
