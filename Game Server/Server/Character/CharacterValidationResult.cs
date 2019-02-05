namespace GameServer.Server.Character {
    public struct CharacterValidationResult {
        public AlertMessageType AlertMessageType { get; set; }
        public MenuResetType MenuResetType { get; set; }
        public bool Disconnect { get; set; }
    }
}