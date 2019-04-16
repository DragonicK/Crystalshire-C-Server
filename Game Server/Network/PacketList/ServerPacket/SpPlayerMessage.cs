using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerMessage : SendPacket {
        public void Build(string message, TextColor color) {
            msg.Flush();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(message);
            msg.Write((int)color);
        }
    }
}