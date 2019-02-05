using System;
using System.Collections.Generic;
using GameServer.Data;
using GameServer.Communication;
using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class MapInstance : Map {
        public Dictionary<int, MapNpc> Npcs { get; set; }
        public Dictionary<int, Player> Players { get; set; }

        private Random random;
        private int updateTick;
        private const int Time = 500;

        public MapInstance(Map map) {
            random = new Random();
            Players = new Dictionary<int, Player>();
            Npcs = new Dictionary<int, MapNpc>();

            Id = map.Id;
            Name = map.Name;
            Moral = map.Moral;
            Up = map.Up;
            Down = map.Down;
            Left = map.Left;
            Right = map.Right;

            BootMap = map.BootMap;
            BootX = map.BootX;
            BootY = map.BootY;

            MaxX = map.MaxX;
            MaxY = map.MaxY;

            Tile = new Tile[MaxX + 1, MaxY + 1];

            for (var x = 0; x <= MaxX; x++) {
                for (var y = 0; y <= MaxY; y++) {

                    Tile[x, y] = new Tile {
                        Type = map.Tile[x, y].Type,
                        Data1 = map.Tile[x, y].Data1,
                        Data2 = map.Tile[x, y].Data2,
                        Data3 = map.Tile[x, y].Data3,
                        Data4 = map.Tile[x, y].Data4,
                        Data5 = map.Tile[x, y].Data5,
                        DirBlock = map.Tile[x, y].DirBlock
                    };
                }
            }

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                Npc[i] = map.Npc[i];
                Npcs.Add(i, new MapNpc());
            }

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                SpawnNpc(i);
            }
        }

        public void AddPlayer(Player player) {
            if (!Players.ContainsKey(player.Index)) {
                Players.Add(player.Index, player);

                var msg = new SpPlayerData(player);
                msg.SendToMapBut(player.Index, Id);
            }
        }

        public void RemovePlayer(Player player) {
            if (Players.ContainsKey(player.Index)) {
                Players.Remove(player.Index);

                var left = new SpPlayerLeft(player.Index);
                left.SendToMapBut(player.Index, Id);
            }
        }

        public void UpdateLogic() {
            if (Environment.TickCount > updateTick + Time) {
                updateTick = Environment.TickCount;

                UpdateNpcsLogic();
            }
        }

        public void SendPlayersTo(Player player) {

            foreach (var pData in Players.Values) {
                if (Players.ContainsKey(pData.Index)) {

                    if (player.Index != pData.Index) {
                        var msg = new SpPlayerData(pData);
                        msg.Send(player.Connection);
                    }

                }
            }
        }

        public void Respawn(Player player) {
            SendPlayersTo(player);

            var mapNpcs = new SpMapNpcData(this);
            mapNpcs.Send(player.Connection);
        }

        private void UpdateNpcsLogic() {
            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                if (Npcs[i].Id > 0) {
                    if (Npcs[i].InChatWith <= 0) {
                        GenerateNpcMovement(i);
                    }
                }
            }
        }

        private void GenerateNpcMovement(int index) {
            if (Npcs[index].Behaviour == NpcBehaviour.Patrol) {
                int direction = random.Next(0, 5);

                if (Enum.IsDefined(typeof(Direction), direction)) {
                    if (CanNpcMove(index, (Direction)direction)) {
                        NpcMove(index, (Direction)direction);
                    }
                }
            }
        }

        private void SpawnNpc(int index) {
            if (!SpawnAtSelectCoordiantes(index)) {
                SpawnAtRandomCoordinates(index);
            }
        }

        private bool SpawnAtSelectCoordiantes(int index) {
            for (var x = 0; x <= MaxX; x++) {
                for (var y = 0; y <= MaxY; y++) {

                    if (Tile[x, y].Type == TileType.NpcSpawn) {
                        if (Tile[x, y].Data1 == index + 1) {

                            var npc = DataManagement.NpcDatabase[Npc[index]];
                            Npcs[index].Id = Npc[index];
                            Npcs[index].X = x;
                            Npcs[index].Y = y;
                            Npcs[index].HP = npc.HP;
                            Npcs[index].Direction = (Direction)Tile[x, y].Data2;
                            Npcs[index].Behaviour = (NpcBehaviour)npc.Behaviour;

                            return true;

                        }
                    }
                }
            }

            return false;
        }

        private void SpawnAtRandomCoordinates(int index) {
            var random = new Random();
            var attempt = 100;
            int x = 0;
            int y = 0;

            // Well, try 100 times to randomly place the sprite.
            for (var i = 0; i < attempt; i++) {
                x = random.Next(0, MaxX);
                y = random.Next(0, MaxY);

                if (NpcTileIsOpen(x, y)) {
                    Npcs[index].Id = Npc[index];
                    Npcs[index].Behaviour = NpcBehaviour.Patrol;
                    Npcs[index].X = x;
                    Npcs[index].Y = y;
                    
                    break;
                } 
            }
        }

        private bool NpcTileIsOpen(int x, int y) {
            var players = Authentication.Players;

            for (var i = 1; i < Authentication.HighIndex; i++) {
                if (players[i].MapId == Id) {
                    if (players[i].X == x && players[i].Y == y) {
                        return false;
                    }
                }
            }
 
            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                if (Npcs[i].Id > 0) {
                    if (Npcs[i].X == x && Npcs[i].Y == y) {
                        return false;
                    }
                }
            }

            if (Tile[x, y].Type != TileType.Walkable) {
                if (Tile[x, y].Type == TileType.NpcSpawn) {
                    return false;
                }
            }

            return true;
        }

        private void NpcMove(int index, Direction direction) {
            Npcs[index].Direction = direction;

            switch (direction) {
                case Direction.Up:
                    Npcs[index].Y--;
                    break;
                case Direction.Down:
                    Npcs[index].Y++;
                    break;
                case Direction.Left:
                    Npcs[index].X--;
                    break;
                case Direction.Right:
                    Npcs[index].X++;
                    break;
            }

            var msg = new SpMapNpcMove(index, Npcs[index].X, Npcs[index].Y, direction, MovementType.Walking);
            msg.SendToMap(Id);
        }

        private bool CanNpcMove(int npcIndex, Direction direction) {
            return CheckForDirection(npcIndex, direction, Npcs[npcIndex].X, Npcs[npcIndex].Y);
        }

        private bool IsDirBlocked(uint blockVar, int dir) {
            var dirResult = Convert.ToUInt32(Math.Pow(2, dir));
            var result = (~blockVar & dirResult);

            if (Convert.ToBoolean(result)) {
                return false;
            }

            return true;
        }

        private bool CheckForDirection(int npcIndex, Direction direction, int x, int y) {
            Player pData;

            if (direction == Direction.Up) {
                if (y <= 0) {
                    return false;
                }

                y -= 1;

            }
            else if (direction == Direction.Down) {
                if (y >= MaxY) {
                    return false;
                }

                y += 1;
            }
            else if (direction == Direction.Left) {
                if (x <= 0) {
                    return false;
                }

                x -= 1;
            }
            else if (direction == Direction.Right) {
                if (x >= MaxX) {
                    return false;
                }

                x += 1;
            }

            var type = Tile[x, y].Type;
            if (type != TileType.Walkable && type != TileType.NpcSpawn) {
                return false;
            }

            for (var i = 1; i <= Authentication.HighIndex; i++) {
                if (Authentication.Players.ContainsKey(i)) {
                    pData = Authentication.Players[i];

                    if (pData.MapId == Id) {
                        if (pData.X == x && pData.Y == y) {
                            return false;
                        }
                    }
                }
            }

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                if (i != npcIndex) {
                    if (Npcs[i].Id > 0) {
                        if (Npcs[i].X == x && Npcs[i].Y == y) {
                            return false;
                        }
                    }
                }
            }

            if (IsDirBlocked(Tile[x, y].DirBlock, (int)direction + 1)) {
                return false;
            }

            return true;
        }
    }
}