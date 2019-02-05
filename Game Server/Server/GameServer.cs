using System;
using GameServer.Data;
using GameServer.Script;
using GameServer.Network;
using GameServer.Communication;
using GameServer.Server.Character;
using Elysium.Logs;

namespace GameServer.Server {
    public sealed class GameServer : Global{
        public bool ServerRunning { get; set; }

        /// <summary>
        /// Indica que o loop pode ser processado.
        /// </summary>
        public bool Initialized { get; set; }
        public Action<int> UpdateUps;

        TcpLogin Login;
        TcpServer Server;
        IpFiltering IpFiltering;

        private int tick;
        private int count;
        private int ups;

        public void InitServer() {
            Configuration.Open();
            Configuration.GetGeneralConfig();
            Configuration.GetDatabaseConfig();
            Configuration.Close();

            Configuration.ShowConfigInLog();
            Configuration.CheckDatabaseConnection();

            // Cria a instância e registra o delegate.
            DeleteRequest = new CharacterDeleteRequest();
            DeleteRequest.DeleteCharacter += DeleteCharacter;

            IpBlockList.Initialize();

            IpFiltering = new IpFiltering() {
                CheckAccessTime = Configuration.CheckAccessTime,
                IpLifetime = Configuration.IpLifetime,
                IpMaxAccessCount = Configuration.IpMaxAccessCount,
                IpMaxAttempt = Configuration.IpMaxAttempt
            };

            WriteLog(LogType.System, "Initializing scripts", LogColor.Blue);
            var lua = new LuaScript();
            lua.InitializeScript();

            WriteLog(LogType.System, "Initializing prohibited names", LogColor.Blue);
            lua.LoadProhibitedNames();
  
            WriteLog(LogType.System, "Initializing character config", LogColor.Blue);
            lua.LoadCharacterConfiguration();
            Configuration.ShowCharacterConfigInLog();

            WriteLog(LogType.System, "Initializing game config", LogColor.Blue);
            lua.LoadGameConfiguration();

            WriteLog(LogType.System, "Initializing classes", LogColor.Blue);
            lua.LoadClasses();

            DataManagement.InitializeData();

            WriteLog(LogType.System, "Initializing map instances", LogColor.Blue);
            InitializeMaps();

            OpCode.Initialize();

            Login = new TcpLogin() {
                Port = Configuration.LoginPort
            };

            Login.InitServer();

            WriteLog(LogType.System, "Waiting for Login Server conenction", LogColor.Black);

            Server = new TcpServer() {
                Port = Configuration.Port,
                IpFiltering = IpFiltering
            };

            Server.InitServer();
            WriteLog(LogType.System, "Game Server started", LogColor.Green);

            Initialized = true;
        }

        public void StopServer() {
            Server.Stop();
            Login.Stop();

            DataManagement.ClearDatabases();
            Authentication.Clear();

            IpFiltering.Clear();
            IpBlockList.Clear();

            CloseLog();
        }

        public void ServerLoop() {
            // Aceita a conexão do servidor de login.
            Login.AcceptClient();
            // Recebe os dados.
            Login.ReceiveData();
            // Verifica o estado da conexão enviando um ping.
            Login.SendPing();

            // Aceita novas conexões.
            Server.AcceptClient();
            // Processa os dados das conexões.
            Server.ReceiveData();
            // Verifica o estado da conexão enviando um ping.
            Server.SendPing();
            // Verifica o tempo limite de cada conexão.
            Server.CheckConnectionTimeOut();

            ProcessMaps();

            // Verifica por exclusões.
            DeleteRequest.CheckForDeletedCharacters(); 

            IpFiltering.RemoveExpiredIpAddress();
            IpBlockList.RemoveExpiredIpAddress();

            CountUps();
        }

        private void ProcessMaps() {
            for (var i = 1; i <= Configuration.MaxMaps; i++) {
                Maps[i].UpdateLogic();
            }
        }
 
        private void CountUps() {
            if (Environment.TickCount >= tick + 1000) {
                ups = count;
                count = 0;
                tick = Environment.TickCount;

                UpdateUps?.Invoke(ups);
            }
            else {
                count++;
            }
        }
    }
}