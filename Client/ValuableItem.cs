namespace Client
{
    internal class ValuableItem
    {
        public string name { get; set; }
        public int value { get; set; }
        public Image image { get; set; }
        public int spawnChance { get; set; }
        public ValuableItem(string name, int value, int spawnChance, Image image) { 
            this.name = name;
            this.value = value;
            this.image = image;
            this.spawnChance = spawnChance;
        }
    }
}
