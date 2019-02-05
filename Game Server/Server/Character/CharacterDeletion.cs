using System;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Server.Character {
    public sealed class CharacterDeletion : CharacterValidationResultMessage {
        CharacterSelection character;
        private readonly int characterIndex;

        public CharacterDeletion(int characterIndex) {
            this.characterIndex = characterIndex;
        }

        public void Delete() {
            if (!Configuration.CharacterDelete) {
                SendMessage(new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.CharacterDelete,
                    MenuResetType = MenuResetType.Characters        
                });

                return;
            }

            var validation = new CharacterValidation() {
                Player = Player
            };

            CharacterValidationResult validationResult;

            validationResult = validation.ValidateCharacterIndex(characterIndex);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            // Retorna AlertMessageType.None quando o slot está vazio.
            validationResult = validation.ValidateEmptyCharacterSelection(characterIndex);

            // Quando o slot de personagem está vazio, muda o AlertType para Connection.
            if (validationResult.AlertMessageType == AlertMessageType.None) {
;               validationResult.AlertMessageType = AlertMessageType.Connection;
                validationResult.MenuResetType = MenuResetType.Login;
                validationResult.Disconnect = true;

                // Realiza a desconexão pois é impossível deletar um personagem que não existe.
                if (!CanValidateNext(validationResult)) {
                    return;
                }
            }

            character = Player.CharacterSelection[characterIndex];

            validationResult = validation.ValidateLevel(character.Level);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            // Inicia o processo de requisição para exclusão.
            if (!character.PendingExclusion) {
                ProcessRequestDelete();
            }
        }

        private void ProcessRequestDelete() {
            Global.DeleteRequest.AddCharacter(Player.AccountId, character.CharacterId, character.Level);
            DateTime date = Global.DeleteRequest.GetCharacterExclusionDate(character.CharacterId);

            // Atualiza o tempo do personagem e envia os personagens.
            var characters = new CharacterDatabase(Player);
            characters.UpdateRequestDelete(character.CharacterId, true, date);
            characters.SendCharactersAsync();

            Global.WriteLog(LogType.Player, $"Request delete AccountId {Player.AccountId} CharacterId {character.CharacterId} Date {date}", LogColor.BlueViolet);
        }
    }
}