using System;

namespace LoginServer.Server {
    /// <summary>
    /// Dados de um usuário banido.
    /// </summary>
    public struct AccountBan {
        /// <summary>
        /// Ban Id (não é o Id do usuário).
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Indica que o ban é permanente ou não.
        /// </summary>
        public bool Permanent { get; set; }
 
        /// <summary>
        /// Indica o estado atual, expirado ou não expirado.
        /// </summary>
        public Expired Expired { get; set; }

        /// <summary>
        /// Data para expiração.
        /// </summary>
        public DateTime ExpireDate { get; set; }
    }
}