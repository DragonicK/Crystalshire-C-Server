namespace GameServer.Network.PacketList {
    public sealed class SpLoadMap : SendPacket {
        public SpLoadMap(int mapId) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(mapId);
        }
    }
}