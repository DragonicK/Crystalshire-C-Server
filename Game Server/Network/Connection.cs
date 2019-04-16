using System;
using System.Net.Sockets;
using GameServer.Communication;
using GameServer.Network.PacketList;
using Elysium.Logs;

namespace GameServer.Network {
    public sealed class Connection : IConnection {
        public Action<int> OnDisconnect { get; set; }
        public bool Authenticated { get; set; }
        public bool Connected { get; set; }
        public int ConnectedTime { get; private set; }
        public string IpAddress { get; set; }
        public int Index { get; set; }

        private TcpClient Client;
        private ByteBuffer msg;
        private SpPing ping;

        private bool lastState;
        private int pingTick;
        private const int OneSecond = 1000;

        private int tick;
        private int time;

        ~Connection() {
            Client.Close();
            Client = null;
            msg = null;
        }

        public Connection(int index, TcpClient tcpClient, string ipAddress) {
            msg = new ByteBuffer();
            ping = new SpPing();

            Client = tcpClient;
            Client.NoDelay = false;

            Index = index;
            Connected = true;
            IpAddress = ipAddress;

            ChangeState();
        }

        public void Disconnect() {
            Connected = false;
            Client.Close();
            ChangeState();
            msg.Flush();

            OnDisconnect?.Invoke(Index);
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
                        // Recebe os primeiros dados.
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
                        var t_size  = msg.ReadInt32();
                        // Remove the header.
                        var header = msg.ReadInt32();
                        // Decrease 4 bytes of header.
                        pLength -= 4;

                        if (Enum.IsDefined(typeof(ClientPacket), header)) {
                            if (OpCode.RecvPacket.ContainsKey((ClientPacket)header)) {

                                // Se os dados do usuário estiverem atuenticados, processa dos pacotes.
                                if (Authenticated) {
                                    ((IRecvPacket)Activator.CreateInstance(OpCode.RecvPacket[(ClientPacket)header])).Process(msg.ReadBytes(pLength), this);
                                }
                                // Do contrário, redireciona para o a autenticação do usuário.
                                // Caso algum pacote esteja fora de ordem ou com informações incorretas.
                                // A desconexão irá ocorrer.
                                else {
                                    ((IRecvPacket)Activator.CreateInstance(OpCode.RecvPacket[ClientPacket.AuthLogin])).Process(msg.ReadBytes(pLength), this);
                                }
                            }

                        }
                        else {
                            Global.WriteLog(LogType.System, $"Header: {header} was not found", LogColor.Red);
                            msg.Flush();
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

                    msg.Trim();
                }
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

        public void SendPing() {
            if (Environment.TickCount >= pingTick) { 

                if (Connected) {
                    ping.Send(this);
                }

                pingTick = Environment.TickCount + Constants.PingTime;
            }
        }

        public void CheckConnectionTimeOut() {
            if (!Authenticated) {
                if (Environment.TickCount >= tick) {
                    time++;

                    if (time >= Constants.ConnectionTimeOut) {
                        Disconnect();
                    }

                    tick = Environment.TickCount + OneSecond;
                }
            }
        }

        private void ReadData(ref NetworkStream stream) {

        }

        /// <summary>
        /// Exibe a alteração no log quando o estado de conexão é alterado.
        /// </summary>
        private void ChangeState() {
            if (Connected != lastState) {

                if (Connected) {
                    Global.WriteLog(LogType.Connection, $"Index: {Index} {IpAddress} is connected", LogColor.Coral);
                }
                else {
                    Global.WriteLog(LogType.Connection, $"Index: {Index} {IpAddress} is disconnected", LogColor.Coral);
                }

                lastState = Connected;
            }
        }
    }
}