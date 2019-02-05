namespace GameServer.Server.Character {
    /// <summary>
    /// Level mínimo, máximo e o tempo para exclusão do personagem.
    /// </summary>
    public struct CharacterLevelRange {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public short Minutes { get; set; }

        public CharacterLevelRange(int minimum, int maximum, short minutes) {
            Minimum = minimum;
            Maximum = maximum;
            Minutes = minutes;
        }
    }
}