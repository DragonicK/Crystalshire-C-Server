using LoginServer.Server;

namespace LoginServer.Network.PacketList.Login {
    /// <summary>
    /// Exibe a mensagem de dialogo no cliente.
    /// </summary>
    public class SpAlertMessage : SendPacket {
        public SpAlertMessage(AlertMessageType messageType, MenuResetType menuResetType, bool kick = false) {
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write((int)messageType);
            msg.Write((int)menuResetType);
            msg.Write( (kick) ? 1 : 0 );
        }
    }
}