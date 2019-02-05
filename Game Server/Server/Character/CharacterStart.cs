using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Server.Character {
     public class CharacterStart : CharacterValidationResultMessage {

        public void Use(int characterIndex) {

            var validation = new CharacterValidation() {
                Player = Player
            };

            CharacterValidationResult validationResult;
            validationResult = validation.ValidateCharacterIndex(characterIndex);

            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateEmptyCharacterSelection(characterIndex);

            // Quando o slot de personagem está vazio, muda o AlertType para Connection.
            if (validationResult.AlertMessageType == AlertMessageType.None) {
                validationResult.AlertMessageType = AlertMessageType.Connection;
                validationResult.MenuResetType = MenuResetType.Login;
                validationResult.Disconnect = true;

                // Realiza a desconexão pois é impossível utilizar um personagem que não existe.
                if (!CanValidateNext(validationResult)) {
                    return;
                }
            }

            var character = Player.CharacterSelection[characterIndex];
            if (character.CharacterId > 0) {
                Player.CharacterId = character.CharacterId;

                // Quando um personagem pendente para exclusão é usado. A requisição é cancelada.
                var cancelRequest = false;

                if (Player.CharacterSelection[characterIndex].PendingExclusion) {
                    Global.DeleteRequest.CancelDelete(character.CharacterId);
                    cancelRequest = true;
                }

                var characters = new CharacterDatabase(Player);
                characters.LoadCharacter(cancelRequest);
                characters.Close();

                Global.WriteLog(LogType.Player, $"Loaded characters from {Player.Username}", LogColor.Green);

                var game = new JoinGame() {
                    Player = Player
                };

                game.Join();
            }
        }
    }
}
