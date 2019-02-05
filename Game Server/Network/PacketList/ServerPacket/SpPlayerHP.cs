using GameServer.Data;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerHP : SendPacket {
        public SpPlayerHP(Player player) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(5000); //MaxHP
            msg.Write(player.Vitals[(int)VitalType.HP]);
        }
    }
}