using System.Collections;

namespace LoginServer.Communication {
    /// <summary>
    /// Checksum dos arquivos principais do cliente.
    /// </summary>
    public static class Checksum {
        public static bool Enabled { get; set; }

        private static Hashtable checksum = new Hashtable();

        /// <summary>
        /// Adiciona um novo checksum.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="checkSum"></param>
        public static void Add(string version, string checkSum) {
            checksum.Add(version, checkSum);
        }

        /// <summary>
        /// Realiza uma comparação.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="checksum"></param>
        /// <returns></returns>
        public static bool Compare(string version, string checkSum) {
            return (string.CompareOrdinal(checkSum, (string)checksum[version]) == 0) ? true : false;
        }

        /// <summary>
        /// Limpa a lista de dados.
        /// </summary>
        public static void Clear() {
            checksum.Clear();
        }
    }
}