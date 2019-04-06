using UnityEngine;

namespace Grid {
    public class Grid2D {
        public static Vector2Int[] ortho = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
        public static Vector2Int[] diago = { new Vector2Int(1,1), new Vector2Int(-1,-1), new Vector2Int(-1, 1), new Vector2Int(1, -1) };

        public enum CorridorType {Ortho,Diago,Both}

        public int[,] grid;

        public int Width => grid.GetLength(1);
        public int Height => grid.GetLength(0);

        public Vector2Int Center => new Vector2Int(Width / 2, Height / 2);

        public int this[int x, int y] {
            get {
                if(Inside(x, y)) {
                    return grid[y, x];
                }
                else {
                    return 0;
                }
            }
            set {
                if(Inside(x, y)) {
                    grid[y, x] = value;
                }
            }
        }

        public int this[Vector2Int vec] {
            get {
                return this[vec.x, vec.y];
            }
            set {
                this[vec.x, vec.y] = value;
            }
        }

        public bool Inside(int x, int y) {
            if(x < 0) return false;
            if(x >= Width) return false;
            if(y < 0) return false;
            if(y >= Height) return false;
            return true;
        }

        public bool NextTo(Vector2Int vec, CorridorType type = CorridorType.Ortho ,byte val = 255) {
            if((type== CorridorType.Ortho)||(type == CorridorType.Both)) {
                foreach(var p in ortho) {
                    if(this[vec + p] == val) return true;
                }
            }
            if((type == CorridorType.Diago) || (type == CorridorType.Both)) {
                foreach(var p in diago) {
                    if(this[vec + p] == val) return true;
                }
            }
            return false;
        }

        public int CountRect(Vector2Int point, int size, int value) {
            int cnt = 0;
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    if((this[point.x + col, point.y + row]) == value) {
                        cnt++;
                    }
                }
            }
            return cnt;
        }

        public Grid2D Inverse() {
            var tmp = Create(Width, Height);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    var val = byte.MaxValue - grid[y, x];
                    tmp[x, y] = (byte)val;
                }
            }
            return tmp;
        }

        public string Print() {
            string txt = "Grid:W=" + Width + "*H=" + Height + "\n";
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    if(this[x, y] == 255) {
                        txt += "#";
                    }
                    else {
                        txt += "_";
                    }
                }
                txt += "\n";
            }
            return txt;
        }

        static public Grid2D Create(int w, int h, int def = 0) {
            var tmp = new int[h, w];
            for(int y = 0; y < h; y++) {
                for(int x = 0; x < w; x++) {
                    tmp[y, x] = def;
                }
            }
            return new Grid2D { grid = tmp, };
        }
    }
}
