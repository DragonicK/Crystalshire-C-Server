using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpAdminWarp : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var warp = new AdministratorWarp() {
                Player = Authentication.Players[connection.Index]
            };

            warp.AdminWarpLocation(msg.ReadInt32(), msg.ReadInt32());
        }
    }
}