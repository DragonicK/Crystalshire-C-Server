using System.Data;
using System.Data.SqlClient;

namespace GameServer.Database.SqlServer {
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

        public void SetCommandType(DBCommandType dBCommandType) {
            CommandType command = CommandType.Text;

            switch (dBCommandType) {
                case DBCommandType.Text:
                    command = CommandType.Text;
                    break;
                case DBCommandType.StoredProcedure:
                    command = CommandType.StoredProcedure;
                    break;
                case DBCommandType.TableDirect:
                    command = CommandType.TableDirect;
                    break;
            }

            sqlCommand.CommandType = command;
        }
    }
}