namespace LoginServer.Communication {
    public static class Constants {
        /// <summary>
        /// Quantidade mínima de caracteres em uma string.
        /// </summary>
        public const int MinStringLength = 3;

        /// <summary>
        /// Quantidade máxima de caracteres em uma string.
        /// </summary>
        public const int MaxStringLength = 255;

        /// <summary>
        /// Tempo limite em segundos de uma conexão no sistema.
        /// </summary>
        public const int ConnectionTimeOut = 3;

        /// <summary>
        /// Tempo limite de leitura em microsegundos.
        /// </summary>
        public const int ReceiveTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo limite de envio em microsegundos.
        /// </summary>
        public const int SendTimeOut = 15 * 1000 * 1000;
    }
}