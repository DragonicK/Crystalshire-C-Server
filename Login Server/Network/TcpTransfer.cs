using System;
using System.Net.Sockets;
using LoginServer.Communication;
using LoginServer.Network.PacketList.Game;
using Elysium.Logs;

namespace LoginServer.Network {
    /// <summary>
    /// Cliente para conectar ao Game Server e realizar a transferência dos dados do usuário.
    /// </summary>
    public sealed class TcpTransfer {
        public int GamePort { get; set; }
        public string GameIpAddress { get; set; }
        public bool Connected { get; private set; }

        /// <summary>
        /// Tempo entre cada tentativa de conexão.
        /// </summary>
        private const int ConnectTime = 5000;
        private const int PingTime = 5000;

        private bool disposed;
        private TcpClient client;
        private bool lastState;
        private int tick;
        private int pingTick;

        // Pacote para o ping.
        private ByteBuffer ping;

        public void InitClient() {
            client = new TcpClient {
                NoDelay = true
            };

            ping = new SpPing().Build();

            tick = Environment.TickCount;
            pingTick = tick;
        }

        public void Connect() {
            if (Environment.TickCount >= tick + ConnectTime) {
                tick = Environment.TickCount;

                if (!Connected) {
                    if (disposed) {
                        client = new TcpClient {
                            NoDelay = true
                        };

                        disposed = false;
                    }

                    try {
                        client.Connect(GameIpAddress, GamePort);
                        Connected = true;
                    }
                    catch (SocketException ex) {
                        // SocketException foi definido para o campo não ficar vazio.                  
                        if (ex.SocketErrorCode == SocketError.Success) {
                            Global.WriteLog(LogType.Connection, $"Connect Error: Class {GetType().Name}", LogColor.Red);
                            Global.WriteLog(LogType.Connection, $"Message: {ex.SocketErrorCode.ToString()}", LogColor.Red);
                        }

                        Disconnect();
                    }
                }

                ChangeState();
            }
        }

        public void Disconnect() {
            client.Close();
            Connected = false;
            disposed = true;

            ChangeState();
        }

        public void Send(ByteBuffer msg) {
            var buffer = new byte[msg.Length() + 4];
            var values = BitConverter.GetBytes(msg.Length());

            Array.Copy(values, 0, buffer, 0, 4);
            Array.Copy(msg.ToArray(), 0, buffer, 4, msg.Length());

            if (client.Client.Poll(Constants.SendTimeOut, SelectMode.SelectWrite)) {
                try {
                    client.Client.Send(buffer, SocketFlags.None);
                }
                catch (SocketException ex) {
                    // SocketException foi definido para o campo não ficar vazio.                  
                    if (ex.SocketErrorCode == SocketError.Success) {
                        Global.WriteLog(LogType.Connection, $"Send Data Error: Class {GetType().Name}", LogColor.Red);
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
                    Send(ping);
                }

                pingTick = Environment.TickCount + PingTime;
            }
        }

        /// <summary>
        /// Exibe a alteração no log quando o estado de conexão é alterado.
        /// </summary>
        private void ChangeState() {
            if (Connected != lastState) {

                if (Connected) {
                    Global.WriteLog(LogType.System, "Game Server is connected", LogColor.Green);             
                }
                else {
                    Global.WriteLog(LogType.System, "Game Server is disconnected", LogColor.BlueViolet);
                }

                lastState = Connected;
            }
        }
    }
}