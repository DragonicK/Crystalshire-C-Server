namespace GameServer.Data {
    public sealed class EventCommand {
        public byte Type { get; set; }
        public string Text { get; set; }
        public int Colour { get; set; }
        public byte Channel { get; set; }
        public byte TargetType { get; set; }
        public int Target { get; set; }
    }
}