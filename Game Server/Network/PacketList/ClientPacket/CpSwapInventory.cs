using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpSwapInventory : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            var msg = new ByteBuffer(buffer);
            var oldSlot = msg.ReadInt32();
            var newSlot = msg.ReadInt32();

            var oldInventory = pData.Inventory[oldSlot];
            var newInventory = pData.Inventory[newSlot];

            pData.Inventory[newSlot] = oldInventory;
            pData.Inventory[oldSlot] = newInventory;

            var updateInventory = new SpUpdateInventory();
            updateInventory.Build(oldSlot, pData.Inventory[oldSlot]);
            updateInventory.Send(connection);

            updateInventory.Build(newSlot, pData.Inventory[newSlot]);
            updateInventory.Send(connection);
        }
    }
}
