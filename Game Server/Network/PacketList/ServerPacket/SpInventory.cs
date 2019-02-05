using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpInventory : SendPacket {
        public SpInventory(Player player) {
             msg.Write((int)OpCode.SendPacket[GetType()]);
            
            for (var i = 1; i <= player.Inventory.Count; i++) {
                msg.Write(player.Inventory[i].Id);
                msg.Write(player.Inventory[i].Value);
            }
        }
    }
}