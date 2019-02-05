using GameServer.Data;
using GameServer.Communication;
using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class PlayerMovement {
        public Player Player { get; set; }
        public MapInstance Map { get; set; }

        public void RequestMap(Direction direction) {
            Move(direction, MovementType.Walking);
        }

        public void Move(Direction direction, MovementType movement, int x, int y) { 
            if (Player.X != x) {
                var msg = new SpPlayerXY(Player.Index, Player.X, Player.Y, Player.Direction);
                msg.Send(Player.Connection);

                return;
            }

            if (Player.Y != y) {
                var msg = new SpPlayerXY(Player.Index, Player.X, Player.Y, Player.Direction);
                msg.Send(Player.Connection);

                return;
            }

            if (Player.ChatNpcId > 0) {
                var chat = new NpcConversation() {
                    Player = Player
                };

                chat.Close();
            }

            Move(direction, movement);
        }

        private void Move(Direction direction, MovementType movement) {
            Player.Direction = direction;
            var result = CanMove(direction);
            var moved = false;

            if (result == 1) {
                switch (direction) {
                    case Direction.Up:
                        Player.Y -= 1;
                        moved = true;
                        break;

                    case Direction.Down:
                        Player.Y += 1;
                        moved = true;
                        break;

                    case Direction.Left:
                        Player.X -= 1;
                        moved = true;
                        break;

                    case Direction.Right:
                        Player.X += 1;
                        moved = true;
                        break;
                }
            }

            if (moved) {
                var msg = new SpPlayerMove(Player, movement);
                msg.SendToMapBut(Player.Index, Player.MapId);
            }

            var tile = Map.Tile[Player.X, Player.Y];

            if (tile.Type == TileType.Warp) {
                var map = Global.GetMap(tile.Data1);

                if (map != null) {
                    Warp(map, tile.Data2, tile.Data3);
                    moved = true;
                }
            }

            // They tried to hack. 
            if (moved = false && result != 2) {
                Warp(Player.GetMap(), Player.X, Player.Y);
            }
        }

        public byte CanMove(Direction direction) {

            switch (direction) {
                case Direction.Up:

                    if (Player.Y > 0) {
                        if (CheckDirection(Direction.Up)) {
                            return 0;
                        }
                    }
                    else {
                        if (Map.Up > 0) {
                            var newMapId = Map.Up;
                            var newMapY = Map.MaxY;
                        
                            if (newMapId > 0) { 
                                Warp(Global.GetMap(newMapId), Player.X, newMapY);
                            }
                        }

                        return 0;
                    }

                    break;
                case Direction.Down:
                    if (Player.Y < Map.MaxY) {
                        if (CheckDirection(Direction.Down)) {
                            return 0;
                        }
                    }
                    else {
                        if (Map.Down > 0) {
                            var newMapId = Map.Down;

                            if (newMapId > 0) { 
                                Warp(Global.GetMap(newMapId), Player.X, 0);
                            }
                        }

                        return 0;
                    }

                    break;
                case Direction.Left:

                    if (Player.X > 0) {
                        if (CheckDirection(Direction.Left)) {
                            return 0;
                        }
                    }
                    else {
                        if (Map.Left > 0) {
                            var newMapId = Map.Left;
                            var newMapX = Map.MaxX;

                            if (newMapId > 0) {
                                Warp(Global.GetMap(newMapId), newMapX, Player.Y);
                            }
                        }

                        return 0;
                    }

                    break;
                case Direction.Right:

                    if (Player.X < Map.MaxX) {
                        if (CheckDirection(Direction.Right)) {
                            return 0;
                        }
                    }
                    else {
                        if (Map.Right > 0) {
                            var newMapId = Map.Right;

                            if (newMapId > 0) {
                                Warp(Global.GetMap(newMapId), 0, Player.Y);
                            }
                        }

                        return 0;
                    }

                    break;
            }

            return 1;
        }

        public bool CheckDirection(Direction direction) {
            var result = false;
            int x = 0;
            int y = 0;

            switch (direction) {
                case Direction.Up:
                    x = Player.X;
                    y = Player.Y - 1;
                    break;
                case Direction.Down:
                    x = Player.X;
                    y = Player.Y + 1;
                    break;
                case Direction.Left:
                    x = Player.X - 1;
                    y = Player.Y;
                    break;
                case Direction.Right:
                    x = Player.X + 1;
                    y = Player.Y;
                    break;
            }

            if (Map.Tile[x, y].Type == TileType.Blocked) {
                result = true;
            }

            return result;
        }

        public void Warp(MapInstance map, int x, int y) {
            if (x > map.MaxX) {
                x = map.MaxX;
            }

            if (y > map.MaxY) {
                y = map.MaxY;
            }

            if (x < 0) {
                x = 0;
            }

            if (y < 0) {
                y = 0;
            }

            SpPlayerXY coordinates = null;

            // Se o jogador está tentando teleportar para o mesmo mapa.
            // Apenas envia as coordenadas.
            if (Player.MapId == map.Id) {
                coordinates = new SpPlayerXY(Player.Index, Player.X, Player.Y, Player.Direction);
                coordinates.SendToMap(Player.MapId);
            }

            // Remove o jogador do mapa antigo.
            if (Player.MapId != map.Id) {
                var oldMap = Player.GetMap();
                oldMap.RemovePlayer(Player);
            }

            var loadMap = new SpLoadMap(map.Id);
            loadMap.Send(Player.Connection);

            if (Player.Target > 0) {
                var target = new PlayerTarget() {
                    Player = Player
                };

                target.ClearTarget();
            }

            Player.MapId = map.Id;
            Player.X = x;
            Player.Y = y;

            coordinates = new SpPlayerXY(Player.Index, Player.X, Player.Y, Player.Direction);
            coordinates.Send(Player.Connection);

            map.AddPlayer(Player);
            map.SendPlayersTo(Player);

            var mapNpcs = new SpMapNpcData(map);
            mapNpcs.Send(Player.Connection);

        }
    }
}