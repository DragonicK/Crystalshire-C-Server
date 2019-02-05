namespace GameServer {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.ServerMenu = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuServer = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerTab = new System.Windows.Forms.TabControl();
            this.PageSystem = new System.Windows.Forms.TabPage();
            this.TextSystem = new System.Windows.Forms.RichTextBox();
            this.PageConnection = new System.Windows.Forms.TabPage();
            this.TextConnection = new System.Windows.Forms.RichTextBox();
            this.PagePlayer = new System.Windows.Forms.TabPage();
            this.TextPlayer = new System.Windows.Forms.RichTextBox();
            this.PageGame = new System.Windows.Forms.TabPage();
            this.TextGame = new System.Windows.Forms.RichTextBox();
            this.PageChat = new System.Windows.Forms.TabPage();
            this.TextChat = new System.Windows.Forms.RichTextBox();
            this.TimerClear = new System.Windows.Forms.Timer(this.components);
            this.MenuPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerMenu.SuspendLayout();
            this.ServerTab.SuspendLayout();
            this.PageSystem.SuspendLayout();
            this.PageConnection.SuspendLayout();
            this.PagePlayer.SuspendLayout();
            this.PageGame.SuspendLayout();
            this.PageChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerMenu
            // 
            this.ServerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.configurationToolStripMenuItem,
            this.MenuServer});
            this.ServerMenu.Location = new System.Drawing.Point(0, 0);
            this.ServerMenu.Name = "ServerMenu";
            this.ServerMenu.Size = new System.Drawing.Size(565, 24);
            this.ServerMenu.TabIndex = 0;
            this.ServerMenu.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExit});
            this.MenuFile.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(39, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(180, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // MenuServer
            // 
            this.MenuServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuPlayers});
            this.MenuServer.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuServer.Name = "MenuServer";
            this.MenuServer.Size = new System.Drawing.Size(55, 20);
            this.MenuServer.Text = "Server";
            // 
            // ServerTab
            // 
            this.ServerTab.Controls.Add(this.PageSystem);
            this.ServerTab.Controls.Add(this.PageConnection);
            this.ServerTab.Controls.Add(this.PagePlayer);
            this.ServerTab.Controls.Add(this.PageGame);
            this.ServerTab.Controls.Add(this.PageChat);
            this.ServerTab.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerTab.Location = new System.Drawing.Point(16, 33);
            this.ServerTab.Name = "ServerTab";
            this.ServerTab.SelectedIndex = 0;
            this.ServerTab.Size = new System.Drawing.Size(534, 345);
            this.ServerTab.TabIndex = 1;
            // 
            // PageSystem
            // 
            this.PageSystem.Controls.Add(this.TextSystem);
            this.PageSystem.Location = new System.Drawing.Point(4, 24);
            this.PageSystem.Name = "PageSystem";
            this.PageSystem.Padding = new System.Windows.Forms.Padding(3);
            this.PageSystem.Size = new System.Drawing.Size(526, 317);
            this.PageSystem.TabIndex = 0;
            this.PageSystem.Text = "System";
            this.PageSystem.UseVisualStyleBackColor = true;
            // 
            // TextSystem
            // 
            this.TextSystem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextSystem.DetectUrls = false;
            this.TextSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextSystem.Location = new System.Drawing.Point(3, 3);
            this.TextSystem.Name = "TextSystem";
            this.TextSystem.ReadOnly = true;
            this.TextSystem.Size = new System.Drawing.Size(520, 311);
            this.TextSystem.TabIndex = 1;
            this.TextSystem.Text = "";
            // 
            // PageConnection
            // 
            this.PageConnection.Controls.Add(this.TextConnection);
            this.PageConnection.Location = new System.Drawing.Point(4, 24);
            this.PageConnection.Name = "PageConnection";
            this.PageConnection.Padding = new System.Windows.Forms.Padding(3);
            this.PageConnection.Size = new System.Drawing.Size(526, 317);
            this.PageConnection.TabIndex = 4;
            this.PageConnection.Text = "Connection";
            this.PageConnection.UseVisualStyleBackColor = true;
            // 
            // TextConnection
            // 
            this.TextConnection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextConnection.DetectUrls = false;
            this.TextConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextConnection.Location = new System.Drawing.Point(3, 3);
            this.TextConnection.Name = "TextConnection";
            this.TextConnection.ReadOnly = true;
            this.TextConnection.Size = new System.Drawing.Size(520, 311);
            this.TextConnection.TabIndex = 3;
            this.TextConnection.Text = "";
            // 
            // PagePlayer
            // 
            this.PagePlayer.Controls.Add(this.TextPlayer);
            this.PagePlayer.Location = new System.Drawing.Point(4, 24);
            this.PagePlayer.Name = "PagePlayer";
            this.PagePlayer.Padding = new System.Windows.Forms.Padding(3);
            this.PagePlayer.Size = new System.Drawing.Size(526, 317);
            this.PagePlayer.TabIndex = 1;
            this.PagePlayer.Text = "Player";
            this.PagePlayer.UseVisualStyleBackColor = true;
            // 
            // TextPlayer
            // 
            this.TextPlayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextPlayer.DetectUrls = false;
            this.TextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPlayer.Location = new System.Drawing.Point(3, 3);
            this.TextPlayer.Name = "TextPlayer";
            this.TextPlayer.ReadOnly = true;
            this.TextPlayer.Size = new System.Drawing.Size(520, 311);
            this.TextPlayer.TabIndex = 0;
            this.TextPlayer.Text = "";
            // 
            // PageGame
            // 
            this.PageGame.Controls.Add(this.TextGame);
            this.PageGame.Location = new System.Drawing.Point(4, 24);
            this.PageGame.Name = "PageGame";
            this.PageGame.Padding = new System.Windows.Forms.Padding(3);
            this.PageGame.Size = new System.Drawing.Size(526, 317);
            this.PageGame.TabIndex = 2;
            this.PageGame.Text = "Game";
            this.PageGame.UseVisualStyleBackColor = true;
            // 
            // TextGame
            // 
            this.TextGame.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextGame.DetectUrls = false;
            this.TextGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextGame.Location = new System.Drawing.Point(3, 3);
            this.TextGame.Name = "TextGame";
            this.TextGame.ReadOnly = true;
            this.TextGame.Size = new System.Drawing.Size(520, 311);
            this.TextGame.TabIndex = 2;
            this.TextGame.Text = "";
            // 
            // PageChat
            // 
            this.PageChat.Controls.Add(this.TextChat);
            this.PageChat.Location = new System.Drawing.Point(4, 24);
            this.PageChat.Name = "PageChat";
            this.PageChat.Padding = new System.Windows.Forms.Padding(3);
            this.PageChat.Size = new System.Drawing.Size(526, 317);
            this.PageChat.TabIndex = 3;
            this.PageChat.Text = "Chat";
            this.PageChat.UseVisualStyleBackColor = true;
            // 
            // TextChat
            // 
            this.TextChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextChat.DetectUrls = false;
            this.TextChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextChat.Location = new System.Drawing.Point(3, 3);
            this.TextChat.Name = "TextChat";
            this.TextChat.ReadOnly = true;
            this.TextChat.Size = new System.Drawing.Size(520, 311);
            this.TextChat.TabIndex = 2;
            this.TextChat.Text = "";
            // 
            // TimerClear
            // 
            this.TimerClear.Enabled = true;
            this.TimerClear.Interval = 5000;
            this.TimerClear.Tick += new System.EventHandler(this.TimerClear_Tick);
            // 
            // MenuPlayers
            // 
            this.MenuPlayers.Name = "MenuPlayers";
            this.MenuPlayers.Size = new System.Drawing.Size(180, 22);
            this.MenuPlayers.Text = "Players";
            this.MenuPlayers.Click += new System.EventHandler(this.MenuPlayers_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 390);
            this.Controls.Add(this.ServerTab);
            this.Controls.Add(this.ServerMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ServerMenu;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crystalshire - Game Server  { 0 Ups }";
            this.ServerMenu.ResumeLayout(false);
            this.ServerMenu.PerformLayout();
            this.ServerTab.ResumeLayout(false);
            this.PageSystem.ResumeLayout(false);
            this.PageConnection.ResumeLayout(false);
            this.PagePlayer.ResumeLayout(false);
            this.PageGame.ResumeLayout(false);
            this.PageChat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ServerMenu;
        private System.Windows.Forms.TabControl ServerTab;
        private System.Windows.Forms.TabPage PageSystem;
        private System.Windows.Forms.TabPage PagePlayer;
        private System.Windows.Forms.TabPage PageGame;
        private System.Windows.Forms.TabPage PageChat;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.RichTextBox TextSystem;
        private System.Windows.Forms.RichTextBox TextPlayer;
        private System.Windows.Forms.RichTextBox TextGame;
        private System.Windows.Forms.RichTextBox TextChat;
        private System.Windows.Forms.Timer TimerClear;
        private System.Windows.Forms.TabPage PageConnection;
        private System.Windows.Forms.RichTextBox TextConnection;
        private System.Windows.Forms.ToolStripMenuItem MenuServer;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuPlayers;
    }
}

