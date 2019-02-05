using System;

namespace GameServer.Server.Character {
    public sealed class CharacterDelete {
        public int AccountId { get; set; }
        public int CharacterId { get; set; }
        public DateTime Date { get; set; }
 
        public CharacterDelete(int accountId, int characterId, DateTime date) {
            AccountId = accountId;
            CharacterId = characterId;
            Date = date;
        }
    }
}