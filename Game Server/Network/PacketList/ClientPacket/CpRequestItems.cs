using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestItems : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            Item item;

            var items = new SpUpdateItem();
            for (var i = 1; i <= Configuration.MaxItems; i++) {
                if (DataManagement.ItemDatabase[i].Name.Length > 0) {
                    item = DataManagement.ItemDatabase[i];

                    items.Build(item);
                    items.Send(connection);
                }
            }
        }
    }
}