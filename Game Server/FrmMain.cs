using System;
using System.Drawing;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer {
    public partial class FrmMain : Form {

        #region Peek Message
        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct Message {
            public IntPtr hWnd;
            public IntPtr msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

        public void OnApplicationIdle(object sender, EventArgs e) {
            while (AppStillIdle) {

                // Quando estiver inicializando.     
                if (Server.Initialized) {
                    
                    // Processa
                    if (Server.ServerRunning) {
                        Server.ServerLoop();

                        if (Configuration.Sleep > 0) {
                            Thread.Sleep(Configuration.Sleep);
                        }
                    }
                    else {
                        Server.StopServer();
                        Application.Exit();
                    }
                }
            }   
        }

        private bool AppStillIdle {
            get {
                return !PeekMessage(out Message msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        #endregion

        Server.GameServer Server;
        FrmPlayers frmPlayers;

        private const int MaxLogsLines = 250;
        private const int PreserveLogsLines = 25;

        public FrmMain() {
            InitializeComponent();

            frmPlayers = new FrmPlayers();
        }

        public void InitializeServer() {
            Global.OpenLog(WriteLog);

            Server = new Server.GameServer();
            Server.UpdateUps += UpdateUps;
            Server.InitServer();

            Server.ServerRunning = true;
        }
       
        private void WriteLog(object sender, LogEventArgs e) {
            var text = TextSystem;

            switch ((LogType)e.Index) {
                case LogType.System:
                    text = TextSystem;
                    break;
                case LogType.Player:
                    text = TextPlayer;
                    break;
                case LogType.Game:
                    text = TextGame;
                    break;
                case LogType.Chat:
                    text = TextChat;
                    break;
                case LogType.Connection:
                    text = TextConnection;
                    break;
            }

            text.SelectionStart = text.TextLength;
            text.SelectionLength = 0;
            text.SelectionColor = e.Color;
            text.AppendText($"{DateTime.Now}: {e.Text}{Environment.NewLine}");
            text.ScrollToCaret();
        }

        private void UpdateUps(int ups) {
            Text = $"Crystalshire - Game Server @ {ups} Ups";
        }

        private void TimerClear_Tick(object sender, EventArgs e) {
            if (TextPlayer.Lines.Length >= MaxLogsLines) {
                var currentLines = TextPlayer.Lines;
                var newLines = new string[PreserveLogsLines];

                Array.Copy(currentLines, currentLines.Length - PreserveLogsLines, newLines, 0, PreserveLogsLines);

                TextPlayer.Lines = newLines;

                currentLines = null;
            }
        }

        private void MenuExit_Click(object sender, EventArgs e) {
            Server.ServerRunning = false;
        }

        private void MenuPlayers_Click(object sender, EventArgs e) {
            if (frmPlayers == null || frmPlayers.IsDisposed) {
                frmPlayers = new FrmPlayers();
            }

            frmPlayers.Show();
        }
    }
}