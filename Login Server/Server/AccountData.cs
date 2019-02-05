namespace LoginServer.Server {
    public sealed class AccountData {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Passphrase { get; set; }
        public byte Activated { get; set; }
        public bool Banned { get; set; }
        public int Cash { get; set; }
        public int AccessLevel { get; set; }
    }
}