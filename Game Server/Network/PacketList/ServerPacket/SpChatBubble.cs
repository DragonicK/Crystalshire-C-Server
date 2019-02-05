using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpChatBubble : SendPacket {
        public SpChatBubble(int index, TargetType targetType, string message, TextColor color) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write((int)targetType);
            msg.Write(message);
            msg.Write((int)color);
        }
    }
}