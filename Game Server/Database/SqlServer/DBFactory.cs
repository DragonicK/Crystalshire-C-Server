namespace GameServer.Database.SqlServer {
    public sealed class DBFactory : IDBFactory {
        public IDBCommand GetCommand(IDBConnection dbConnection) {
            return new DBCommand(dbConnection);
        }

        public IDBConnection GetConnection() {
            return new DBConnection();
        }
    }
}