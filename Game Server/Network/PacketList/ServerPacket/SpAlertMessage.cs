using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpAlertMessage : SendPacket {
        public SpAlertMessage(AlertMessageType messageType, MenuResetType menuResetType, bool kick = false) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write((int)messageType);
            msg.Write((int)menuResetType);
            msg.Write((kick) ? 1 : 0);
        }
    }
}