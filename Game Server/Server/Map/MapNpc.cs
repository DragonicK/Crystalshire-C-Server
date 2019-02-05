using GameServer.Data;

namespace GameServer.Server {
    public sealed class MapNpc {
        public int Id { get; set; }
        public int HP { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public NpcBehaviour Behaviour { get; set; }
        
        public int SpwanWait { get; set; }
        public Direction LastDir { get; set; }
        public int InChatWith { get; set; }
    }
}