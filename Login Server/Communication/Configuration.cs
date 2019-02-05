using Elysium.IO;
using Elysium.Logs;
using LoginServer.Network;
using LoginServer.Database.MySql;

namespace LoginServer.Communication {
    public static class Configuration {
        public static ClientVersion Version { get; set; }

        /// <summary>
        /// Porta de conexão do servidor.
        /// </summary>
        public static int Port { get; set; }

        /// <summary>
        /// Ip do connect server.
        /// </summary>
        public static string GameServerIp { get; set; }

        /// <summary>
        /// Porta do connect server.
        /// </summary>
        public static int GameServerPort { get; set; }

        /// <summary>
        /// Sleep do loop principal.
        /// </summary>
        public static int Sleep { get; set; }

        /// <summary>
        /// Desativa ou ativa o login do usuário.
        /// </summary>
        public static bool Maintenance { get; set; }

        /// <summary>
        /// Permite que o sistema use o email como usuário de login.
        /// </summary>
        public static bool UseEmailAsLogin { get; set; }

        /// <summary>
        /// Tempo de verificação de cada novo acesso de ip.
        /// </summary>
        public static int CheckAccessTime { get; set; }

        /// <summary>
        /// Quantidade máxima de tentativas de acesso de cada ip a cada intervalo de tempo.
        /// </summary>
        public static int IpMaxAttempt { get; set; }

        /// <summary>
        /// Quantidade máxima de acesso pelo mesmo ip.
        /// </summary>
        public static int IpMaxAccessCount { get; set; }

        /// <summary>
        /// Tempo de vida do ip bloqueado no sistema para consulta.
        /// </summary>
        public static int IpLifetime { get; set; }

        private static Settings settings;

        public static void Open() {
            settings = new Settings();
            settings.ParseConfigFile("LoginConfig.txt");
        }

        public static void Close() {
            settings.Clear();
            settings = null;
        }

        public static void GetGeneralConfig() {
            Port = settings.GetInt32("Port");
            Sleep = settings.GetInt32("Sleep");
 
            UseEmailAsLogin = settings.GetBoolean("UseEmailAsLogin");

            GeoIpBlock.Enabled = settings.GetBoolean("GeoIp");
            Checksum.Enabled = settings.GetBoolean("CheckSum");

            IpBlockList.Enabled = settings.GetBoolean("IpBlock");
            IpBlockList.IpBlockTime = settings.GetInt32("IpBlockLifeTime");

            CheckAccessTime = settings.GetInt32("FilterCheckAccessTime");
            IpLifetime = settings.GetInt32("FilterIpLifeTime");
            IpMaxAttempt = settings.GetInt32("IpMaxAttempt");
            IpMaxAccessCount = settings.GetInt32("IpMaxAccessCount");

            Version = new ClientVersion() {
                ClientMajor = settings.GetByte("ClientMajor"),
                ClientMinor = settings.GetByte("ClientMinor"),
                ClientRevision = settings.GetByte("ClientRevision")
            };

            GameServerIp = settings.GetString("GameServerIp");
            GameServerPort = settings.GetInt32("GameServerPort");
        }

        public static void GetDatabaseConfig() {
            DBConnection.DataSource = settings.GetString("DataSource");
            DBConnection.Database = settings.GetString("Database");
            DBConnection.UserId = settings.GetString("UserID");
            DBConnection.Password = settings.GetString("Password");
            DBConnection.MinPoolSize = settings.GetInt32("MinPoolSize");
            DBConnection.MaxPoolSize = settings.GetInt32("MaxPoolSize");
        }

        public static void CheckDatabaseConnection() {
            Global.WriteLog(LogType.System,"Trying to connect database", LogColor.Green);

            var dbFactory = new DBFactory();
            var dBConnection = dbFactory.GetConnection();
            var dbError = dBConnection.Open();

            if (dbError.Number != 0) {
                Global.WriteLog(LogType.System, $"Cannot connect to database", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                if (dBConnection.IsOpen()) {
                    Global.WriteLog(LogType.System, "Database is connected successfully", LogColor.Green);
                }
            }

            dBConnection?.Close();
        }

        public static void ShowConfigInLog() {
            string result = string.Empty;
            const string Enabled = "Enabled";
            const string Disabled = "Disabled";

            Global.WriteLog(LogType.System, $"Port: {Port}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Sleep: {Sleep}", LogColor.Black);

            result = (UseEmailAsLogin) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"UseEmailAsLogin: {result}", LogColor.BlueViolet);

            result = (GeoIpBlock.Enabled) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"GeoIp: {result}", LogColor.BlueViolet);
            
            result = (Checksum.Enabled) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"CheckSum: {result}", LogColor.BlueViolet);

            result = (IpBlockList.Enabled) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"IpBlock: {result}", LogColor.BlueViolet);

            Global.WriteLog(LogType.System, $"IpBlockLifeTime: {IpBlockList.IpBlockTime}", LogColor.Black);

            Global.WriteLog(LogType.System, $"FilterCheckAccessTime: {CheckAccessTime}", LogColor.Black);
            Global.WriteLog(LogType.System, $"FilterIpLifeTime: {IpLifetime}", LogColor.Black);
            Global.WriteLog(LogType.System, $"IpMaxAttempt: {IpMaxAttempt}", LogColor.Black);
            Global.WriteLog(LogType.System, $"IpMaxAccessCount: {IpMaxAccessCount}", LogColor.Black);

            Global.WriteLog(LogType.System, $"Game Server Ip: {GameServerIp}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Game Server Port: {GameServerPort}", LogColor.Black);

            Global.WriteLog(LogType.System, $"Version: {Version.ClientMajor}.{Version.ClientMinor}.{Version.ClientRevision}", LogColor.BlueViolet);
        }
    }
}