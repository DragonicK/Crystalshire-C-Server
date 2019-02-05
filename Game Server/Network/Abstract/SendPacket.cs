using GameServer.Server;

namespace GameServer.Network {
    public abstract class SendPacket {
        protected ByteBuffer msg;

        public SendPacket() {
            msg = new ByteBuffer();
        }

        ~SendPacket() {
            msg.Clear();
            msg = null;
        }

        public void Send(IConnection connection) {
            connection.Send(msg, GetType().Name);
        }

        public void SendToAll() {
            var players = Authentication.Players;
            var highIndex = Authentication.HighIndex;

            for (var i = 1; i <= highIndex; i++) {
                if (players.ContainsKey(i)) {
                    if (players[i].Connected) {
                        Send(players[i].Connection);
                    }
                }
            }
        }

        public void SendToAllBut(int index) {
            var players = Authentication.Players;
            var highIndex = Authentication.HighIndex;

            for (var i = 1; i <= highIndex; i++) {
                if (players.ContainsKey(i)) {
                    if (i != index) {
                        Send(players[i].Connection);
                    }
                }
            }
        }

        public void SendToMap(int mapId) {
            var players = Authentication.Players;
            var highIndex = Authentication.HighIndex;

            for (var i = 1; i <= highIndex; i++) {
                if (players.ContainsKey(i)) {
                    if (players[i].MapId == mapId) {
                        Send(players[i].Connection);
                    }
                }
            }
        }

        public void SendToMapBut(int index, int mapId) {
            var players = Authentication.Players;
            var highIndex = Authentication.HighIndex;

            for (var i = 1; i <= highIndex; i++) {
                if (players.ContainsKey(i)) {
                    if (i != index && players[i].MapId == mapId) {
                        Send(players[i].Connection);
                    }
                }
            }
        }
    }
}