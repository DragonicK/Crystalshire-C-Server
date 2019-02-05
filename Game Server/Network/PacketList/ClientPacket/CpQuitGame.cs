using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpQuitGame : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            Authentication.Quit(connection.Index);
        }
    }
}