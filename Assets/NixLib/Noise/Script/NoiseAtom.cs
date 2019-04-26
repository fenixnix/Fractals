using UnityEngine;

namespace Noise{
    public class NoiseAtom {
        int octave = 2;
        float persistance = .5f;
        float lacunarity = 2f;
        float[] ampArray;
        float[] freqArray;
        Vector2 center;
        float scale = 1;
        Vector2[] offsets;
        float max;
        float min;

        public void Init(int octave, float persistance, float lacunarity,float scale = 1 ,Vector2 center = default,int seed =0) {
            this.octave = octave;
            this.persistance = persistance;
            this.lacunarity = lacunarity;
            this.center = center;
            this.scale = scale;
            Random.InitState(seed);
            ampArray = new float[octave];
            freqArray = new float[octave];
            offsets = new Vector2[octave];
            max = 0;
            for(int i = 0; i < octave; i++) {
                ampArray[i] = Mathf.Pow(persistance, i);
                freqArray[i] = Mathf.Pow(lacunarity, i);
                offsets[i] = Random.insideUnitCircle;
                max += ampArray[i];
            }
            max *= .5f;
            min = -max;
        }

        public float NoiseValue(float x, float y) {
            float value = 0;
            for(int i = 0; i < octave; i++) {
                float noise = Mathf.PerlinNoise((x/scale + center.x + offsets[i].x) * freqArray[i],
                    (y/scale + center.y + offsets[i].y) * freqArray[i]);
                value += (noise - .5f) * ampArray[i];
            }
            return  Mathf.InverseLerp(min,max,value);
        }

        public float NoiseValue(Vector2 v) {
            return NoiseValue(v.x, v.y);
        }
    }
}
