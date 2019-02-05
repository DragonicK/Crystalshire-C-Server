namespace GameServer.Network.PacketList {
    public sealed class SpMapEditor : SendPacket {
        public SpMapEditor () {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}