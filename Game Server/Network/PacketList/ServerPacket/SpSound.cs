using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpSound : SendPacket{
        public SpSound(int x, int y, SoundEntityType soundEntityType, int objectId) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(x);
            msg.Write(y);
            msg.Write((int)soundEntityType);
            msg.Write(objectId);
        }
    }
}