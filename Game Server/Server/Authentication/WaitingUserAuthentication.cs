using GameServer.Network;
using GameServer.Communication;
using GameServer.Network.PacketList;
using GameServer.Server.Character;
using Elysium.Logs;

namespace GameServer.Server {
    public sealed class WaitingUserAuthentication {
        public IConnection Connection { get; set; }
        public string Username { get; set; }
        public string UniqueKey { get; set; }

        public void Authenticate() {       
            var user = new WaitingUserData();

            if (Validate()) {
                // Sai do método quando o login está duplicado.
                if (IsLoginDuplicated()) {
                    // Remove os dados da lista obrigando um novo login.
                    WaitingUserData.Remove(user.ListIndex);
                }
                else {
                    user = WaitingUserData.FindUser(UniqueKey);

                    // Depois de conferido, adiciona na lista de usuários.
                    Authentication.Add(user, Connection);

                    // Remove da lista já que não é mais necessário.
                    WaitingUserData.Remove(user.ListIndex);

                    SendData();

                    Global.WriteLog(LogType.Player, $"Authenticated user {Username} {UniqueKey}", LogColor.Green);
                }
            }
            else {
                Disconnect(Connection, AlertMessageType.Connection);
                return;
            }
        }

        /// <summary>
        /// Carrega os dados dos personages e envia as classes.
        /// </summary>
        private void SendData() {
            var characters = new CharacterDatabase(Authentication.Players[Connection.Index]);
            characters.SendCharactersAsync();

            var msg = new SpClasses();
            msg.Send(Connection);
        }

        private bool IsLoginDuplicated() {

            // Verifica se o usuário já está conectado.
            var pData = Authentication.FindByUsername(Username);

            if (pData != null) {
                // Desconecta e envia a mensagem de login duplicado.
                Disconnect(Connection, AlertMessageType.DuplicatedLogin);
                Connection.Disconnect();

                // Desconecta e envia a mensagem de tentativa de login.
                Disconnect(pData.Connection, AlertMessageType.TryingToLogin);

                return true;
            }

            return false;
        }

        private bool Validate() {
            var user = new WaitingUserData();
            var isValid = false;

            if (!string.IsNullOrEmpty(UniqueKey)) {
                user = WaitingUserData.FindUser(UniqueKey);

                if (user != null) {
                    if (string.CompareOrdinal(user.Username.ToLower(), Username.ToLower()) == 0) {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        private void Disconnect(IConnection connection, AlertMessageType alertMessageType) {
            var msg = new SpAlertMessage(alertMessageType, MenuResetType.Login);
            msg.Send(connection);

            connection.Disconnect();
        }

        private void WriteLogResult(int loadResult) {
            var message = string.Empty;
            var color = LogColor.Green;

            if (loadResult == 0) {
                message = $"Loaded characters from {Username}";
            }
            else {
                message = $"Failed to load characters from {Username}";
                color = LogColor.Crimson;
            }

            Global.WriteLog(LogType.Player, message, color);
        }
    }
}