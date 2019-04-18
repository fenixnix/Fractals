using UnityEngine;

#region
[System.Serializable]
public struct LayerBlendData {
    public int index;
    [Range(0,1f)]
    public float rate;
}

[System.Serializable]
public struct TerrainLayerData {
    public string id;
    public float height;
    public LayerBlendData[] layerBlendData;
}

[System.Serializable]
public struct RegionType {
    public string name;
    [Range(0, 1f)]
    public float height;
    public Color color;
}

[System.Serializable]
public class Region {
    public RegionType[] region;

    public Color this[float val] {
        get {
            for(int i = 0; i < region.Length; i++) {
                if(val <= region[i].height) {
                    return region[i].color;
                }
            }
            return Color.black;
        }
    }

    public int GetIndex(float val) {
        for(int i = 0; i < region.Length; i++) {
            if(val <= region[i].height) {
                return i;
            }
        }
        return region.Length - 1;
    }
}
#endregion

public class TerrainModify : MonoBehaviour {
    public Terrain t;
    public NoiseGUI noise;
    public AnimationCurve curve;
    public Region region = new Region();

    [ContextMenu("Random Generate")]
    public void Generate() {
        noise.Draw();
        SetPerlinHeight();
        SetLayer();
    }

    [ContextMenu("Set")]
    public void SetPerlinHeight() {
        Debug.Log(t.terrainData.heightmapWidth + "*" + t.terrainData.heightmapHeight);
        var data = new float[noise.noise.Height, noise.noise.Width];
        for(int y = 0; y < noise.noise.Height; y++) {
            for(int x = 0; x < noise.noise.Width; x++) {
                data[y, x] = curve.Evaluate(noise.noise[x, y]);
            }
        }
        t.terrainData.SetHeights(0, 0, data);
    }

    [ContextMenu("SetLayer")]
    public void SetLayer() {
        float[,,] map = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, t.terrainData.alphamapLayers];
        int[,] detail = new int[t.terrainData.detailWidth, t.terrainData.detailHeight];
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
                        detail[x, y] = 1;
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
        TerrainPaint.ModifyDetail(t.terrainData, detail, 1);
    }

    public TerrainLayerData[] layerDatas;
    public TerrainData[] terrainDatas;
    public void SetLayerByHeight() {
        foreach(var t in terrainDatas) {
            SetLayerByHeight(t);
            Debug.Log(t.GetHeight(0, 0));
        }
    }

    public void SetLayerByHeight(TerrainData td) {
        float[,,] map = new float[td.alphamapWidth, td.alphamapHeight, td.alphamapLayers];
        int[,] detail = new int[td.detailWidth, td.detailHeight];
        for(int y = 0; y < td.alphamapHeight; y++) {
            for(int x = 0; x < td.alphamapWidth; x++) {
                var height = td.GetHeight(x, y);
                for(int i = 0; i < layerDatas.Length; i++) {
                    if(height <= layerDatas[i].height) {
                        foreach(var bd in layerDatas[i].layerBlendData) {
                            map[y, x, bd.index] = bd.rate;
                        }
                        break;
                    }
                }
            }
        }
        td.SetAlphamaps(0, 0, map);
    }

    public Terrain[] terrains;
    public Transform root;
    public float ScanLength = 100f;
    [ContextMenu("FlattenSideGround")]
    public void FlattenSiteGround() {
        if(terrains == null) return;
        for(int i = 0; i < root.childCount; i++) {
            var pSrc = transform.GetChild(i).transform.position + new Vector3(0, ScanLength, 0);
            var pDir = new Vector3(0, -ScanLength * 2, 0);
            var ray = new Ray(pSrc, pDir);
            var hit = new RaycastHit();

            foreach(var t in terrains) {
                var coll = t.gameObject.GetComponent<TerrainCollider>();
                if(coll.Raycast(ray, out hit, ScanLength * 2)) {
                    Debug.Log(hit.point);
                    var terrain = t;
                    //TODO:FlattenGround;
                    break;
                }
            }
        }
    }

    public void DrawLayerPath(Nation nation) {
        //float[,,] map = new float[td.alphamapWidth, td.alphamapHeight, td.alphamapLayers];
        //int[,] detail = new int[td.detailWidth, td.detailHeight];
        //for(int y = 0; y < td.alphamapHeight; y++) {
        //    for(int x = 0; x < td.alphamapWidth; x++) {
        //        var height = td.GetHeight(x, y);
        //        for(int i = 0; i < layerDatas.Length; i++) {
        //            if(height <= layerDatas[i].height) {
        //                foreach(var bd in layerDatas[i].layerBlendData) {
        //                    map[y, x, bd.index] = bd.rate;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}
        //td.SetAlphamaps(0, 0, map);
    }
}
