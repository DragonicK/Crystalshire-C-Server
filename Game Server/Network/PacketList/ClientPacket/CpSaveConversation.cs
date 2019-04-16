using System.Collections.Generic;
using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network.PacketList {
    public sealed class CpSaveConversation : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData.AccessLevel < AccessLevel.Administrator) {
                return;
            }

            var msg = new ByteBuffer(buffer);
            var conversationId = msg.ReadInt32();

            if (conversationId <= 0 || conversationId > Configuration.MaxConversations) {
                msg.Flush();
                return;
            }

            var conversation = new Conversation() {
                Id = conversationId,
                Name = msg.ReadString(),
                ChatCount = msg.ReadInt32()
            };

            conversation.Chats = new Dictionary<int, Chat>();

            for (var i = 1; i <= conversation.ChatCount; i++) {
                conversation.Chats.Add(i, new Chat());
                conversation.Chats[i].Conversation = msg.ReadString();

                for (var n = 1; n <= Configuration.MaxConversationOptions; n++) {
                    conversation.Chats[i].RText[n] = msg.ReadString();
                    conversation.Chats[i].RTarget[n] = msg.ReadInt32();
                }

                conversation.Chats[i].Event = msg.ReadInt32();
                conversation.Chats[i].Data1 = msg.ReadInt32();
                conversation.Chats[i].Data2 = msg.ReadInt32();
                conversation.Chats[i].Data3 = msg.ReadInt32();
            }

            DataManagement.ConversationDatabase[conversationId] = conversation;
            DataManagement.ConversationDatabase.SaveFile(conversationId);

            Global.WriteLog(LogType.Game, $"Character: {pData.Character} {pData.AccessLevel.ToString()} saved conversationId {conversationId}", LogColor.Green);

            var updateConversation = new SpUpdateConversation();
            updateConversation.Build(conversation);
            updateConversation.SendToAllBut(pData.Index);
        }
    }
}