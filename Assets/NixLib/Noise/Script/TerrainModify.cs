using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noise;

public class TerrainModify : MonoBehaviour
{
    public Terrain t;
    public NoiseGUI noise;
    public AnimationCurve curve;
    public Region region = new Region();
    
    [ContextMenu("Test")]
    public void Test() {
        noise.Draw();
        SetPerlinHeight();
        SetLayer();
    }

    [ContextMenu("Set")]
    public void SetPerlinHeight() {
        Debug.Log(t.terrainData.heightmapWidth + "*" +t.terrainData.heightmapHeight);
        var data = new float[noise.noise.Height, noise.noise.Width];
        for(int y = 0; y < noise.noise.Height; y++) {
            for(int x = 0; x < noise.noise.Width; x++) {
                data[y,x] = curve.Evaluate(noise.noise[x, y]);
            }
        }

        t.terrainData.SetHeights(0, 0, data);
    }

    [ContextMenu("SetLayer")]
    public void SetLayer() {
        float[,,] map = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, t.terrainData.alphamapLayers];
        for(int y = 0; y < t.terrainData.alphamapHeight; y++) {
            for(int x = 0; x < t.terrainData.alphamapWidth; x++) {
                for(int i = 0; i < map.GetLength(2); i++) {
                    var height = noise.noise.rawData[x, y];
                    var index = region.GetIndex(height);
                    if(index == 0) {
                        map[x, y, 1] = 1;
                    }
                    if(index == 1) {
                        map[x, y, 2] = 1;
                    }
                    if(index == 2) {
                        map[x, y, 3] = 1;
                    }
                    if(index == 3) {
                        map[x, y, 0] = 1;
                    }
                    if(index == 4) {
                        map[x, y, 5] = 1;
                    }
                }
            }
        }
        t.terrainData.SetAlphamaps(0, 0, map);
    }
}
