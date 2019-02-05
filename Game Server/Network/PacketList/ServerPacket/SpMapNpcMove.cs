using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpMapNpcMove : SendPacket {
        public SpMapNpcMove(int index, int x, int y, Direction direction, MovementType movement) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write(x);
            msg.Write(y);
            msg.Write((int)direction);
            msg.Write((int)movement);
        }
    }
}