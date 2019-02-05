using System;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpPlayerMove : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var direction = msg.ReadInt32();
            var movement = msg.ReadInt32();
            var x = msg.ReadInt32();
            var y = msg.ReadInt32();

            if (!Enum.IsDefined(typeof(Direction), direction)) {
                return;
            }

            if (!Enum.IsDefined(typeof(MovementType), movement)) {
                return;
            }

            var movements = new PlayerMovement() {
                Player = Authentication.Players[connection.Index],
                Map = Authentication.Players[connection.Index].GetMap()
            };

            movements.Move((Direction)direction, (MovementType)movement, x, y);
        }
    }
}