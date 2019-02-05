using System.IO;
using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class NpcDatabase : Database<Npc> {
        public NpcDatabase() {
            MaxValues = Configuration.MaxNpcs;
            values = new Dictionary<int, Npc>();

            for (var i = 1; i <= MaxValues; i++) {
                values.Add(i, new Npc());
            }
        }
        
        public override void LoadFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var npc = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(file);

            npc.Id = reader.ReadInt32();
            npc.Name = reader.ReadString();
            npc.Sound = reader.ReadString();
            npc.Sprite = reader.ReadInt32();
            npc.SpawnSeconds = reader.ReadInt32();
            npc.Behaviour = reader.ReadByte();
            npc.HP = reader.ReadInt32();
            npc.Animation = reader.ReadInt32();
            npc.Level = reader.ReadInt32();
            npc.Conversation = reader.ReadInt32();

            reader.Close();
            reader.Dispose();

            file.Close();
            file.Dispose();
        }

        public override void SaveFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var npc = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(file);

            npc.Id = objectId;
            writer.Write(npc.Id);
            writer.Write(npc.Name); 
            writer.Write(npc.Sound);
            writer.Write(npc.Sprite);
            writer.Write(npc.SpawnSeconds);
            writer.Write(npc.Behaviour);
            writer.Write(npc.HP);
            writer.Write(npc.Animation);
            writer.Write(npc.Level);
            writer.Write(npc.Conversation);

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }
    }
}