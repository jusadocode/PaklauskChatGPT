namespace Client
{
    internal class AnimalDrop(string name, int value, int spawnChance, Image image)
    {
        public string Name { get; set; } = name;
        public int HealthSize { get; set; } = value;
        public Image Image { get; set; } = image;
        public int SpawnChance { get; set; } = spawnChance;
    }
}
