namespace GameServer.Network.PacketList {
    public sealed class SpItemEditor : SendPacket {
        public SpItemEditor() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}