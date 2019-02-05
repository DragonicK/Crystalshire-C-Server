using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpAuthenticateLogin : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var username = msg.ReadString();
            var uniqueKey = msg.ReadString();

            if (username.Length > 0 && uniqueKey.Length > 0) {
                var authenticator = new WaitingUserAuthentication() {
                    Username = username,
                    UniqueKey = uniqueKey,
                    Connection = connection
                };

                authenticator.Authenticate();
            }
        }
    }
}