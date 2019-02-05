using GameServer.Server;
namespace GameServer.Network.PacketList {
    public sealed class SpMapMessage: SendPacket {
        public SpMapMessage(string message, TextColor color) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(message);
            msg.Write((int)color);
        }
    }
}