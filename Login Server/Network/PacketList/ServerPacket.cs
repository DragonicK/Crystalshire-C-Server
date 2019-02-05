namespace LoginServer.Network.PacketList {
    public static class ServerPacket {

        #region Game Client
        /// <summary>
        /// Cabeçalho para ping.
        /// </summary>
        public const int Ping = 1;

        /// <summary>
        /// Exibe mensagem de dialogo no cliente.
        /// </summary>
        public const int AlertMessage = 2;

        /// <summary>
        /// Envia a chave unica para o cliente.
        /// </summary>
        public const int LoginToken = 3;

        #endregion

        #region Game Server

        /// <summary>
        /// Envia os dados do login para o game server.
        /// </summary>
        public const int SendUserData = 255;

        #endregion
    }
}