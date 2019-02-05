namespace GameServer.Data {
    public sealed class Event {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PageCount { get; set; }
        public EventPage[] EventPages { get; set; }
    }
}