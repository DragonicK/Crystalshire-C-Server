namespace GameServer.Network.PacketList {
    public sealed class SpCancelAnimation : SendPacket {
        public SpCancelAnimation(int index) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
        }
    }
}