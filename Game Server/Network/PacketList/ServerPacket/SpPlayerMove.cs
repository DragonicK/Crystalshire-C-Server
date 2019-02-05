using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerMove : SendPacket {
        public SpPlayerMove(Player player, MovementType movement) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(player.Index);
            msg.Write(player.X);
            msg.Write(player.Y);
            msg.Write((int)player.Direction);
            msg.Write((int)movement);
        }
    }
}