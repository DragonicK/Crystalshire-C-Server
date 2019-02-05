using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpKickPlayer : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var name = msg.ReadString();

            if (name.Length > 0) {
                var command = new KickPlayer() {
                    Player = Authentication.Players[connection.Index]
                };

                command.Kick(name);
            }
        }
    }
}