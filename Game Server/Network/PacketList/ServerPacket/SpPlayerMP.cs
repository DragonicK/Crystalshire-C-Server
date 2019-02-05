using GameServer.Data;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerMP : SendPacket {
        public SpPlayerMP(Player player) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(5000); //Max
            msg.Write(player.Vitals[(int)VitalType.MP]);
        }
    }
}