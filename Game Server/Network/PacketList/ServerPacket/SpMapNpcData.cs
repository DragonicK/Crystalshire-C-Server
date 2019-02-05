using GameServer.Server;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class SpMapNpcData : SendPacket {
        public SpMapNpcData(MapInstance map) {
            msg.Write((int)OpCode.SendPacket[GetType()]);

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                msg.Write(map.Npcs[i].Id);
                msg.Write(map.Npcs[i].X);
                msg.Write(map.Npcs[i].Y);
                msg.Write((int)map.Npcs[i].Direction);
                msg.Write(map.Npcs[i].HP);
            }
        }
    }
}