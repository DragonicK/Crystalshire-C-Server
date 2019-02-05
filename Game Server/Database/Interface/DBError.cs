namespace GameServer.Database {
    public sealed class DBError {
        public int Number { get; set; }
        public string Message { get; set; }

        public DBError() {
            Number = 0;
            Message = string.Empty;
        }
    }
}