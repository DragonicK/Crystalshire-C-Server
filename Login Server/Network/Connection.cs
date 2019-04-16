using System;
using System.Net.Sockets;
using LoginServer.Communication;
using Elysium.Logs;

namespace LoginServer.Network {
    public sealed class Connection : IConnection {
        /// <summary>
        /// Chave única de identificação no sistema.
        /// </summary>
        public string UniqueKey { get; set; }

        /// <summary>
        /// Id de indice de conexão.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Tempo de conexão em segundos.
        /// </summary>
        public int ConnectedTime { get; set; }

        public bool Connected { get; private set; }

        public string IpAddress { get; set; }

        private TcpClient Client;
        private ByteBuffer msg;

        private bool lastState;
        private int timeTick;
        private const int OneSecond = 1000;

        ~Connection() {
            Client.Close();
            Client = null;
            msg = null;
        }

        public Connection(int index, TcpClient tcpClient, string uniqueKey) {
            timeTick = Environment.TickCount;
            msg = new ByteBuffer();

            Index = index;
            Client = tcpClient;
            Client.NoDelay = true;

            UniqueKey = uniqueKey;
            Connected = true;

            IpAddress = tcpClient.Client.RemoteEndPoint.ToString();
            ChangeState();
        }

        public void Disconnect() {
            Connected = false;
            Client.Close();
            ChangeState();
        }

        public void ReceiveData() {
            if (Client.Client == null) {
                return;
            }

            if (Client.Available > 0) {
                var size = Client.Available;
                byte[] buffer = new byte[size];

                if (Client.Client.Poll(Constants.ReceiveTimeOut, SelectMode.SelectRead)) {
                    try {
                        Client.Client.Receive(buffer, size, SocketFlags.None);
                    }
                    catch (SocketException ex) {
                        Global.WriteLog(LogType.Connection, $"Receive Data Error: Class {GetType().Name}", LogColor.Red);
                        Global.WriteLog(LogType.Connection, $"Message: {ex.Message}", LogColor.Red);
                        Disconnect();
                        return;
                    }
                }
                else {
                    Disconnect();
                    return;
                }

                int pLength = 0;
                msg.Write(buffer);

                if (msg.Length() >= 4) {
                    pLength = msg.ReadInt32(false);

                    if (pLength < 0) {
                        return;
                    }
                }

                while (pLength > 0 && pLength <= msg.Length() - 4) {
                    if (pLength <= msg.Length() - 4) {
                        // Remove the first packet (Size of Packet).
                        msg.ReadInt32();
                        // Remove the header.
                        var header = msg.ReadInt32();
                        // Decrease 4 bytes of header.
                        pLength -= 4;

                        if (OpCode.RecvPacket.ContainsKey(header)) {
                            ((IRecvPacket)Activator.CreateInstance(OpCode.RecvPacket[header])).Process(msg.ReadBytes(pLength), this);
                        }
                        else {
                            Global.WriteLog(LogType.Connection, $"Header: {header} was not found", LogColor.Red);
                            Disconnect();
                        }
                    }

                    pLength = 0;

                    if (msg.Length() >= 4) {
                        pLength = msg.ReadInt32(false);

                        if (pLength < 0) {
                            return;
                        }
                    }
                }

                msg.Trim();
            }
        }

        public void Send(ByteBuffer msg, string name) {
            if (Client.Client == null) {
                return;
            }

            var buffer = new byte[msg.Length() + 4];
            var values = BitConverter.GetBytes(msg.Length());

            Array.Copy(values, 0, buffer, 0, 4);
            Array.Copy(msg.ToArray(), 0, buffer, 4, msg.Length());



            if (Client.Client.Poll(Constants.SendTimeOut, SelectMode.SelectWrite)) {
                try {
                    Client.Client.Send(buffer, SocketFlags.None);
                }
                catch (SocketException ex) {
                    // SocketException foi definido para o campo não ficar vazio.                  
                    if (ex.SocketErrorCode == SocketError.Success) {
                        Global.WriteLog(LogType.Connection, $"Send Data Error: Class {name}", LogColor.Red);
                        Global.WriteLog(LogType.Connection, $"Message: {ex.SocketErrorCode.ToString()}", LogColor.Red);
                    }

                    Disconnect();
                }
            }
            else {
                Disconnect();
            }
        }

        /// <summary>
        /// Realiza a contagem do tempo da conexão.
        /// </summary>
        public void CountConnectionTime() {
            if (Environment.TickCount >= timeTick + OneSecond) {
                ConnectedTime++;
                timeTick = Environment.TickCount;
            }
        }

        /// <summary>
        /// Exibe a alteração no log quando o estado de conexão é alterado.
        /// </summary>
        private void ChangeState() {
            if (Connected != lastState) {

                if (Connected) {
                    Global.WriteLog(LogType.Connection, $"Index: {Index} Key {UniqueKey} {IpAddress} is connected", LogColor.Coral);
                }
                else {
                    Global.WriteLog(LogType.Connection, $"Index: {Index} Key {UniqueKey} {IpAddress} is disconnected", LogColor.Coral);
                }

                lastState = Connected;
            }
        }
    }
}