using System;
using System.Collections.Generic;
using Elysium.Logs;
using LoginServer.Communication;
using LoginServer.Network;
using LoginServer.Script;

namespace LoginServer.Server {
    public sealed class Login : Global {
        public Action<int> UpdateUps { get; set; }
        
        public Dictionary<int, Connection> Connections {
            get {
                return Server.Connections;
            }
        }

        /// <summary>
        /// Conexão geral do servidor de login.
        /// </summary>
        public TcpServer Server { get; set; }

        /// <summary>
        /// Connecta ao game server e envia os dados para conexão do usuário.
        /// </summary>
        public TcpTransfer TcpTransfer { get; set; }

        public IpFiltering IpFiltering { get; set; }

        // Fps Variaveis.
        private int count;
        private int tick;
        private int ups;

        public void InitServer() {
            Configuration.Open();
            Configuration.GetGeneralConfig();
            Configuration.GetDatabaseConfig();
            Configuration.Close();

            Configuration.ShowConfigInLog();
            Configuration.CheckDatabaseConnection();

            if (GeoIpBlock.Enabled) {
                WriteLog(LogType.System, "Loading GeoIp data country", LogColor.Black);
                GeoIpBlock.LoadData();
            }

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

            OpCode.Initialize();

            TcpTransfer = new TcpTransfer() {
                GameIpAddress = Configuration.GameServerIp,
                GamePort = Configuration.GameServerPort
            };

            TcpTransfer.InitClient();

            WriteLog(LogType.System, "Trying to connect to Game Server", LogColor.Black);

            Server = new TcpServer() {
                IpFiltering = IpFiltering,
                Port = Configuration.Port
            };

            Server.InitServer();

            // Delegate 
            SendGameServerPacket = SendGameServerData;

            WriteLog(LogType.System, "Login Server started", LogColor.Green);

        }

        public void StopServer() {
            Server.Stop();

            TcpTransfer.Disconnect();
            TcpTransfer = null;

            IpFiltering.Clear();

            Checksum.Clear();
            GeoIpBlock.Clear();
            IpBlockList.Clear();

            CloseLog();
        }

        public void ServerLoop() {
            TcpTransfer.Connect();
            TcpTransfer.SendPing();

            Server.AcceptClient();
            Server.ProcessClients();

            IpBlockList.RemoveExpiredIpAddress();
            IpFiltering.RemoveExpiredIpAddress();

            CountUps();
        }

        private void CountUps() {
            if (Environment.TickCount >= tick + 1000) {
                ups = count;
                tick = Environment.TickCount;
                count = 0;

                UpdateUps?.Invoke(ups);
            }
            else {
                count++;
            }
        }

        private void SendGameServerData(ByteBuffer msg) {
            if (TcpTransfer.Connected) {
                TcpTransfer.Send(msg);
            }
        }

    //End File
    }
}