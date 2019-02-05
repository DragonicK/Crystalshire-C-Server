using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public class Map {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Music { get; set; }
        public byte Moral { get; set; }

        public int Up { get; set; }
        public int Down { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public int BootMap { get; set; }
        public byte BootX { get; set; }
        public byte BootY { get; set; }

        public byte MaxX { get; set; }
        public byte MaxY { get; set; }

        public Tile[,] Tile { get; set; }

        public int BossNpc { get; set; }
        public Dictionary<int, int> Npc { get; set; }

        public int EventCount { get; set; }
        public Event[] Events { get; set; }

        public Map() {
            Name = string.Empty;
            Music = string.Empty;
            Tile = new Tile[MaxX + 1, MaxY + 1];
           
            for (var x = 0; x <= MaxX; x++) {
                for (var y = 0; y <= MaxY; y++) {
                    Tile[x, y] = new Tile();
                }
            }

            Npc = new Dictionary<int, int>();
            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                Npc.Add(i, 0);
            }
        }
    }
}