using UnityEngine;

namespace Nixlib.Grid {
    public class Grid2DInt : Grid2D<int> {
        public Grid2DInt(int w, int h, int val) {
            data = Create(w, h, val).data;
        }

        public Grid2DInt Inverse() {
            var tmp = Create(Width, Height);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    var val = byte.MaxValue - data[y, x];
                    tmp[x, y] = (byte)val;
                }
            }
            return tmp as Grid2DInt;
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
    }
}
