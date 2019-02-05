namespace LoginServer.Database {
    public abstract class DBTemplate {
        protected IDBConnection sqlConnection;
        protected IDBFactory factory;

        public DBTemplate(IDBFactory dbFactory) {
            factory = dbFactory;

            sqlConnection = factory.GetConnection();
        }

        public DBError Open() {
            return sqlConnection.Open();
        }

        public bool Connected() {
            return sqlConnection.IsOpen();
        }

        public void Close() {
            sqlConnection.Close();
        }
    }
}