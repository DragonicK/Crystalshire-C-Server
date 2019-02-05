namespace GameServer.Data {
    public sealed class Item {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sound { get; set; }
        public int Icon { get; set; }
        public ItemType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int ClassRequired { get; set; }
        public int AccessLevelRequired { get; set; }
        public int LevelRequired { get; set; }
        public int Price { get; set; }
        public byte Rarity { get; set; }
        public byte BindType { get; set; }
        public int Animation { get; set; }

        public Item() {
            Name = string.Empty;
            Description = string.Empty;
            Sound = "None.";
        }
    }
}