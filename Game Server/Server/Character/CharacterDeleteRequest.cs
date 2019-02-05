using System;
using System.Collections.Generic;

namespace GameServer.Server.Character {
    public sealed class CharacterDeleteRequest {

        #region Level Range
        private static List<CharacterLevelRange> levels;

        static CharacterDeleteRequest() {
            levels = new List<CharacterLevelRange>();
        }

        public static void AddTime(int minimum, int maximum, short minutes) {
            levels.Add(new CharacterLevelRange(minimum, maximum, minutes));
        }
        #endregion

        public Action<int, int> DeleteCharacter { get; set; }

        public List<CharacterDelete> Characters { get; set; }

        public CharacterDeleteRequest() {
            Characters = new List<CharacterDelete>();
        }

        public void AddCharacter(int accountId, int characterId, int level) {
            if (!FindCharacterByID(characterId)) {
                Characters.Add(new CharacterDelete(accountId, characterId, GetExclusionDate(level)));
            }
        }

        public void AddCharacter(int accountId, int characterId, DateTime date) {
            if (!FindCharacterByID(characterId)) {
                Characters.Add(new CharacterDelete(accountId, characterId, date));
            }
        }

        /// <summary>
        /// Cancela a exclusão do personagem.
        /// </summary>
        /// <param name="charID"></param>
        public void CancelDelete(int characterId) {
            for (int n = 0; n < Characters.Count; n++) {
                if (Characters[n].CharacterId.CompareTo(characterId) == 0) {
                    Characters.RemoveAt(n);
                }
            }
        }

        /// <summary>
        /// Retorna a data com o tempo de exclusão.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public DateTime GetExclusionDate(int level) {
            for (int n = 0; n < levels.Count; n++) {
                if (level >= levels[n].Minimum && level <= levels[n].Maximum) {
                    return DateTime.Now.AddMinutes(levels[n].Minutes);
                }
            }

            return DateTime.Now;
        }

        /// <summary>
        /// Retorna o tempo em minutos.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public short GetExclusionMinutes(int level) {
            for (int n = 0; n < levels.Count; n++) {
                if (level >= levels[n].Minimum && level <= levels[n].Maximum) {
                    return levels[n].Minutes;
                }
            }

            return 0;
        }

        /// <summary>
        /// Obtem a data de exclusão do personagem.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public DateTime GetCharacterExclusionDate(int characterId) {
            for (int n = 0; n < Characters.Count; n++) {
                if (Characters[n].CharacterId == characterId) {
                    return Characters[n].Date;
                }
            }

            return new DateTime();
        }

        public bool FindCharacterByID(int characterId) {
            for (int n = 0; n < Characters.Count; n++) {
                if (Characters[n].CharacterId.CompareTo(characterId) == 0) {
                    return true;
                }
            }

            return false;
        }

        public void CheckForDeletedCharacters() {
            for (int n = 0; n < Characters.Count; n++) {
                if (CanDelete(n)) {
                    // Invoca o evento e remove o personagem.
                    DeleteCharacter?.Invoke(Characters[n].AccountId, Characters[n].CharacterId);
                    // Remove da lista?
                    Characters.RemoveAt(n);
                }
            }
        }

        private bool CanDelete(int index) {
            return (DateTime.Now.CompareTo(Characters[index].Date) == (int)Expired.Yes) ? true : false;
        }
    }
}