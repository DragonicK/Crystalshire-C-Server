using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class SpUpdateConversation : SendPacket {
        public void Build(Conversation conversation) {
            msg.Clear();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(conversation.Id);
            msg.Write(conversation.Name);
            msg.Write(conversation.ChatCount);

            for (var i = 1; i <= conversation.ChatCount; i++) {
                msg.Write(conversation.Chats[i].Conversation);

                for (var n = 1; n <= Configuration.MaxConversationOptions; n++) {
                    msg.Write(conversation.Chats[i].RText[n]);
                    msg.Write(conversation.Chats[i].RTarget[n]);
                }

                msg.Write(conversation.Chats[i].Event);
                msg.Write(conversation.Chats[i].Data1);
                msg.Write(conversation.Chats[i].Data2);
                msg.Write(conversation.Chats[i].Data3);
            }
        }
    }
}