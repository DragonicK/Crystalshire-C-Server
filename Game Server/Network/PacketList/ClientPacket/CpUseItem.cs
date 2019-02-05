using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpUseItem : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var inventory = msg.ReadInt32();

            if (inventory < 1 || inventory > Configuration.MaxInventories) {
                return;
            }

            // ItemFunctions.UseItem(connection.Index, inventory);
        }
    }
}