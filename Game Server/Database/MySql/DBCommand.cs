using System.Data;
using MySql.Data.MySqlClient;

namespace GameServer.Database.MySql {
    public sealed class DBCommand : IDBCommand {
        private MySqlCommand sqlCommand;
        private DBConnection sqlConnection;

        public DBCommand(IDBConnection dbConnection) {
            sqlConnection = dbConnection as DBConnection;

            sqlCommand = new MySqlCommand {
                Connection = sqlConnection.Connection,
                CommandType = CommandType.Text
            };
        }

        public void AddParameter(string parameter, object value) {
            sqlCommand.Parameters.AddWithValue(parameter, value);
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

        public void ClearParameter() {
            sqlCommand.Parameters.Clear();
        }

        public int ExecuteNonQuery() {
            if (sqlConnection.IsOpen()) {
                return sqlCommand.ExecuteNonQuery();
            }

            return 0;
        }

        public IDBDataReader ExecuteReader() {
            if (sqlConnection.IsOpen()) {
                var reader = sqlCommand.ExecuteReader();
                var sqlReader = new DBDataReader(reader);

                return sqlReader;
            }

            return null;
        }

        public void SetCommand(string commandText) {
            sqlCommand.CommandText = commandText;
        }
    }
}