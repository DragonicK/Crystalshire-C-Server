using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using LoginServer.Common;
using LoginServer.Communication;
using Elysium.Logs;

namespace LoginServer.Network {
    public sealed class TcpServer {
        public Dictionary<int, Connection> Connections;
        public IpFiltering IpFiltering { get; set; }
        public int Port { get; set; }

        private int highIndex;
        private bool accept;
        private TcpListener server;

        public TcpServer() { }

        public TcpServer(int port) {
            Port = port;
        }

        public void InitServer() {
            Connections = new Dictionary<int, Connection>();
            server = new TcpListener(IPAddress.Any, Port);
            server.Start();

            accept = true;
        }

        public void AcceptClient() {
            if (accept) {
                if (server.Pending()) {

                    var client = server.AcceptTcpClient();
                    var ipAddress = client.Client.RemoteEndPoint.ToString();

                    // Retira o número da porta.
                    ipAddress = ipAddress.Remove(ipAddress.IndexOf(':'));

                    if (!IsValidIpAddress(ipAddress)) {
                        Global.WriteLog(LogType.Connection, "Hacking attempt: Ip Address is not valid", LogColor.DarkViolet);
                        client.Close();

                        return;
                    }
                    else {
                        Global.WriteLog(LogType.Connection, $"Incoming connection {ipAddress}", LogColor.Coral);
                    }
                    
                    if (CanApproveIpAddress(ipAddress)) {
                        Add(client, ipAddress);
                    }
                    else {
                        Global.WriteLog(LogType.Connection, $"Refused connection {ipAddress}", LogColor.Coral);
                        client.Close();
                    }
                }

            }
        }

        public void Stop() {
            accept = false;
            server.Stop();

            Connections.Clear();
            Connections = null;
        }

        public void Remove(int index) {
            // Verifica a key para não ocorrer erros.
            if (Connections.ContainsKey(index)) {
                // Realiza a desconexao quando nao esta nulo.
                if (Connections[index].Connected) {
                    Connections[index]?.Disconnect();
                }

                Connections.Remove(index);
            }
        }

        public void Clear() {
            Connections.Clear();
        }

        public void ReceiveData() {
            for(var n = 1; n <= highIndex; n++) {
                if (Connections.ContainsKey(n)) {
                    Connections[n].ReceiveData();
                }
            }
        }

        public void RemoveInvalidConnections() {
            var remove = false;
            // Uma conexão JAMAIS pode permanecer ativa no servidor.
            // Quando a conexão é aceita, os dados são processados.
            // E em seguida, a conexão é fechada por não ter mais utilidade.

            for (int n = 1; n <= highIndex; n++) {
                remove = false;

                if (Connections.ContainsKey(n)) {

                    // Realiza a contagem de tempo.
                    Connections[n].CountConnectionTime();

                    // Quando o limite estipulado é ultrapasasdo, remove a conexão.
                    if (Connections[n].ConnectedTime >= Constants.ConnectionTimeOut) {
                        Connections[n].Disconnect();
                        remove = true;
                    }

                    if (!Connections[n].Connected) {
                        remove = true;
                    }

                    if (remove) {
                        Remove(n);
                    }
                }
            }
        }

        private bool CanApproveIpAddress(string ipAddress) {
            var approve = true;

            if (GeoIpBlock.Enabled) {
                if (GeoIpBlock.IsCountryIpBlocked(ipAddress)) {
                    var country = GeoIpBlock.FindCountryByIp(ipAddress);
                    Global.WriteLog(LogType.Connection, $"Banned country trying to connect from [{ipAddress}] Country [{country.Country}-{country.Code}]", LogColor.DarkRed);
                    approve = false;
                }
            }

            if (IpBlockList.Enabled) {
                if (IpBlockList.IsIpBlocked(ipAddress)) {
                    Global.WriteLog(LogType.Connection, $"Warning: Attempted IP Banned[{ipAddress}]", LogColor.DarkRed);
                    approve = false;
                }
                else {
                    if (IpFiltering.CanBlockIpAddress(ipAddress)) {
                        IpBlockList.AddIpAddress(ipAddress, false);
                        Global.WriteLog(LogType.Connection, $"Warning: [{ipAddress}] has been blocked", LogColor.BlueViolet);
                        approve = false;
                    }
                }
            }

            return approve;
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

        private void Add(TcpClient client, string ipAddress) {
            var index = 0;
            var uniqueKey = new KeyGenerator().GetUniqueKey();

            if (Connections.Count < highIndex) {
                // Procura por um slot que não está sendo usado.
                for (var i = 1; i <= highIndex; i++) {
                    if (!Connections.ContainsKey(i)) {
                        index = i;
                        break;
                    }
                }
            }
            // Caso contrário, adiciona um novo slot.
            else {
                index = ++highIndex;
            }

            Connections.Add(index, new Connection(index, client, uniqueKey));
        }
    }
}