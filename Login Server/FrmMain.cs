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
        Thread t;
        Login loginServer;
        int ThreadSleepBackup;
        bool ServerRunning;

        FrmBlockList frmBlockList;
        FrmConnections frmConnections;

        private delegate void DelegateSetText(int ups);
        private delegate void DelegateWriteLog(object sender, LogEventArgs e);

        public FrmMain() {
            InitializeComponent();
        }

        public void InitializeServer() {
            ServerRunning = true;

            Global.OpenLog(WriteLog);

            loginServer = new Login();
            loginServer.UpdateUps += UpdateUps;
            loginServer.InitServer();

            t = new Thread(ServerLoop);
            t.Start();
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

            if (textBox.InvokeRequired) {
                var d = new DelegateWriteLog(WriteLog);
                textBox.Invoke(d, sender, e);
            }
            else {
                textBox.SelectionStart = textBox.TextLength;
                textBox.SelectionLength = 0;
                textBox.SelectionColor = e.Color;
                textBox.AppendText($"{DateTime.Now}: {e.Text}{Environment.NewLine}");
                textBox.ScrollToCaret();
            }
        }

        private void UpdateUps(int ups) {
            if (InvokeRequired) {
                var d = new DelegateSetText(UpdateUps);
                Invoke(d, ups);
            }
            else {
                Text = $"Crystalshire - Authentication @ {ups} Ups";
            }
        }

        private void MenuDisableLogin_Click(object sender, EventArgs e) {
            Configuration.Maintenance = MenuDisableLogin.Checked;

            var result = (Configuration.Maintenance) ? "Disabled" : "Enabled";
            Global.WriteLog(LogType.System, $"User Login: {result}", LogColor.DarkViolet);
        }

        private void MenuExit_Click(object sender, EventArgs e) {
            ServerRunning = false;
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e) {
            ServerRunning = false;
        }

        private void ServerLoop() {
            while (ServerRunning) {

                loginServer.ServerLoop();

                if (Configuration.Sleep > 0) {
                    Thread.Sleep(Configuration.Sleep);
                }
            }

            loginServer.StopServer();
            Application.Exit();
        }
    }
}