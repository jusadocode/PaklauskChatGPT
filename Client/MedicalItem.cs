namespace Client
{
    internal class MedicalItem
    {
        public string name { get; set; }
        public int healthSize { get; set; }
        public Image image { get; set; }
        public int spawnChance { get; set; }
        public MedicalItem(string name, int healthSz, int spawnChance, Image image)
        {
            this.name = name;
            this.healthSize = healthSz;
            this.image = image;
            this.spawnChance = spawnChance;
        }
    }
}
