using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpMapRespawn : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            pData.GetMap().Respawn(pData);
        }
    }
}