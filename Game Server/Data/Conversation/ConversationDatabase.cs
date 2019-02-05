using System.IO;
using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class ConversationDatabase : Database<Conversation> {

        public ConversationDatabase() {
            MaxValues = Configuration.MaxConversations;
            values = new Dictionary<int, Conversation>();

            for (var i = 1; i <= MaxValues; i++) {
                values.Add(i, new Conversation());
            }
        }
        
        public override void LoadFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var conversation = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(file);

            conversation.Id = reader.ReadInt32();
            conversation.Name = reader.ReadString();
            conversation.ChatCount = reader.ReadInt32();
            conversation.Chats = new Dictionary<int, Chat>();

            for (var i = 1; i <= conversation.ChatCount; i++) {
                conversation.Chats.Add(i, new Chat());
                conversation.Chats[i].Conversation = reader.ReadString();

                for (var n = 1; n <= Configuration.MaxConversationOptions; n++) {
                    conversation.Chats[i].RText[n] = reader.ReadString();
                    conversation.Chats[i].RTarget[n] = reader.ReadInt32();
                }

                conversation.Chats[i].Event = reader.ReadInt32();
                conversation.Chats[i].Data1 = reader.ReadInt32();
                conversation.Chats[i].Data2 = reader.ReadInt32();
                conversation.Chats[i].Data3 = reader.ReadInt32();
            }

            reader.Close();
            reader.Dispose();

            file.Close();
            file.Dispose();
        }

        public override void SaveFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var conversation = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(file);

            conversation.Id = objectId;
            writer.Write(conversation.Id);
            writer.Write(conversation.Name);
            writer.Write(conversation.ChatCount);

            for (var i = 1; i <= conversation.ChatCount; i++) {
                writer.Write(conversation.Chats[i].Conversation);
               
                for (var n = 1; n <= Configuration.MaxConversationOptions; n++) {
                    writer.Write(conversation.Chats[i].RText[n]);
                    writer.Write(conversation.Chats[i].RTarget[n]);
                }

                writer.Write(conversation.Chats[i].Event);
                writer.Write(conversation.Chats[i].Data1);
                writer.Write(conversation.Chats[i].Data2);
                writer.Write(conversation.Chats[i].Data3);
            }

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }
    }
}