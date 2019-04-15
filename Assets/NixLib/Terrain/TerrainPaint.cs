using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geometric;
using Nixlib.Grid;
using NixLib.Common;
using Fractals;

public class TerrainPaint : MonoBehaviour {
    public TerrainData t;
    public int LayerIndex = 0;
    public Line2DInt[] lines;
    public bool noiseLine = false;
    public float noiseScale = .2f;
    public int LineWidth = 1;

    [ContextMenu("Draw")]
    public void Test() {
        Draw(LayerIndex);
        int[,] dat = new int[10, 10];
        for(int y = 0; y < 10; y++) {
            for(int x = 0; x < 10; x++) {
                if(x % 2 == 0) {
                    dat[y, x] = 1;
                }
            }
        }
        ModifyDetail(t,dat, 0);
    }

    public void Draw(int LayerIndex = 0) {
        int w = t.alphamapWidth;
        int h = t.alphamapHeight;
        var data = t.GetAlphamaps(0, 0, w, h);
        var tmpData = new float[h, w];
        foreach(var l in lines) {
            if(noiseLine) {
                var sublinePoints = SubDividLine.Generate(new Line(l),8,noiseScale);
                Vector2Int pre = new Vector2Int((int)sublinePoints[0].x, (int)sublinePoints[0].y);
                foreach(var sl in sublinePoints) {
                    Vector2Int cur = new Vector2Int((int)sl.x, (int)sl.y);
                    ArrayPaint<float>.Line(tmpData,new Line2DInt(pre,cur), 1,LineWidth);
                    pre = cur;
                }
            }
            else {
                ArrayPaint<float>.Line(tmpData, l, 1, LineWidth);
            }
        }
        ModifyLayer(t, tmpData, LayerIndex);
    }

    static public void ModifyLayer(TerrainData td, float[,] data, int index) {
        int w = td.alphamapWidth;
        int h = td.alphamapHeight;
        var alphaTmp = td.GetAlphamaps(0, 0, w, h);
        for(int y = 0; y < h; y++) {
            for(int x = 0; x < w; x++) {
                alphaTmp[y, x, index] = data[y, x];
            }
        }
        td.SetAlphamaps(0, 0, alphaTmp);
    }

    static public void ModifyDetail(TerrainData td, int[,] data, int index) {
        td.SetDetailLayer(0, 0, index, data);
    }
}
