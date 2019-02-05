using GameServer.Network;
using GameServer.Database.MySql;
using Elysium.IO;
using Elysium.Logs;

namespace GameServer.Communication {
    public static class Configuration {
        /// <summary>
        /// Porta de conexão do servidor.
        /// </summary>
        public static int Port { get; set; }

        /// <summary>
        /// Porta para a conexão com o servidor de login.
        /// </summary>
        public static int LoginPort { get; set; }

        /// <summary>
        /// Quantidade máxima de conexões.
        /// </summary>
        public static int MaxConnections { get; set; }

        /// <summary>
        /// Sleep do loop principal.
        /// </summary>
        public static int Sleep { get; set; }

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

        #region Game Configuration
        public static int MaxCharacters { get; set; }
        public static int MaxConversationOptions { get; set; }
        public static int MaxAnimationLayer { get; set; }
        public static int MaxAnimations { get; set; }
        public static int MaxConversations { get; set; }
        public static int MaxItems { get; set; }
        public static int MaxMaps { get; set; }
        public static int MaxNpcs { get; set; }
        public static int MaxMapNpcs { get; set; }
        public static int MaxInventories { get; set; }
        #endregion

        #region Character Configuration

        /// <summary>
        /// Permite a criação de personagens.
        /// </summary>
        public static bool CharacterCreation { get; set; }

        /// <summary>
        /// Permite a exclusão de personagens.
        /// </summary>
        public static bool CharacterDelete { get; set; }

        /// <summary>
        /// Level mínimo para exclusão.
        /// </summary>
        public static int CharacterDeleteMinLevel { get; set; }

        /// <summary>
        /// Level máximo para exclusão.
        /// </summary>
        public static int CharacterDeleteMaxLevel { get; set; }

        /// <summary>
        /// Impede que outras sprites sejam escolhidas por edição de pacotes.
        /// O usuário somente pode escolher entre o valor mínimo e máximo.
        /// </summary>
        public static int SpriteRangeMinimum { get; set; }

        /// <summary>
        /// Impede que outras sprites sejam escolhidas por edição de pacotes.
        /// O usuário somente pode escolher entre o valor mínimo e máximo.
        /// </summary>
        public static int SpriteRangeMaximum { get; set; }

        #endregion

        private const string Enabled = "Enabled";
        private const string Disabled = "Disabled";
        private static Settings settings;

        public static void Open() {
            settings = new Settings();
            settings.ParseConfigFile("GameConfig.txt");
        }

        public static void Close() {
            settings.Clear();
            settings = null;
        }

        public static void GetGeneralConfig() {
            Port = settings.GetInt32("Port");
            MaxConnections = settings.GetInt32("MaximumConnections");
            Sleep = settings.GetInt32("Sleep");

            IpBlockList.Enabled = settings.GetBoolean("IpBlock");
            IpBlockList.IpBlockTime = settings.GetInt32("IpBlockLifeTime");

            CheckAccessTime = settings.GetInt32("FilterCheckAccessTime");
            IpLifetime = settings.GetInt32("FilterIpLifeTime");
            IpMaxAttempt = settings.GetInt32("IpMaxAttempt");
            IpMaxAccessCount = settings.GetInt32("IpMaxAccessCount");

            LoginPort = settings.GetInt32("LoginPort");
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
            Global.WriteLog(LogType.System, "Trying to connect database", LogColor.Green);

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

            Global.WriteLog(LogType.System, $"Port: {Port}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Maximum Connection: {MaxConnections}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Sleep: {Sleep}", LogColor.Black);

            result = (UseEmailAsLogin) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"UseEmailAsLogin: {result}", LogColor.BlueViolet);

            result = (IpBlockList.Enabled) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"IpBlock: {result}", LogColor.BlueViolet);

            Global.WriteLog(LogType.System, $"IpBlockLifeTime: {IpBlockList.IpBlockTime}", LogColor.Black);

            Global.WriteLog(LogType.System, $"FilterCheckAccessTime: {CheckAccessTime}", LogColor.Black);
            Global.WriteLog(LogType.System, $"FilterIpLifeTime: {IpLifetime}", LogColor.Black);
            Global.WriteLog(LogType.System, $"IpMaxAttempt: {IpMaxAttempt}", LogColor.Black);
            Global.WriteLog(LogType.System, $"IpMaxAccessCount: {IpMaxAccessCount}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Login Server Port: {LoginPort}", LogColor.Black);
        }

        public static void ShowCharacterConfigInLog() {
            var result = (CharacterCreation) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"Create Character: {result}", LogColor.Black);
            result = (CharacterDelete) ? Enabled : Disabled;
            Global.WriteLog(LogType.System, $"Delete Character: {result}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Delete Min Level: {CharacterDeleteMinLevel}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Delete Min Level: {CharacterDeleteMaxLevel}", LogColor.Black);

            Global.WriteLog(LogType.System, $"Sprite Range Min: {SpriteRangeMinimum}", LogColor.Black);
            Global.WriteLog(LogType.System, $"Sprite Range Max: {SpriteRangeMaximum}", LogColor.Black);
        }
    }
}