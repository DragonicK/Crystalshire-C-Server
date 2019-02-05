namespace GameServer.Network.PacketList {
    public sealed class SpNpcEditor : SendPacket {
        public SpNpcEditor() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}