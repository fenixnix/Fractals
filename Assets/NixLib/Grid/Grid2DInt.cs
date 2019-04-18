using UnityEngine;
using System.IO;

namespace Nixlib.Grid {
    public class Grid2DInt : Grid2D<int> {
        public Grid2DInt(int w, int h, int val = 0) {
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
                        continue;
                    }
                    if(this[x, y] == 0) {
                        txt += "_";
                        continue;
                    }

                    txt += this[x, y];

                }
                txt += "\n";
            }
            return txt;
        }

        public void Save(string filePath = "Assets/GridData.raw") {
            var file = File.Create(filePath);
            BinaryWriter bw = new BinaryWriter(file);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    bw.Write(data[y, x]);
                }
            }
            bw.Close();
            file.Close();
        }

        public void Load(string filePath = "Assets/GridData.raw") {
            var file = File.OpenRead(filePath);
            BinaryReader br = new BinaryReader(file);
            for(int y = 0; y < Height; y++) {
                for(int x = 0; x < Width; x++) {
                    data[y, x] = br.ReadInt32();
                }
            }
            br.Close();
            file.Close();
        }
    }
}
