namespace Client
{
    internal class MedicalItem(string name, int healthSz, int spawnChance, Image image)
    {
        public string Name { get; set; } = name;
        public int HealthSize { get; set; } = healthSz;
        public Image Image { get; set; } = image;
        public int SpawnChance { get; set; } = spawnChance;
    }
}
