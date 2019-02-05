using System.Collections.Generic;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpUpdateInventory : SendPacket {

        public void Build(int inventoryIndex, Dictionary<int,Inventory> inventories) {
            msg.Clear();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(inventoryIndex);
            msg.Write(inventories[inventoryIndex].Id);
            msg.Write(inventories[inventoryIndex].Value);
        }

        public void Build(int inventoryIndex, Inventory inventory) {
            msg.Clear();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(inventoryIndex);
            msg.Write(inventory.Id);
            msg.Write(inventory.Value);
        }
    }
}