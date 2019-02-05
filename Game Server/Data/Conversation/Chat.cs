using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class Chat {
        public string Conversation { get; set; }
        public Dictionary<int, string> RText { get; set; }
        public Dictionary<int, int> RTarget { get; set; }
        public int Event { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }

        public Chat() {
            Conversation = string.Empty;

            RText = new Dictionary<int, string>();
            RTarget = new Dictionary<int, int>();

            for (var i = 1; i <= Configuration.MaxConversationOptions; i++) {
                RText.Add(i, string.Empty);
                RTarget.Add(i, 0);
            }
        }
    }
}