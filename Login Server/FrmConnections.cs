using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LoginServer.Network;

namespace LoginServer {
    public partial class FrmConnections : Form {
        /// <summary>
        /// Quantida de colunas no grid.
        /// </summary>
        private const int TotalGridColumn = 3;

        private Dictionary<int, Connection> _Connections;

        public FrmConnections() {
            InitializeComponent();
        }

        public void ShowForm(Dictionary<int, Connection> connections) {
            _Connections = connections;
            Show();
        }

        private void FrmConnections_Load(object sender, EventArgs e) {
            GridConnections.Font = new Font("Lucida Sans", 9f, FontStyle.Regular);
            CreateDataFromConnections();
        }

        /// <summary>
        /// Adiciona os dados no grid.
        /// </summary>
        /// <param name="items"></param>
        private void FillData(ref List<string> items) {
            var row = 0;

            while (items.Count > 0) {
                row = GridConnections.Rows.Add();

                for (var n = 0; n < TotalGridColumn; n++) {
                    GridConnections.Rows[row].Cells[n].Value = items[n];
                }

                items.RemoveRange(0, TotalGridColumn);
            }
        }

        /// <summary>
        /// Cria os dados para ser colocados no grid.
        /// </summary>
        private void CreateDataFromConnections() {
            var accounts = _Connections;
            var list = new List<string>();

            for(var n = 1; n <= _Connections.Count; n++) {
                if (_Connections.ContainsKey(n)) {
                    list.Add(n + "");
                    list.Add(_Connections[n].UniqueKey);
                    list.Add(_Connections[n].IpAddress + "");
                }
            }

            FillData(ref list);

            GridConnections.Sort(GridConnections.Columns[0], ListSortDirection.Ascending);
        }

        private void MenuRefresh_Click(object sender, EventArgs e) {
            GridConnections.Rows.Clear();
            CreateDataFromConnections();
        }
    }
}