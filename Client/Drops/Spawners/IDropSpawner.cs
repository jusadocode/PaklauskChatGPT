namespace Client.Drops.Spawners;

public interface IDropSpawner
{
    public IDroppableItem CreateDrop(string dropType, Point? location = null, string? animalName = null);
}
