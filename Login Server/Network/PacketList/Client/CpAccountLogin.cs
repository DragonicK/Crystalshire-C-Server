using LoginServer.Server;
using LoginServer.Communication;
using LoginServer.Network.PacketList.Login;
using LoginServer.Network.PacketList.Game;
using Elysium.Logs;

namespace LoginServer.Network.PacketList.Client {
    /// <summary>
    /// Processo de login do cliente.
    /// </summary>
    public sealed class CpAccountLogin : IRecvPacket {
        IConnection _Connection;

        public void Process(byte[] buffer, IConnection connection) {
            _Connection = connection;

            var msg = new ByteBuffer(buffer);

            var checksum = msg.ReadString();
            if (string.IsNullOrEmpty(checksum)) {
                SendConnectionError();
                return;
            }

            var username = msg.ReadString().ToLower();
            if (string.IsNullOrEmpty(username)) {
                SendConnectionError();
                return;
            }

            var password = msg.ReadString();
            if (string.IsNullOrEmpty(password)) {
                SendConnectionError();
                return;
            }

            var account = Authentication.Authenticate( GetVersion(ref msg) , username, password, out var result);

            if (result != AuthenticationResult.Success) {
                SendMessage(result);
            }
            else {
                // Envia os dados do usuario para o game server.
                SendUserData(account);
                // Envia a chave unica para o cliente.
                SendLoginToken();

                Global.WriteLog(LogType.User, $"Authenticated user {username}", LogColor.Green);
            }

            _Connection.Disconnect();
        }

        private ClientVersion GetVersion(ref ByteBuffer msg) {
            var version = new ClientVersion() {
                ClientMajor = (byte)msg.ReadInt32(),
                ClientMinor = (byte)msg.ReadInt32(),
                ClientRevision = (byte)msg.ReadInt32()
            };

            return version;
        }

        private void SendMessage(AuthenticationResult authenticationResult) {
            SpAlertMessage msg = null;

            switch (authenticationResult) {
                case AuthenticationResult.Error:
                    msg = new SpAlertMessage(AlertMessageType.Database, MenuResetType.None);
                    break;
                case AuthenticationResult.Maintenance:
                    msg = new SpAlertMessage(AlertMessageType.Maintenance, MenuResetType.None);
                    break;
                case AuthenticationResult.AccountIsBanned:
                    msg = new SpAlertMessage(AlertMessageType.Banned, MenuResetType.None);
                    break;
                case AuthenticationResult.AccountIsNotActivated:
                    msg = new SpAlertMessage(AlertMessageType.UserNotActivated, MenuResetType.None);
                    break;
                case AuthenticationResult.WrongUserData:
                    msg = new SpAlertMessage(AlertMessageType.WrongPassword, MenuResetType.Login);
                    break;
                case AuthenticationResult.VersionOutdated:
                    msg = new SpAlertMessage(AlertMessageType.Outdated, MenuResetType.None);
                    break;
                case AuthenticationResult.StringLength:
                    msg = new SpAlertMessage(AlertMessageType.StringLength, MenuResetType.Login);
                    break;
            }

            msg.Send(_Connection);
            msg = null;
        }

        private void SendLoginToken() {
            var msg = new SpLoginToken( ((Connection)_Connection).UniqueKey );
            msg.Send(_Connection);
            msg = null;
        }

        private void SendConnectionError() {
            var msg = new SpAlertMessage(AlertMessageType.Connection, MenuResetType.Login);
            msg.Send(_Connection);
            msg = null;

            _Connection.Disconnect();
        }

        private void SendUserData(AccountData account) {
            var msg = new SpSendUserData() {
                AccountId = account.AccountId,
                Username = account.Username,
                AccessLevel = account.AccessLevel,
                Cash = account.Cash,
                UniqueKey = ((Connection)_Connection).UniqueKey
            };

            msg.Send();
            msg = null;
        }
    }
}