using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class SpChatUpdate : SendPacket {
        public SpChatUpdate(int npcIndex, int currentChat, Conversation conversation) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(npcIndex);

            if (currentChat > 0) {
                msg.Write(conversation.Chats[currentChat].Conversation);
            } 
            else {
                msg.Write(string.Empty);
            }

            for (var i = 1; i <= Configuration.MaxConversationOptions; i++) {
                if (currentChat > 0) {
                    msg.Write(conversation.Chats[currentChat].RText[i]);
                }
                else {
                    msg.Write(string.Empty);
                }
            }
        }
    }
}