using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using GameServer.Server;
using GameServer.Network;

namespace GameServer {
    public partial class FrmPlayers : Form {
        /// <summary>
        /// Quantida de colunas no grid.
        /// </summary>
        private const int TotalGridColumn = 7;

        public FrmPlayers() {
            InitializeComponent();
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
            var players = Authentication.Players;
            var list = new List<string>();
            MapInstance map = null;

            foreach (var player in players.Values) {
                list.Add(player.Index + "");
                list.Add(player.UniqueKey);
                list.Add(((Connection)player.Connection).IpAddress);
                list.Add(player.Username);
                list.Add(player.Character);
                list.Add(player.CharacterId + "");

                if (player.MapId > 0) {
                    map = player.GetMap();
                    list.Add((map != null) ? map.Name : string.Empty);
                }
                else {
                    list.Add("xana ue");
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