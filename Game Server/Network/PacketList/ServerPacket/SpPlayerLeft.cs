namespace GameServer.Network.PacketList {
    public sealed class SpPlayerLeft : SendPacket {
        public SpPlayerLeft(int index) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
        }
    }
}