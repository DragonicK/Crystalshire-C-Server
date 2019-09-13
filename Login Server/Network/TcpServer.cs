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

        private bool accept;
        private TcpListener server;

        public TcpServer() { }

        public TcpServer(int port) {
            Port = port;
        }

        public void InitServer() {
            Connections = new Dictionary<int, Connection>(Configuration.MaxConnections);

            for (var i = 1; i <= Configuration.MaxConnections; i++) {
                Connections.Add(i, null);
            }

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
                        Add(client);
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
        }

        public void Clear() {
            Connections.Clear();
        }

        public void ProcessClients() {
            for (var i = 1; i <= Configuration.MaxConnections; i++) {
                if (Connections[i] != null) {
                    Connections[i].ReceiveData();
                    RemoveInvalidConnections(i);
                }
            }
        }

        private void RemoveInvalidConnections(int index) {
            Connections[index].CountConnectionTime();

            if (Connections[index].ConnectedTime >= Constants.ConnectionTimeOut) {
                Connections[index].Disconnect();
            }

            if (!Connections[index].Connected) {
                Connections[index] = null;
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

        private void Add(TcpClient client) {
            var uniqueKey = new KeyGenerator().GetUniqueKey();

            for (var i = 1; i <= Configuration.MaxConnections; i++) {
                if (Connections[i] == null) {
                    Connections[i] = new Connection(i, client, uniqueKey);
                    return;
                }
            }

            // Fecha a conexão quando um slot não foi encontrado.
            client.Close();
        }
    }
}