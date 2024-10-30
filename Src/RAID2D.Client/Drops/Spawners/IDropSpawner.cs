namespace RAID2D.Client.Drops.Spawners;

public interface IDropSpawner
{
    public IDroppableItem CreateDrop(string dropType, Point? location = null, string? animalName = null);
}
