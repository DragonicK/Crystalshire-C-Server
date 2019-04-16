using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network.PacketList {
    public sealed class CpSaveItem : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData.AccessLevel < AccessLevel.Administrator) {
                return;
            }

            var msg = new ByteBuffer(buffer);
            var itemId = msg.ReadInt32();

            if (itemId <= 0 || itemId > Configuration.MaxItems) {
                msg.Flush();
                return;
            }

            var item = new Item() {
                Name = msg.ReadString(),
                Description = msg.ReadString(),
                Sound = msg.ReadString(),
                Icon = msg.ReadInt32(),
                Type = (ItemType)msg.ReadByte(),
                Data1 = msg.ReadInt32(),
                Data2 = msg.ReadInt32(),
                Data3 = msg.ReadInt32(),
                ClassRequired = msg.ReadInt32(),
                AccessLevelRequired = msg.ReadInt32(),
                LevelRequired = msg.ReadInt32(),
                Price = msg.ReadInt32(),
                Rarity = msg.ReadByte(),
                BindType = msg.ReadByte(),
                Animation = msg.ReadInt32()
            };

            DataManagement.ItemDatabase[itemId] = item;
            DataManagement.ItemDatabase.SaveFile(itemId);

            Global.WriteLog(LogType.Game, $"Character: {pData.Character} {pData.AccessLevel.ToString()} saved itemId {itemId}", LogColor.Green);

            var updateItems = new SpUpdateItem();
            updateItems.Build(item);
            updateItems.SendToAllBut(pData.Index);
        }
    }
}