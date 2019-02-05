namespace GameServer.Network {
    public interface IRecvPacket {
        void Process(byte[] buffer, IConnection connection);
    }
}