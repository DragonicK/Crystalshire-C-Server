using GameServer.Server;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpWarpTo : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            var msg = new ByteBuffer(buffer);

            var warp = new AdministratorWarp() {
                Player = Authentication.Players[connection.Index]
            };

            var mapId = msg.ReadInt32();

            if (mapId > 0 || mapId <= Configuration.MaxMaps) {
                warp.MoveToMap(Global.GetMap(mapId));
            }
        }
    }
}