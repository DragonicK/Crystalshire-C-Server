namespace GameServer.Data {
    public sealed class Class {
        public int Id { get; set; }
        public bool Selectable { get; set; }
        public string Name { get; set; }
        public int[] MaleSprite { get; set; }
        public int[] FemaleSprite { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int Points { get; set; }

        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Class() {
            MaleSprite = new int[3];
            FemaleSprite = new int[3];
        }
    }
}