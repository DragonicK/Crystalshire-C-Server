using System.Collections.Generic;

namespace GameServer.Data {
    public sealed class Conversation {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatCount { get; set; }
        public Dictionary<int, Chat> Chats { get; set; }

        public Conversation() {
            Name = string.Empty;
            ChatCount = 1;
            Chats = new Dictionary<int, Chat> {
                { 1, new Chat() }
            };
        }
    }
}