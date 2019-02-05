using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpMapNpcDirection : SendPacket {
        public SpMapNpcDirection(int index, Direction direction) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write((int)direction);
        }
    }
}