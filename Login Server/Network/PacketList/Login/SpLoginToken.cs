namespace LoginServer.Network.PacketList.Login {
    /// <summary>
    /// Envia a chave unica para o cliente.
    /// </summary>
    public sealed class SpLoginToken : SendPacket {
        public SpLoginToken(string uniqueKey) {
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(uniqueKey);
        }
    }
}