namespace GameServer.Network.PacketList {
    public sealed class SpAnimationEditor : SendPacket {
        public SpAnimationEditor() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}