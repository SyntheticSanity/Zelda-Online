using System;
using System.Text;
using System.IO;

namespace MapEditor {
    public class MapFile {
        public Version MapVersion;
        public UInt16 Width { get; set; }
        public UInt16 Height { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public String MetaData { get; set; }
        public MapTile[] Tiles;
        public MapFile(Version MapVersion, UInt16 Width, UInt16 Height, Int32 X, Int32 Y) {
            this.MapVersion = MapVersion;
            this.Width = Width;
            this.Height = Height;
            this.X = X;
            this.Y = Y;
            Tiles = new MapTile[Width * Height];
            this.MetaData = "This is a test map used for testing purposes.              There is a lot of whitespace in this meta data.";
        }
        public byte[] GetBytes() {
            byte[] meta = ASCIIEncoding.ASCII.GetBytes(this.MetaData);
            int hsize = 28; // 4    + 4     + 4 + 4 + 2 + 2 + 4     + 4
                            // Desc + Ver   + X + Y + W + H + Off1  + Off2
            int tsize = 4 * this.Width * this.Height;
            byte[] b = new byte[hsize + tsize];
            for (int i = 0; i < this.Width * this.Height; i++) {
                byte[] tb = this.Tiles[i].GetBytes();
                for (int t = 0; t < tb.Length; t++) {
                    b[hsize + i * tb.Length + t] = tb[t];
                }
            }
            return b;
        }
    }
    public class MapTile {
        public Byte Tileset { get; set; }
        public Byte TileIndex { get; set; }
        public Byte TypeData { get; set; }
        public Byte Effect { get; set; }
        public MapTile(Byte Tileset, Byte TileIndex, Byte TypeData, Byte Effect) {
            this.Tileset = Tileset;
            this.TileIndex = TileIndex;
            this.TypeData = TypeData;
            this.Effect = Effect;
        }
        public byte[] GetBytes() {
            byte[] b = { this.Tileset, this.TileIndex, this.TypeData, this.Effect };
            return b;
        }
    }
}
