using System;

namespace GameServer.Data {
    public sealed class Tile {
        public Square[] Layer { get; set; }
        public byte[] AutoTile { get; set; }
        public TileType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Data4 { get; set; }
        public int Data5 { get; set; }
        public byte DirBlock { get; set; }

        public Tile() {
            var length = Enum.GetValues(typeof(LayerType)).Length;

            AutoTile = new byte[length];
            Layer = new Square[length];
        }
    }
}