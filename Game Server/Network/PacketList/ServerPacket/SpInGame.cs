namespace GameServer.Network.PacketList {
    public sealed class SpInGame : SendPacket {
        public SpInGame() {
            msg.Write((int)OpCode.SendPacket[GetType()]);
        }
    }
}