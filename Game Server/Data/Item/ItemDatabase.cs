using System.IO;
using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class ItemDatabase : Database<Item> {
        public ItemDatabase() {
            MaxValues = Configuration.MaxItems;
            values = new Dictionary<int, Item>();

            for (var i = 1; i <= MaxValues; i++) {
                values.Add(i, new Item());
            }
        }

        public override void LoadFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var item = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(file);

            item.Id = reader.ReadInt32();
            item.Name = reader.ReadString();
            item.Description = reader.ReadString();
            item.Sound = reader.ReadString();
            item.Icon = reader.ReadInt32();
            item.Type = (ItemType)reader.ReadByte();
            item.Data1 = reader.ReadInt32();
            item.Data2 = reader.ReadInt32();
            item.Data3 = reader.ReadInt32();
            item.ClassRequired = reader.ReadInt32();
            item.AccessLevelRequired = reader.ReadInt32();
            item.LevelRequired = reader.ReadInt32();
            item.Price = reader.ReadInt32();
            item.Rarity = reader.ReadByte();
            item.BindType = reader.ReadByte();
            item.Animation = reader.ReadInt32();

            reader.Close();
            reader.Dispose();

            file.Close();
            file.Dispose();
        }

        public override void SaveFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var item = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(file);

            item.Id = objectId;
            writer.Write(item.Id);
            writer.Write(item.Name);
            writer.Write(item.Description);
            writer.Write(item.Sound);
            writer.Write(item.Icon);
            writer.Write((byte)item.Type);
            writer.Write(item.Data1);
            writer.Write(item.Data2);
            writer.Write(item.Data3);
            writer.Write(item.ClassRequired);
            writer.Write(item.AccessLevelRequired);
            writer.Write(item.LevelRequired);
            writer.Write(item.Price);
            writer.Write(item.Rarity);
            writer.Write(item.BindType);
            writer.Write(item.Animation);

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }
    }
}
