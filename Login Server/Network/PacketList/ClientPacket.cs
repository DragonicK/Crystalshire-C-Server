namespace LoginServer.Network.PacketList {
    public static class ClientPacket {

        #region Game Client

        /// <summary>
        /// Cabeçalho para ping.
        /// </summary>
        public const int Ping = 1;

        /// <summary>
        /// Tentativa de login a partir do cliente.
        /// </summary>
        public const int AuthLogin = 2;

        #endregion

    }
}