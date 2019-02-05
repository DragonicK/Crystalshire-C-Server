using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerXY : SendPacket {
        public SpPlayerXY(int index, int x, int y, Direction direction) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write(x);
            msg.Write(y);
            msg.Write((int)direction);
        }
    }
}