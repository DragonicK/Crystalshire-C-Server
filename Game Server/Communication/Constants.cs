namespace GameServer.Communication {
    public static class Constants {
        /// <summary>
        /// Quantidade mínima de caracteres no nome do personagem.
        /// </summary>
        public const int MinCharacterNameLength = 4;

        /// <summary>
        /// Quantidade máxima de caracteres no nome do personagem.
        /// </summary>
        public const int MaxCharacterNameLength = 15;

        /// <summary>
        /// Tempo limite em segundos de uma conexão no sistema.
        /// </summary>
        public const int ConnectionTimeOut = 4;

        /// <summary>
        /// Tempo limite de leitura em microsegundos.
        /// </summary>
        public const int ReceiveTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo limite de envio em microsegundos.
        /// </summary>
        public const int SendTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo entre pings em milesimos de segundos.
        /// </summary>
        public const int PingTime = 10000;
    }
}