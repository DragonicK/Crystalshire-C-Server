using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpBroadcastMessage : SendPacket { 
        public SpBroadcastMessage(string message, QBColor color) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(message);
            msg.Write((int)color);
        }
    }
}