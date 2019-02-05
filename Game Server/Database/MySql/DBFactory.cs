namespace GameServer.Database.MySql {
    public sealed class DBFactory : IDBFactory {
        public IDBCommand GetCommand(IDBConnection dBConnection) {
            return new DBCommand(dBConnection);
        }

        public IDBConnection GetConnection() {
            return new DBConnection();
        }
    }
}