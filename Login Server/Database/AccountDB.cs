using System;
using System.Collections.Generic;
using LoginServer.Server;
using LoginServer.Communication;
using LoginServer.Database.MySql;

namespace LoginServer.Database {
    public sealed class AccountDB : DBTemplate {

        public AccountDB() : base(new DBFactory()) {
 
        }

        /// <summary>
        /// Obtém informações básicas da conta de usuário.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public AccountData GetAccount(string username) {
            var query = $"SELECT AccountId, Passphrase, Cash, Activated, AccountLevelCode FROM AccountData WHERE {GetLoginColumn()} = @Username";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@Username", username);

            var sqlReader = sqlCommand.ExecuteReader();
            var account = new AccountData();

            if (sqlReader.Read()) {
                account.Username = username;
                account.AccountId = (int)sqlReader.GetData("AccountId");
                account.Passphrase = (string)sqlReader.GetData("Passphrase");
                account.Cash = (int)sqlReader.GetData("Cash");
                account.Activated = Convert.ToByte(sqlReader.GetData("Activated"));
                account.AccessLevel = Convert.ToInt16(sqlReader.GetData("AccountLevelCode"));
            }

            sqlReader.Close();
            return account;
        }

        public bool IsBanned(int accountId) {
            var records = GetBannedRecords(accountId);
            var result = false;

            if (records.Count > 0) {

                foreach (var record in records) {
                    // Se o registro for permanente ou não estiver expirado.
                    if (record.Permanent || IsDateExpired(record.ExpireDate) == Expired.No) {
                        result = true;
                    }
                }

                // Atualiza a lista quando houver algum registro expirado.
                UpdateBannedRecords(records);
            }

            return result;
        }

        public void UpdateLastLoginDate(int accountId) {
            var query = "UPDATE AccountData SET LastLoginDate=@LastLoginDate WHERE AccountId=@AccountId";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@LastLoginDate", DateTime.Now);
            sqlCommand.AddParameter("@AccountId", accountId);
            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Compara as datas e retorna se está expirado ou não.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private Expired IsDateExpired(DateTime date) {
            if (DateTime.Now.CompareTo(date) == (int)Expired.Yes) {
                return Expired.Yes;
            }

            return Expired.No;
        }

        /// <summary>
        /// Atualiza o vencimento do banimento.
        /// </summary>
        /// <param name="banID"></param>
        /// <param name="expired"></param>
        private void UpdateBanExpiration(long banId, Expired expired) {
            var query = "UPDATE AccountBan SET Expired=@Value WHERE BanId=@BanId";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@Value", (byte)expired);
            sqlCommand.AddParameter("@BanId", banId);
            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Obtém uma lista com os registros banidos do usuário que estão ativos.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private List<AccountBan> GetBannedRecords(int accountId) {
            var query = $"SELECT BanId, ExpireDate, Permanent FROM AccountBan WHERE AccountId=@AccountId AND Expired=@Expired";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@AccountId", accountId);
            sqlCommand.AddParameter("@Expired", (int)Expired.No);

            var sqlReader = sqlCommand.ExecuteReader();

            // Lista de todos os registros de ban que ainda estão ativos.
            var records = new List<AccountBan>();

            while (sqlReader.Read()) {
                var record = new AccountBan {
                    Id = Convert.ToInt32(sqlReader.GetData("BanId"))
                };

                if (!DBNull.Value.Equals(sqlReader.GetData("ExpireDate"))) {
                    record.ExpireDate = Convert.ToDateTime(sqlReader.GetData("ExpireDate"));
                }

                record.Permanent = Convert.ToBoolean(sqlReader.GetData("Permanent"));

                // Adiciona na lista.
                records.Add(record);
            }

            sqlReader.Close();

            return records;
        }

        /// <summary>
        /// Atualiza os registros de banimento na tabela.
        /// </summary>
        /// <param name="records"></param>
        private void UpdateBannedRecords(List<AccountBan> records) {
            foreach (var record in records) {
                if (IsDateExpired(record.ExpireDate) == Expired.Yes) {
                    UpdateBanExpiration(record.Id, Expired.Yes);
                }
            }
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