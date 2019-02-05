using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network {
    public class TcpServer {
        public int Port { get; set; }
        public IpFiltering IpFiltering { get; set; }

        private Dictionary<int, IConnection> connections;
        private int highIndex;

        private bool accept;
        private TcpListener server;

        public TcpServer() {
            connections = new Dictionary<int, IConnection>();
        }

        public TcpServer(int port) {
            Port = port;
            connections = new Dictionary<int, IConnection>();
        }

        public void InitServer() {
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
                    }
                    else {
                        Add(client, ipAddress);
                    }
                }
            }
        }

        public void Stop() {
            accept = false;
            server.Stop();
        }

        public void Remove(int index) {
            if (connections.ContainsKey(index)) {
                connections.Remove(index);
            }
        }

        public void ReceiveData() {
            for (var n = 1; n <= highIndex; n++) {
                if (connections.ContainsKey(n)) {
                    connections[n].ReceiveData();
                }
            }
        }

        public void CheckConnectionTimeOut() {
            for (var n = 1; n <= highIndex; n++) {
                if (connections.ContainsKey(n)) {
                    connections[n].CheckConnectionTimeOut();
                }
            }
        }

        public void SendPing() {
            for (var n = 1; n <= highIndex; n++) {
                if (connections.ContainsKey(n)) {
                    connections[n].SendPing();
                }
            }
        }

        private void OnDisconnect(int index) {
            Remove(index);
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

            if (connections.Count < highIndex) {
                // Procura por um slot que não está sendo usado.
                for (var i = 1; i <= highIndex; i++) {
                    if (!connections.ContainsKey(i)) {
                        index = i;
                        break;
                    }
                }
            }
            else {
                index = ++highIndex;
            }

            var connection = new Connection(index, client, ipAddress);
            connection.OnDisconnect += OnDisconnect;

            connections.Add(index, connection);

            // Gambiarra, altera o highindex;
            Authentication.HighIndex = highIndex; 
        }
    }
}