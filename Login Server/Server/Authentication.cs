using Elysium.Logs;
using LoginServer.Common;
using LoginServer.Communication;
using LoginServer.Database;

namespace LoginServer.Server {
    public sealed class Authentication {
        /// <summary>
        ///  Verifica os dados do usuário.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="username"></param>
        /// <param name="passphrase"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public AccountData Authenticate(ClientVersion version, string username, string passphrase, out AuthenticationResult result) {
            if (Configuration.Maintenance) {
                result = AuthenticationResult.Maintenance;
                return null;
            }

            if (!Configuration.Version.Compare(version)) {
                result= AuthenticationResult.VersionOutdated;
                return null;
            }

            if (username.Length < Constants.MinStringLength || passphrase.Length < Constants.MinStringLength) {
                result = AuthenticationResult.StringLength;
                return null; 
            }

            var hash = new Hash();
            var database = new AccountDB();
            var dbError = database.Open();

            if (dbError.Number != 0) {
                Global.WriteLog(LogType.System, $"Failed to authenticate user {username}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);

                result = AuthenticationResult.Error;
                return null;  
            }

            var account = database.GetAccount(username);
            if (account.AccountId > 0) {
                account.Banned = database.IsBanned(account.AccountId);
            }

            if (account.AccountId == 0) {
                result = AuthenticationResult.WrongUserData;
                return account;
            }

            if (account.Activated == 0) {
                result = AuthenticationResult.AccountIsNotActivated;
                return account;
            }

            if (account.Passphrase.CompareTo(hash.Compute(passphrase)) != 0) {
                result = AuthenticationResult.WrongUserData;
                return account;
            }

            if (account.Banned) {
                result = AuthenticationResult.AccountIsBanned;
                return account;
            }

            database.UpdateLastLoginDate(account.AccountId);
            database.Close();

            result = AuthenticationResult.Success;
            return account;
        }
    }
}