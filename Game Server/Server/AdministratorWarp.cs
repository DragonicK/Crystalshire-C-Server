using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class AdministratorWarp {
        public Player Player { get; set; }

        public void AdminWarpLocation(int x, int y) {
            if (Player != null) {
                if (Player.AccessLevel >= AccessLevel.Administrator) {
                    var map = Player.GetMap();

                    Player.X = (x < 0) ? 0 : x;
                    Player.Y = (x < 0) ? 0 : y;
                    Player.X = (x > map.MaxX) ? map.MaxX : x;
                    Player.Y = (y > map.MaxY) ? map.MaxY : y;

                    var playerxy = new SpPlayerXY(Player.Index, Player.X, Player.Y, Player.Direction);
                    playerxy.SendToMap(Player.MapId);
                }
            }
        }

        public void MovePlayerToMe(Player player) {
            var msg = new SpPlayerMessage();

            if (player != null) {
                if (MovePlayer(player, Player.GetMap(), Player.X, Player.Y)) {

                    msg.Build($"You have been summoned by {Player.Character}", TextColor.BrigthBlue);
                    msg.Send(player.Connection);

                    msg.Build($"{player.Character} has been summoned", TextColor.BrigthBlue);
                    msg.Send(Player.Connection);
                }
            }
            else {
                msg.Build("Player is not online", TextColor.White);
                msg.Send(Player.Connection);
            }
        }

        public void MoveToPlayer(Player player) {
            var msg = new SpPlayerMessage();

            if (player != null) {
                if (MovePlayer(Player, player.GetMap(), player.X, player.Y)) {
                    msg.Build($"{Player.Character} has warped to you", TextColor.BrigthBlue);
                    msg.Send(player.Connection);

                    msg.Build($"You have been warped to {player.Character}", TextColor.BrigthBlue);
                    msg.Send(Player.Connection);
                }
            }
            else {
                msg.Build("Player is not online", TextColor.White);
                msg.Send(Player.Connection);
            }
        }
       
        public void MoveToMap(MapInstance map) {
            if (MovePlayer(Player, map, Player.X, Player.Y)) {
                var msg = new SpPlayerMessage();
                msg.Build($"You have been warped to map# {map.Id}", TextColor.BrigthBlue);
                msg.Send(Player.Connection);
            }
        }

        private bool MovePlayer(Player player, MapInstance map, int x, int y) {
            if (Player.AccessLevel >= AccessLevel.Monitor) {
                var movement = new PlayerMovement() {
                    Player = player
                };

                movement.Warp(map, x, y);
                return true;
            }

            return false;
        }
    }
}