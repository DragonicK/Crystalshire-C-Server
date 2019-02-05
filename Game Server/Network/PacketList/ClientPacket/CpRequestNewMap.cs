using System;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestNewMap : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var direction = msg.ReadInt32();

            if (!Enum.IsDefined(typeof(Direction), direction)) {
                return;
            }

            var movements = new PlayerMovement() {
                Player = Authentication.Players[connection.Index],
                Map = Authentication.Players[connection.Index].GetMap()
            };

            movements.RequestMap((Direction)direction);
        }
    }
}