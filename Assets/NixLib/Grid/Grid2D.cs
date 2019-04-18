using UnityEngine;

namespace Nixlib.Grid {
    public class Grid2D<T> {
        public static Vector2Int[] ortho = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
        public static Vector2Int[] diago = { new Vector2Int(1, 1), new Vector2Int(-1, -1), new Vector2Int(-1, 1), new Vector2Int(1, -1) };

        public int Width => data.GetLength(1);
        public int Height => data.GetLength(0);

        public enum CorridorType { Ortho, Diago, Both }
        public T[,] data;

        public Vector2Int Center => new Vector2Int(Width / 2, Height / 2);

        public T this[int x, int y] {
            get {
                if(Inside(x, y)) {
                    return data[y, x];
                }
                else {
                    return default(T);
                }
            }
            set {
                if(Inside(x, y)) {
                    data[y, x] = value;
                }
            }
        }

        public T this[Vector2Int vec] {
            get {
                return this[vec.x, vec.y];
            }
            set {
                this[vec.x, vec.y] = value;
            }
        }

        public Grid2D<T> Clone() {
            var tmp = Create(Width, Height);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    tmp[x, y] = this[x, y];
                }
            }
            return tmp;
        }

        public bool Inside(int x, int y) {
            if(x < 0) return false;
            if(x >= Width) return false;
            if(y < 0) return false;
            if(y >= Height) return false;
            return true;
        }

        public bool NextTo(Vector2Int vec, CorridorType type = CorridorType.Ortho, T val = default(T)) {
            if((type == CorridorType.Ortho) || (type == CorridorType.Both)) {
                foreach(var p in ortho) {
                    if(this[vec + p].Equals(val)) return true;
                }
            }
            if((type == CorridorType.Diago) || (type == CorridorType.Both)) {
                foreach(var p in diago) {
                    if(this[vec + p].Equals(val)) return true;
                }
            }
            return false;
        }

        public void Noise(T val,float rate = .3f) {
            for(int row = 0; row < Height; row++) {
                for(int col = 0; col < Width; col++) {
                    if(Random.value < rate) {
                        this[col, row] = val;
                    }
                }
            }
        }

        public int CountAround(Vector2Int point,T value) {
            var cntTmp = CountRect(point, 1, value);
            if(this[point].Equals(value)) {
                return (cntTmp - 1);
            }
            return cntTmp;
        }

        public int CountRect(Vector2Int point, int size, T value) {
            int cnt = 0;
            for(int row = -size; row <= size; row++) {
                for(int col = -size; col <= size; col++) {
                    if((this[point.x + col, point.y + row]).Equals(value)) {
                        cnt++;
                    }
                }
            }
            return cnt;
        }

        public bool Is(Vector2Int point,T val) {
            return this[point.x, point.y].Equals(val);
        }

        static public Grid2D<T> Create(int w, int h, T def = default(T)) {
            var tmp = new T[h, w];
            for(int y = 0; y < h; y++) {
                for(int x = 0; x < w; x++) {
                    tmp[y, x] = def;
                }
            }
            return new Grid2D<T> { data = tmp, };
        }

        static public Grid2D<T> Create(Grid2D<T> src) {
            var tmp = new T[src.Height, src.Width];
            for(int y = 0; y < src.Height; y++) {
                for(int x = 0; x < src.Width; x++) {
                    tmp[y, x] = src[x, y];
                }
            }
            return new Grid2D<T> { data = tmp, };
        }
    }
}

