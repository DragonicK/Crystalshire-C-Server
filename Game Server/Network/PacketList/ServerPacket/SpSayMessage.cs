using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpSayMessage : SendPacket {
        public void Build(string character, AccessLevel accessLevel, string message, string tag, QBColor color) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(character);
            msg.Write((int)accessLevel);
            msg.Write(message);
            msg.Write(tag);
            msg.Write((int)color);
        }
    }
}