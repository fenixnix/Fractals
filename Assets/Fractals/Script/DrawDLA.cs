using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fractals;

public class DrawDLA : MonoBehaviour
{
    public DLA dla = new DLA();
    bool Ready = false;

    public int CubeSize = 3;
    [ContextMenu("Init")]
    public void Init() {
        dla.Init(CubeSize);
        Ready = true;
    }

    [ContextMenu("Step")]
    public void Step() {
        dla.Step();
    }

    private void OnDrawGizmos() {
        if(!Ready) return;
        Step();
        var size = dla.CubeSize * 2 + 1;
        for(int z = 0; z < size; z++) {
            for(int y = 0; y < size; y++) {
                for(int x = 0; x < size; x++) {
                    if(dla.pixels[z, y, x]) {
                        Gizmos.DrawCube(new Vector3(x, y, z) - 
                            new Vector3(dla.CubeSize, dla.CubeSize, dla.CubeSize), Vector3.one);
                    }
                }
            }
        }
    }
}
