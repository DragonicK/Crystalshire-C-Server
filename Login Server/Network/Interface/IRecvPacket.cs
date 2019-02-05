namespace LoginServer.Network {
    public interface IRecvPacket {
        void Process(byte[] buffer, IConnection connection);
    }
}