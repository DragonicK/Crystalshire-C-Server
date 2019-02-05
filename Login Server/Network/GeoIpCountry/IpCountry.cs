namespace LoginServer.Network {
    public sealed class IpCountry {
        /// <summary>
        /// Endereço inicial.
        /// </summary>
        public string IpFrom { get; set; }

        /// <summary>
        /// Endereço final.
        /// </summary>
        public string IpTo { get; set; }

        /// <summary>
        /// Número do endereço, mínimo.
        /// </summary>
        public long NumberMin { get; set; }

        /// <summary>
        /// Número do endereço, máximo.
        /// </summary>
        public long NumberMax { get; set; }

        /// <summary>
        /// Código do país
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// País.
        /// </summary>
        public string Country { get; set; }
    }
}