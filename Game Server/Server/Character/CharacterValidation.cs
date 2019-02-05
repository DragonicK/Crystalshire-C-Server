using System;
using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Server.Character {
    public sealed class CharacterValidation {
        public Player Player { get; set; }
        public string CharacterName { get; private set; }
        public int CharacterIndex { get; private set; }
        public Class CharacterClass { get; private set; }
        public GenderType GenderType { get; private set; }
        public int SpriteId { get; private set; }
          
        /// <summary>
        /// Verifica se o nome é válido.
        /// </summary>
        /// <param name="characterName"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateName(string characterName) {

            if (string.IsNullOrEmpty(characterName)) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true                  
                };      
            }

            if (ProhibitedNames.Compare(characterName)) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.NameIllegal,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            if (characterName.Length < Constants.MinCharacterNameLength || characterName.Length > Constants.MaxCharacterNameLength) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.NameLength,
                    MenuResetType = MenuResetType.Characters,    
                };
            }

            // Adiciona o nome verificado na propriedade.
            CharacterName = characterName;

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verifica se o slot do personagem é válido.
        /// </summary>
        /// <param name="characterIndex"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateCharacterIndex(int characterIndex) {
            if (characterIndex < 1 || characterIndex > Configuration.MaxCharacters) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true

                };
            }

            CharacterIndex = characterIndex;

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verifica se o índice da classe é válido.
        /// </summary>
        /// <param name="classIndex"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateClass(int classIndex) {
            if (!DataManagement.IsValidClass(classIndex)) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            CharacterClass = DataManagement.Classes[classIndex];

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verfica se o gênero é válido.
        /// </summary>
        /// <param name="genderType"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateGender(int genderType) {
            if (!Enum.IsDefined(typeof(GenderType), genderType)) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            GenderType = (GenderType)genderType;

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verifica se o índice da sprite é válido.
        /// Este método deve ser executado por último, pois usa as informações da classe e de gênero.
        /// </summary>
        /// <param name="spriteIndex"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateSprite(int spriteIndex) {
            int[] sprites = null;

            if (GenderType == GenderType.Male) {
                sprites = CharacterClass.MaleSprite;
            }

            if (GenderType == GenderType.Female) {
                sprites = CharacterClass.FemaleSprite;
            }

            if (spriteIndex < 0 || spriteIndex >= sprites.Length) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            SpriteId = sprites[spriteIndex];

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verifica a faixa de level para exclusão do personagem.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateLevel(int level) {
            if (level < 1) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            if (level < Configuration.CharacterDeleteMinLevel && level > Configuration.CharacterDeleteMaxLevel) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.InvalidLevelDelete,
                    MenuResetType = MenuResetType.Characters,
                };
            }

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }

        /// <summary>
        /// Verifica se o slot de personagem está vazio.
        /// Retorna AlertMessageType.None quando está vazio.
        /// </summary>
        /// <param name="characterIndex"></param>
        /// <returns></returns>
        public CharacterValidationResult ValidateEmptyCharacterSelection(int characterIndex) {
            if (Player.CharacterSelection[characterIndex].Name.Length > 0) {
                return new CharacterValidationResult() {
                    AlertMessageType = AlertMessageType.Connection,
                    MenuResetType = MenuResetType.Login,
                    Disconnect = true
                };
            }

            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.None,
                MenuResetType = MenuResetType.None
            };
        }
    }
}