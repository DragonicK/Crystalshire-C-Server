using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpTarget : SendPacket {
        public SpTarget(int target, TargetType targetType) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(target);
            msg.Write((int)targetType);
        }
    }
}