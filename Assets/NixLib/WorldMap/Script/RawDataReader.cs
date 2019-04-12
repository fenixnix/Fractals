using System.IO;
using UnityEngine;

[System.Serializable]
public struct RawData {
    public TerrainData terrainData;
    public string fileName;
}

public class RawDataReader : MonoBehaviour {
    public TerrainRawData data;
    public Terrain[] terrains;

    public int width = 3601;
    public int height = 3601;
    public int altitude = 1000;
    public int terrainResolution = 129;

    [ContextMenu("Process")]
    public void Process() {
        foreach(var d in data.data) {
            ReadRawData(d.terrainData, data.path + d.fileName);
        }
    }

    public void ReadRawData(TerrainData t, string fileName = "Assets/RawData.raw") {
        float tResolution = altitude;
        //t.heightmapResolution = altitude;
        var file = File.OpenRead(fileName);
        BinaryReader br = new BinaryReader(file);
        float[,] rawHeightData = new float[height, width];
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                rawHeightData[y, x] = (br.ReadInt16())/(tResolution*2);
            }
        }
        file.Close();
        float[,] heightData = new float[terrainResolution, terrainResolution];
        for(int y = 0; y < terrainResolution; y++) {
            for(int x = 0; x < terrainResolution; x++) {
                int sampleX = x*width / terrainResolution;
                int sampleY = y*height / terrainResolution;
                heightData[terrainResolution -1 -y, x] = rawHeightData[sampleY,sampleX];
            }
        }
        t.SetHeights(0, 0, heightData);
    }
}
