using System;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpPlayerDirection : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            var msg = new ByteBuffer(buffer);
            var direction = msg.ReadInt32();

            if (!Enum.IsDefined(typeof(Direction), direction)) {
                return;
            }

            pData.Direction = (Direction)direction;

            var pDirection = new SpPlayerDirection(connection.Index, pData.Direction);
            pDirection.SendToMapBut(pData.Index, pData.MapId);            
        }
    }
}