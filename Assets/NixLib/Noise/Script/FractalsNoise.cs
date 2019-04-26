using UnityEngine;

namespace Noise {
    public class FractalsNoise {
        public float[,] rawData;

        public int Width => rawData.GetLength(1);
        public int Height => rawData.GetLength(0);
        public float this[int x, int y] =>rawData[y,x];

        public void Generate(Vector2Int size, float scale, int octave, float persistance, float lacunarity, Vector2 offset) {
            rawData = new float[size.y, size.x];
            var halfWidth = size.x / 2;
            var halfHeight = size.y / 2;
            var noise = new NoiseAtom();
            noise.Init(octave, persistance, lacunarity,scale,offset);

            for(int raw = 0; raw < size.y; raw++) {
                float y = (raw - halfHeight);
                for(int col = 0; col < size.x; col++) {
                    float x = (col - halfWidth);
                    float sampleVal = noise.NoiseValue(x, y);
                    rawData[raw, col] = sampleVal;
                }
            }
        }

        //float max = float.MinValue;
        //float min = float.MaxValue;
        //void Normalize() {
        //    for(int y = 0; y < rawData.GetLength(0); y++) {
        //        for(int x = 0; x < rawData.GetLength(1); x++) {
        //            rawData[y, x] = Mathf.InverseLerp(min, max, rawData[y, x]);
        //        }
        //    }
        //}
    }
}
