namespace LoginServer {
    partial class FrmBlockList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBlockList));
            this.GridBlock = new System.Windows.Forms.DataGridView();
            this.col_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_access = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_attempt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_perm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridBlock)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridBlock
            // 
            this.GridBlock.AllowUserToAddRows = false;
            this.GridBlock.AllowUserToDeleteRows = false;
            this.GridBlock.AllowUserToOrderColumns = true;
            this.GridBlock.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridBlock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridBlock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridBlock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ip,
            this.col_time,
            this.col_access,
            this.col_attempt,
            this.col_perm});
            this.GridBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridBlock.GridColor = System.Drawing.SystemColors.Control;
            this.GridBlock.Location = new System.Drawing.Point(0, 24);
            this.GridBlock.Name = "GridBlock";
            this.GridBlock.ReadOnly = true;
            this.GridBlock.RowHeadersVisible = false;
            this.GridBlock.RowTemplate.Height = 21;
            this.GridBlock.Size = new System.Drawing.Size(584, 287);
            this.GridBlock.TabIndex = 5;
            // 
            // col_ip
            // 
            this.col_ip.HeaderText = "IpAddress";
            this.col_ip.Name = "col_ip";
            this.col_ip.ReadOnly = true;
            // 
            // col_time
            // 
            this.col_time.HeaderText = "Time";
            this.col_time.Name = "col_time";
            this.col_time.ReadOnly = true;
            this.col_time.Width = 120;
            // 
            // col_access
            // 
            this.col_access.HeaderText = "Access Count";
            this.col_access.Name = "col_access";
            this.col_access.ReadOnly = true;
            this.col_access.Width = 120;
            // 
            // col_attempt
            // 
            this.col_attempt.HeaderText = "Attempt Count";
            this.col_attempt.Name = "col_attempt";
            this.col_attempt.ReadOnly = true;
            this.col_attempt.Width = 120;
            // 
            // col_perm
            // 
            this.col_perm.HeaderText = "Permanent";
            this.col_perm.Name = "col_perm";
            this.col_perm.ReadOnly = true;
            this.col_perm.Width = 120;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 6;
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
            // FrmBlockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.GridBlock);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBlockList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Block List";
            this.Load += new System.EventHandler(this.FrmBlockList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridBlock)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GridBlock;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_access;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_attempt;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_perm;
    }
}