using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestNpcs : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new SpUpdateNpc();
            var npc = new Npc();

            for (var i = 1; i <= Configuration.MaxNpcs; i++) {
                if (DataManagement.NpcDatabase[i].Name.Length > 0) {
                    npc = DataManagement.NpcDatabase[i];

                    msg.Build(npc);
                    msg.Send(connection);
                }
            }
        }
    }
}