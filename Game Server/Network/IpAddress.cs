namespace GameServer.Network {
    public struct IpAddress {
        public string Ip { get; set; }

        public int AttemptCount { get; set; }

        public int AccessCount { get; set; }

        public bool Permanent { get; set; }

        /// <summary>
        /// Tempo de duração do ip no sistema para verificação.
        /// </summary>
        public int Time { get; set; }

        public IpAddress(string ipAddress, int time, bool permanent) {
            Ip = ipAddress;
            Time = time;
            Permanent = permanent;
            AttemptCount = 1;
            AccessCount = 1;
        }
    }
}