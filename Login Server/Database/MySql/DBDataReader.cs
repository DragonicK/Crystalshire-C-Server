using MySql.Data.MySqlClient;

namespace LoginServer.Database.MySql {
    public sealed class DBDataReader : IDBDataReader {
        private MySqlDataReader sqlReader;

        public DBDataReader(MySqlDataReader reader) {
            sqlReader = reader;
        }

        public void Close() {
            sqlReader.Close();
            sqlReader.Dispose();
        }

        public bool Read() {
            return sqlReader.Read();
        }

        public object GetData(string column) {
            return sqlReader[column];
        }
    }
}