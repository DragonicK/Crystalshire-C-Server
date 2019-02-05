using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestEditMap : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData !=null) {
                if (pData.AccessLevel >= AccessLevel.Administrator) {
                    var msg = new SpMapEditor();
                    msg.Send(connection);
                }
            } 
        }
    }
}