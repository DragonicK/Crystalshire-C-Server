using GameServer.Communication;
using GameServer.Database.MySql;

namespace GameServer.Database {
    public sealed class AccountDB : DBTemplate {

        public AccountDB() : base(new DBFactory()) {
 
        }

        /// <summary>
        /// Obtém a atual coluna para fazer as pesquisas na tabela.   
        /// </summary>
        /// <returns></returns>
        private string GetLoginColumn() {
            return (Configuration.UseEmailAsLogin) ? "Email" : "Username";   
        }
    }
}