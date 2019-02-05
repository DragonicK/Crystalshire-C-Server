namespace GameServer {
    partial class FrmPlayers {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlayers));
            this.GridConnections = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.col_index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_hexid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_character = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_characterid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_map = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridConnections)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridConnections
            // 
            this.GridConnections.AllowUserToAddRows = false;
            this.GridConnections.AllowUserToDeleteRows = false;
            this.GridConnections.AllowUserToOrderColumns = true;
            this.GridConnections.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridConnections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridConnections.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_index,
            this.col_hexid,
            this.col_ip,
            this.col_username,
            this.col_character,
            this.col_characterid,
            this.col_map});
            this.GridConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridConnections.GridColor = System.Drawing.SystemColors.Control;
            this.GridConnections.Location = new System.Drawing.Point(0, 24);
            this.GridConnections.Name = "GridConnections";
            this.GridConnections.ReadOnly = true;
            this.GridConnections.RowHeadersVisible = false;
            this.GridConnections.RowTemplate.Height = 21;
            this.GridConnections.Size = new System.Drawing.Size(584, 287);
            this.GridConnections.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuRefresh
            // 
            this.MenuRefresh.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuRefresh.Name = "MenuRefresh";
            this.MenuRefresh.Size = new System.Drawing.Size(62, 20);
            this.MenuRefresh.Text = "Refresh";
            this.MenuRefresh.Click += new System.EventHandler(this.MenuRefresh_Click);
            // 
            // col_index
            // 
            this.col_index.HeaderText = "Index";
            this.col_index.Name = "col_index";
            this.col_index.ReadOnly = true;
            // 
            // col_hexid
            // 
            this.col_hexid.HeaderText = "Unique Key";
            this.col_hexid.Name = "col_hexid";
            this.col_hexid.ReadOnly = true;
            this.col_hexid.Width = 120;
            // 
            // col_ip
            // 
            this.col_ip.HeaderText = "Ip Address";
            this.col_ip.Name = "col_ip";
            this.col_ip.ReadOnly = true;
            this.col_ip.Width = 120;
            // 
            // col_username
            // 
            this.col_username.HeaderText = "Username";
            this.col_username.Name = "col_username";
            this.col_username.ReadOnly = true;
            // 
            // col_character
            // 
            this.col_character.HeaderText = "Character";
            this.col_character.Name = "col_character";
            this.col_character.ReadOnly = true;
            // 
            // col_characterid
            // 
            this.col_characterid.HeaderText = "Character Id";
            this.col_characterid.Name = "col_characterid";
            this.col_characterid.ReadOnly = true;
            this.col_characterid.Width = 135;
            // 
            // col_map
            // 
            this.col_map.HeaderText = "Map";
            this.col_map.Name = "col_map";
            this.col_map.ReadOnly = true;
            // 
            // FrmPlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.GridConnections);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmPlayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connections";
            this.Load += new System.EventHandler(this.FrmConnections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridConnections)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridConnections;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_index;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_hexid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_character;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_characterid;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_map;
    }
}