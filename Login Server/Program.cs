using System;
using System.Windows.Forms;

namespace LoginServer {
    static class Program { 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var frmMain = new FrmMain();
            frmMain.Show();
            frmMain.InitializeServer();

            Application.Run(frmMain);
        }
    }
}