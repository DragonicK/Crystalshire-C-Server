using System.Data;
using System.Data.SqlClient;

namespace LoginServer.Database.SqlServer {
    public sealed class DBCommand : IDBCommand {
        private SqlCommand sqlCommand;
        private DBConnection sqlConnection;

        public DBCommand(IDBConnection dbConnection) {
            sqlConnection = dbConnection as DBConnection;

            sqlCommand = new SqlCommand {
                Connection = sqlConnection.Connection,
                CommandType = CommandType.Text
            };
        }

        public void AddParameter(string parameter, object value) {
            sqlCommand.Parameters.AddWithValue(parameter, value);
        }

        public int ExecuteNonQuery() {
            return sqlCommand.ExecuteNonQuery();
        }

        public IDBDataReader ExecuteReader() {
            var reader = sqlCommand.ExecuteReader();
            var sqlReader = new DBDataReader(reader);
            return sqlReader;
        }

        public void SetCommand(string commandText) {
            sqlCommand.CommandText = commandText;
        }
    }
}