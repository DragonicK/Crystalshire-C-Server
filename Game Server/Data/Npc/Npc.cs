namespace GameServer.Data {
    public sealed class Npc {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sound { get; set; }
        public int Sprite { get; set; }
        public int SpawnSeconds { get; set; }
        public byte Behaviour { get; set; }
        public int HP { get; set; }
        public int Animation { get; set; }
        public int Level { get; set; }
        public int Conversation { get; set; }

        public Npc() {
            Name = string.Empty;
            Sound = "None.";
        }
    }
}