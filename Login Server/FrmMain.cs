using System;
using System.Drawing;
using System.Security;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using Elysium.Logs;
using LoginServer.Communication;
using LoginServer.Server;
using LoginServer.Script;

namespace LoginServer {
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
            while (this.AppStillIdle) {

                if (loginServer.Initialized) {
                    // Processa
                    if (loginServer.ServerRunning) {
                        loginServer.ServerLoop();

                        if (Configuration.Sleep > 0) {
                            Thread.Sleep(Configuration.Sleep);
                        }
                    }
                    else {
                        loginServer.StopServer();

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

        Login loginServer;
        int ThreadSleepBackup;

        FrmBlockList frmBlockList;
        FrmConnections frmConnections;
  
        public FrmMain() {
            InitializeComponent();
        }

        public void InitializeServer() {
            Global.OpenLog(WriteLog);

            loginServer = new Login();
            loginServer.UpdateUps += UpdateUps;
            loginServer.InitServer();

            loginServer.ServerRunning = true;
        }

        /// <summary>
        /// Adiciona as informações de logs na tela.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WriteLog(object sender, LogEventArgs e) {
            RichTextBox textBox = null;

            switch ((LogType)e.Index) {
                case LogType.System:
                    textBox = TextSystem;
                    break;
                case LogType.Connection:
                    textBox = TextConnection;
                    break;
                case LogType.User:
                    textBox = TextUser;
                    break;
            }

            textBox.SelectionStart = textBox.TextLength;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = e.Color;
            textBox.AppendText($"{DateTime.Now}: {e.Text}{Environment.NewLine}");
            textBox.ScrollToCaret();
        }

        private void UpdateUps(int ups) {
            Text = $"Crystalshire - Authentication @ {ups} Ups";
        }

        private void MenuDisableLogin_Click(object sender, EventArgs e) {
            Configuration.Maintenance = MenuDisableLogin.Checked;

            var result = (Configuration.Maintenance) ? "Disabled" : "Enabled";
            Global.WriteLog(LogType.System, $"User Login: {result}", LogColor.DarkViolet);
        }

        private void MenuExit_Click(object sender, EventArgs e) {
            loginServer.ServerRunning = false;
        }

        private void MenuConnections_Click(object sender, EventArgs e) {
            if (frmConnections == null || frmConnections.IsDisposed) {
                frmConnections = new FrmConnections();
            }

            frmConnections.ShowForm(loginServer.Connections);
        }

        private void MenuReloadBlockList_Click(object sender, EventArgs e) {
            var lua = new LuaScript();
            lua.ReloadBlockList();
        }

        private void MenuReloadChecksum_Click(object sender, EventArgs e) {
            var lua = new LuaScript();
            lua.ReloadChecksum();
        }

        private void MenuReloadConfig_Click(object sender, EventArgs e) {
            Configuration.Open();
            Configuration.GetGeneralConfig();
            Configuration.Close();

            Configuration.ShowConfigInLog();
        }

        private void MenuBlockList_Click(object sender, EventArgs e) {
            if (frmBlockList == null || frmBlockList.IsDisposed) {
                frmBlockList = new FrmBlockList();
            }

            frmBlockList.Show();
        }

        private void MenuThreadSleep_Click(object sender, EventArgs e) {
            var value = MenuThreadSleep.Checked;

            if (value) {
                ThreadSleepBackup = Configuration.Sleep;
                Configuration.Sleep = 0;
            }
            else {
                Configuration.Sleep = ThreadSleepBackup;
                ThreadSleepBackup = 0;
            }
        }
    }
}