namespace GameServer.Network.PacketList {
    public sealed class CpPing : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new SpPing();
            msg.Send(connection);
        }
    }
}