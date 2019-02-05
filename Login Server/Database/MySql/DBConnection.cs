using System.Data;
using MySql.Data.MySqlClient;

namespace LoginServer.Database.MySql {
    public sealed class DBConnection : IDBConnection {
        public MySqlConnection Connection { get; set; }

        public static string DataSource { get; set; }
        public static string UserId { get; set; }
        public static string Password { get; set; }
        public static string Database { get; set; }
        public static int MinPoolSize { get; set; }
        public static int MaxPoolSize { get; set; }

        private readonly string connectionString;

        public DBConnection() {
            connectionString = $"Server={DataSource};";
            connectionString += $"Database={Database};";
            connectionString += $"Uid={UserId};";
            connectionString += $"Pwd={Password};";
            connectionString += $"MinimumPoolSize={MinPoolSize};";
            connectionString += $"MaximumPoolSize={MaxPoolSize};";
            connectionString += "Pooling=true;";
            connectionString += "SSL Mode = None;";

            Connection = new MySqlConnection();
        }

        public DBError Open() {
            var dbError = new DBError();

            Connection.ConnectionString = connectionString;

            try {
                Connection.Open();
            }
            catch (MySqlException ex) {
                // O connector do MySQL não retorna o número do erro.
                // Nesse caso, estou usando o tamanho da mensagem para indicar que há um erro.
                dbError.Number = ex.Message.Length;
                dbError.Message = ex.Message;
            }

            return dbError;
        }

        public void Close() {
            Connection.Close();
            Connection.Dispose();
        }

        public bool IsOpen() {
            return (Connection.State == ConnectionState.Open) ? true : false;
        }
    }
}