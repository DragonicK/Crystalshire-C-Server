using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Server.Character {
    public sealed class CharacterCreation : CharacterValidationResultMessage {
        private readonly string name;
        private readonly int characterIndex;
        private readonly int classIndex;
        private readonly int genderType;
        private readonly int spriteIndex;

        public CharacterCreation(string name, int characterIndex, int classIndex, int genderType, int spriteIndex) {
            this.name = name;
            this.characterIndex = characterIndex;
            this.classIndex = classIndex;
            this.genderType = genderType;
            this.spriteIndex = spriteIndex;
        }

        public void Create() {
            var validation = new CharacterValidation() {
                Player = Player
            };

            if (!Configuration.CharacterCreation) {
                SendMessage(new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.CharacterCreation,
                    MenuResetType = MenuResetType.Characters               
                });

                return;
            }
        
            CharacterValidationResult validationResult;

            validationResult = validation.ValidateName(name);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateCharacterIndex(characterIndex);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateEmptyCharacterSelection(characterIndex);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateClass(classIndex);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateGender(genderType);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            validationResult = validation.ValidateSprite(spriteIndex);
            if (!CanValidateNext(validationResult)) {
                return;
            }

            var characters = new CharacterDatabase(Player);
            if (characters.Exist(validation.CharacterName)) {
                characters.Close();

                SendMessage(new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.NameTaken,
                    MenuResetType = MenuResetType.Characters
                });
            }
            else {
                characters.Create(validation);
                characters.SendCharactersAsync();
            }

            Global.WriteLog(LogType.Player, $"Created Character {validation.CharacterName} AccountId {Player.AccountId}", LogColor.Green);
        }
    }
}