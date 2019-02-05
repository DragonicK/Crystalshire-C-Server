namespace GameServer.Network.PacketList {
    public sealed class SpConversationEditor : SendPacket {
        public SpConversationEditor() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}