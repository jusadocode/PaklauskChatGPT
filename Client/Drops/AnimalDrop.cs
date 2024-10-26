namespace Client.Drops;

public class AnimalDrop(string name, uint value, int spawnChance, Image image)
{
    public string Name { get; set; } = name;
    public uint HealthSize { get; set; } = value;
    public Image Image { get; set; } = image;
    public int SpawnChance { get; set; } = spawnChance;
}
