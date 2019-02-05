using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LoginServer.Network;

namespace LoginServer {
    public partial class FrmBlockList : Form {
        /// <summary>
        /// Quantida de colunas no grid.
        /// </summary>
        private const int TotalGridColumn = 5;

        public FrmBlockList() {
            InitializeComponent();
        }

        private void FrmBlockList_Load(object sender, EventArgs e) {
            GridBlock.Font = new Font("Lucida Sans", 9f, FontStyle.Regular);
            CreateDataFromBlockList();
        }

        /// <summary>
        /// Cria os dados para ser colocados no grid.
        /// </summary>
        private void CreateDataFromBlockList() {
            var blocks = IpBlockList.IpBlocked;
            var list = new List<string>();

            foreach (var block in blocks.Values) {
                list.Add(block.Ip  + "");
                list.Add(block.Time + "");
                list.Add(block.AccessCount + "");
                list.Add(block.AttemptCount + "");
                list.Add(block.Permanent.ToString());
            }

            FillData(ref list);

            GridBlock.Sort(GridBlock.Columns[0], ListSortDirection.Ascending);
        }

        /// <summary>
        /// Adiciona os dados no grid.
        /// </summary>
        /// <param name="items"></param>
        private void FillData(ref List<string> items) {
            var row = 0;

            while (items.Count > 0) {
                row = GridBlock.Rows.Add();

                for (var n = 0; n < TotalGridColumn; n++) {
                    GridBlock.Rows[row].Cells[n].Value = items[n];
                }

                items.RemoveRange(0, TotalGridColumn);
            }
        }

        private void MenuRefresh_Click(object sender, EventArgs e) {
            GridBlock.Rows.Clear();
            CreateDataFromBlockList();
        }
    }
}