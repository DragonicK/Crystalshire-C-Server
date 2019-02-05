namespace GameServer.Network {
    public interface IConnection {
        int Index { get; set; }
        bool Connected { get; set; }
        void CheckConnectionTimeOut();
        void Send(ByteBuffer msg, string name);
        void ReceiveData();
        void Disconnect();
        void SendPing();
    }
}