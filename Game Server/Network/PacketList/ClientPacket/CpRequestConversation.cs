using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestConversation : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            Conversation conversation;

            var updateConversations = new SpUpdateConversation();
            for (var i = 1; i <= Configuration.MaxConversations; i++) {
                if (DataManagement.ConversationDatabase[i].Name.Length > 0) {
                    conversation = DataManagement.ConversationDatabase[i];

                    updateConversations.Build(conversation);
                    updateConversations.Send(connection);
                }
            }
        }
    }
}