using System.Data;
using System.Data.SqlClient;

namespace LoginServer.Database.SqlServer {
    public sealed class DBConnection : IDBConnection {
        public SqlConnection Connection { get; }

        public static string DataSource { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }
        public static string Database { get; set; }
        public static int MinPoolSize { get; set; }
        public static int MaxPoolSize { get; set; }

        public DBConnection() {
            var sqlConnectionString = new SqlConnectionStringBuilder {
                DataSource = DataSource,
                InitialCatalog = Database,
                Password = Password,
                UserID = UserID,
                Pooling = true,
                MinPoolSize = MinPoolSize,
                MaxPoolSize = MaxPoolSize
            };

            Connection = new SqlConnection(sqlConnectionString.ToString());
        }
        
        public void Close() {
            if (Connection != null) {
                Connection.Close();
            }
        }

        public DBError Open() {
            var dbError = new DBError();

            try {
                Connection.Open();
            }
            catch (SqlException ex) {
                dbError.Number = ex.Number;
                dbError.Message = ex.Message;
            }

            return dbError;
        }

        public bool IsOpen() {
            return Connection.State == ConnectionState.Open;
        }
    }
}