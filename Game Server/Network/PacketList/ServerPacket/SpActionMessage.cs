using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpActionMessage : SendPacket {
        public SpActionMessage(string message, int x, int y, ActionMessageType messageType, TextColor color) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(message);
            msg.Write(x);
            msg.Write(y);
            msg.Write((int)messageType);
            msg.Write((int)color);
        }
    }
}