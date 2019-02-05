namespace GameServer.Network.PacketList {
    public sealed class SpPing :SendPacket {
        public SpPing() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}