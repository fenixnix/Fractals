﻿using UnityEngine;

namespace Noise {
    public class FractalsNoise {
        public float[,] rawData;

        public int Width => rawData.GetLength(1);
        public int Height => rawData.GetLength(0);
        public float this[int x, int y] =>rawData[y,x];

        float max = float.MinValue;
        float min = float.MaxValue;
        

        public void Generate(Vector2Int size, float scale, int octave, float persistance, float lacunarity, Vector2 offset) {
            rawData = new float[size.y, size.x];
            max = float.MinValue;
            min = float.MaxValue;
            var halfWidth = size.x / 2;
            var halfHeight = size.y / 2;
            var noise = new NoiseAtom();
            noise.Init(octave, persistance, lacunarity);

            for(int raw = 0; raw < size.y; raw++) {
                float y = (raw - halfHeight) / scale;
                for(int col = 0; col < size.x; col++) {
                    float x = (col - halfWidth) / scale;
                    //var sampleVal = NoiseValue(x + offset.x, y + offset.y, octave, persistance, lacunarity);
                    float sampleVal = noise.NoiseValue(x + offset.x, y + offset.y);
                    max = Mathf.Max(max, sampleVal);
                    min = Mathf.Min(min, sampleVal);
                    rawData[raw, col] = sampleVal;
                }
            }
            Normalize();
        }

        void Normalize() {
            for(int y = 0; y < rawData.GetLength(0); y++) {
                for(int x = 0; x < rawData.GetLength(1); x++) {
                    rawData[y, x] = Mathf.InverseLerp(min, max, rawData[y, x]);
                }
            }
        }
    }
}
