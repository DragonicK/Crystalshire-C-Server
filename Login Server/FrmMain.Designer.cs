namespace LoginServer {
    partial class FrmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TabConfig = new System.Windows.Forms.TabControl();
            this.general_TabPage = new System.Windows.Forms.TabPage();
            this.TextSystem = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TextConnection = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TextUser = new System.Windows.Forms.RichTextBox();
            this.MenuServerStrip = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDisableLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReloadConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReloadBlockList = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReloadChecksum = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuThreadSleep = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuServer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBlockList = new System.Windows.Forms.ToolStripMenuItem();
            this.TabConfig.SuspendLayout();
            this.general_TabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.MenuServerStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabConfig
            // 
            this.TabConfig.Controls.Add(this.general_TabPage);
            this.TabConfig.Controls.Add(this.tabPage1);
            this.TabConfig.Controls.Add(this.tabPage2);
            this.TabConfig.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabConfig.Location = new System.Drawing.Point(16, 33);
            this.TabConfig.Name = "TabConfig";
            this.TabConfig.SelectedIndex = 0;
            this.TabConfig.Size = new System.Drawing.Size(534, 345);
            this.TabConfig.TabIndex = 2;
            // 
            // general_TabPage
            // 
            this.general_TabPage.BackColor = System.Drawing.SystemColors.Control;
            this.general_TabPage.Controls.Add(this.TextSystem);
            this.general_TabPage.Location = new System.Drawing.Point(4, 24);
            this.general_TabPage.Name = "general_TabPage";
            this.general_TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.general_TabPage.Size = new System.Drawing.Size(526, 317);
            this.general_TabPage.TabIndex = 0;
            this.general_TabPage.Text = "System";
            // 
            // TextSystem
            // 
            this.TextSystem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextSystem.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.TextSystem.Location = new System.Drawing.Point(3, 3);
            this.TextSystem.Name = "TextSystem";
            this.TextSystem.ReadOnly = true;
            this.TextSystem.Size = new System.Drawing.Size(520, 311);
            this.TextSystem.TabIndex = 0;
            this.TextSystem.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TextConnection);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(526, 317);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Connection";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TextConnection
            // 
            this.TextConnection.BackColor = System.Drawing.SystemColors.Control;
            this.TextConnection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextConnection.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.TextConnection.Location = new System.Drawing.Point(3, 3);
            this.TextConnection.Name = "TextConnection";
            this.TextConnection.ReadOnly = true;
            this.TextConnection.Size = new System.Drawing.Size(520, 311);
            this.TextConnection.TabIndex = 1;
            this.TextConnection.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TextUser);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(526, 317);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "User";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TextUser
            // 
            this.TextUser.BackColor = System.Drawing.SystemColors.Control;
            this.TextUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextUser.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.TextUser.Location = new System.Drawing.Point(3, 3);
            this.TextUser.Name = "TextUser";
            this.TextUser.ReadOnly = true;
            this.TextUser.Size = new System.Drawing.Size(520, 311);
            this.TextUser.TabIndex = 1;
            this.TextUser.Text = "";
            // 
            // MenuServerStrip
            // 
            this.MenuServerStrip.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuServerStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuMaintenance,
            this.configurationToolStripMenuItem,
            this.MenuServer});
            this.MenuServerStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuServerStrip.Name = "MenuServerStrip";
            this.MenuServerStrip.Size = new System.Drawing.Size(565, 24);
            this.MenuServerStrip.TabIndex = 3;
            this.MenuServerStrip.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExit});
            this.MenuFile.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(39, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(95, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // MenuMaintenance
            // 
            this.MenuMaintenance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDisableLogin});
            this.MenuMaintenance.Name = "MenuMaintenance";
            this.MenuMaintenance.Size = new System.Drawing.Size(94, 20);
            this.MenuMaintenance.Text = "Maintenance";
            // 
            // MenuDisableLogin
            // 
            this.MenuDisableLogin.CheckOnClick = true;
            this.MenuDisableLogin.Name = "MenuDisableLogin";
            this.MenuDisableLogin.Size = new System.Drawing.Size(153, 22);
            this.MenuDisableLogin.Text = "Disable Login";
            this.MenuDisableLogin.Click += new System.EventHandler(this.MenuDisableLogin_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuReloadConfig,
            this.MenuReloadBlockList,
            this.MenuReloadChecksum,
            this.MenuThreadSleep});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // MenuReloadConfig
            // 
            this.MenuReloadConfig.Name = "MenuReloadConfig";
            this.MenuReloadConfig.Size = new System.Drawing.Size(197, 22);
            this.MenuReloadConfig.Text = "Reload Login Config";
            this.MenuReloadConfig.Click += new System.EventHandler(this.MenuReloadConfig_Click);
            // 
            // MenuReloadBlockList
            // 
            this.MenuReloadBlockList.Name = "MenuReloadBlockList";
            this.MenuReloadBlockList.Size = new System.Drawing.Size(197, 22);
            this.MenuReloadBlockList.Text = "Reload Block List";
            this.MenuReloadBlockList.Click += new System.EventHandler(this.MenuReloadBlockList_Click);
            // 
            // MenuReloadChecksum
            // 
            this.MenuReloadChecksum.Name = "MenuReloadChecksum";
            this.MenuReloadChecksum.Size = new System.Drawing.Size(197, 22);
            this.MenuReloadChecksum.Text = "Reload Checksum";
            this.MenuReloadChecksum.Click += new System.EventHandler(this.MenuReloadChecksum_Click);
            // 
            // MenuThreadSleep
            // 
            this.MenuThreadSleep.CheckOnClick = true;
            this.MenuThreadSleep.Name = "MenuThreadSleep";
            this.MenuThreadSleep.Size = new System.Drawing.Size(197, 22);
            this.MenuThreadSleep.Text = "Disable Thread Sleep";
            this.MenuThreadSleep.Click += new System.EventHandler(this.MenuThreadSleep_Click);
            // 
            // MenuServer
            // 
            this.MenuServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuConnections,
            this.MenuBlockList});
            this.MenuServer.Name = "MenuServer";
            this.MenuServer.Size = new System.Drawing.Size(55, 20);
            this.MenuServer.Text = "Server";
            // 
            // MenuConnections
            // 
            this.MenuConnections.Name = "MenuConnections";
            this.MenuConnections.Size = new System.Drawing.Size(147, 22);
            this.MenuConnections.Text = "Connections";
            this.MenuConnections.Click += new System.EventHandler(this.MenuConnections_Click);
            // 
            // MenuBlockList
            // 
            this.MenuBlockList.Name = "MenuBlockList";
            this.MenuBlockList.Size = new System.Drawing.Size(147, 22);
            this.MenuBlockList.Text = "Block List";
            this.MenuBlockList.Click += new System.EventHandler(this.MenuBlockList_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 390);
            this.Controls.Add(this.TabConfig);
            this.Controls.Add(this.MenuServerStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuServerStrip;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crystalshire -Authentication @ 0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.TabConfig.ResumeLayout(false);
            this.general_TabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.MenuServerStrip.ResumeLayout(false);
            this.MenuServerStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabConfig;
        private System.Windows.Forms.TabPage general_TabPage;
        public System.Windows.Forms.RichTextBox TextSystem;
        private System.Windows.Forms.MenuStrip MenuServerStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem MenuMaintenance;
        private System.Windows.Forms.ToolStripMenuItem MenuDisableLogin;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuServer;
        private System.Windows.Forms.ToolStripMenuItem MenuConnections;
        private System.Windows.Forms.ToolStripMenuItem MenuReloadConfig;
        private System.Windows.Forms.ToolStripMenuItem MenuReloadBlockList;
        private System.Windows.Forms.ToolStripMenuItem MenuReloadChecksum;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.RichTextBox TextConnection;
        public System.Windows.Forms.RichTextBox TextUser;
        private System.Windows.Forms.ToolStripMenuItem MenuBlockList;
        private System.Windows.Forms.ToolStripMenuItem MenuThreadSleep;
    }
}

