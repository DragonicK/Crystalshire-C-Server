using System;

namespace GameServer.Server.Character {
    public sealed class CharacterSelection {
        public short CharacterIndex { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Sprite { get; set; }
        public int Level { get; set; }
        public short Classe { get; set; }
        public bool PendingExclusion { get; set; }
        public DateTime ExclusionDate { get; set; }

        public CharacterSelection() {
            Name = string.Empty;
            ExclusionDate = new DateTime();
        }

        public void Clear() {
            CharacterIndex = 0;
            CharacterId = 0;
            Name = string.Empty;
            Sprite = 0;
            Level = 0;
            Classe = 0;
            PendingExclusion = false;
        }
    }
}