using UnityEngine;

namespace Noise{
    public class NoiseAtom {
        int octave = 2;
        float persistance = .5f;
        float lacunarity = 2f;
        float[] ampArray;
        float[] freqArray;
        Vector2[] offsets;

        public void Init(int octave, float persistance, float lacunarity,int seed =0) {
            this.octave = octave;
            this.persistance = persistance;
            this.lacunarity = lacunarity;
            Random.InitState(seed);
            ampArray = new float[octave];
            freqArray = new float[octave];
            offsets = new Vector2[octave];
            for(int i = 0; i < octave; i++) {
                ampArray[i] = Mathf.Pow(persistance, i);
                freqArray[i] = Mathf.Pow(lacunarity, i);
                offsets[i] = Random.insideUnitCircle;
            }
        }

        public float NoiseValue(float x, float y) {
            float value = 0;
            for(int i = 0; i < octave; i++) {
                value += (Mathf.PerlinNoise((x+offsets[i].x) * freqArray[i],
                    (y + offsets[i].y) * freqArray[i]) * 2 - 1) * ampArray[i];
            }
            return value;
        }
    }
}
