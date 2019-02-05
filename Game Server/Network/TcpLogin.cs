using System.Net;
using System.Linq;
using System.Net.Sockets;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network {
    /// <summary>
    /// Responsavel por receber os dados do Login Server.
    /// </summary>
    public sealed class TcpLogin {
        public int Port { get; set; }

        public bool Connected {
            get {
                if (Connection != null) {
                    return Connection.Connected;
                }

                return false;
            }
        }

        public string IpAddress { get; set; }

        private bool lastState;
        private bool accept;
        private TcpListener server;

        /// <summary>
        /// Conexão única para o Login Server.
        /// </summary>
        private Connection Connection;

        public TcpLogin() {

        }

        public void Disconnect() {
            if (Connection != null) {
                Connection.Disconnect();
                ChangeState();
            }
        }

        public void InitServer() {
            server = new TcpListener(IPAddress.Any, Port) {
                ExclusiveAddressUse = true
            };

            server.Start();
            accept = true;
        }

        public void AcceptClient() {
            if (accept) {
                if (server.Pending()) {

                    // Se estiver pedindo uma nova conexão.
                    // Desconecta para refazer a conexão.
                    if (Connection != null) {
                        if (Connection.Connected) {
                            Connection.Disconnect();
                        }
                    }

                    var client = server.AcceptTcpClient();

                    IpAddress = client.Client.RemoteEndPoint.ToString();
                    IpAddress = IpAddress.Remove(IpAddress.IndexOf(':'));

                    Connection = new Connection(0, client, IpAddress) {
                        Authenticated = true
                    };

                    ChangeState();
                }
            }
        }

        public void Stop() {
            accept = false;
            server.Stop();
        }

        public void ReceiveData() {
            if (Connection != null) {
                if (Connection.Connected) {
                    Connection.ReceiveData();
                }
            }
        }

        /// <summary>
        /// Envia um ping para determinar o estado da conexão.
        /// </summary>
        public void SendPing() {
            if (Connection != null) {
                Connection.SendPing();
                ChangeState();
            }
        }

        /// <summary>
        /// Exibe a alteração no log quando o estado de conexão é alterado.
        /// </summary>
        private void ChangeState() {
            if (Connected != lastState) {

                if (Connected) {
                    Global.WriteLog(LogType.System, "Login Server is connected", LogColor.Green);
                }
                else {
                    Global.WriteLog(LogType.System, "Login Server is disconnected", LogColor.BlueViolet);
                }

                lastState = Connected;
            }
        }

        private bool IsValidIpAddress(string ipAddress) {
            const int IpAddressArraySplit = 4;

            if (string.IsNullOrWhiteSpace(ipAddress) || string.IsNullOrEmpty(ipAddress)) {
                return false;
            }

            var values = ipAddress.Split('.');
            if (values.Length != IpAddressArraySplit) {
                return false;
            }

            return values.All(r => byte.TryParse(r, out byte parsing));
        }
    }
}