using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpWarpToMe : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            var msg = new ByteBuffer(buffer);
            var name = msg.ReadString();

            if (name.Length > 0) {
                var warp = new AdministratorWarp() {
                    Player = Authentication.Players[connection.Index]
                };

                warp.MovePlayerToMe(Authentication.FindByCharacter(name));
            }
        }
    }
}