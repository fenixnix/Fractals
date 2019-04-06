using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fractals;
using Grid;
using UnityEngine.UI;

public class DrawDLA : MonoBehaviour
{
    public DLA dla = new DLA();
    public Text text;
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

    public Grid2D grid2D;
    public DLA2D dla2;
    [ContextMenu("Init2D")]
    public void Init2D() {
        grid2D = Grid2D.Create(50, 50);
        dla2 = new DLA2D();
        dla2.SetSeedPoint(grid2D, grid2D.Center);
        text.text = grid2D.Print();
    }

    [ContextMenu("Step2D")]
    public void Step2D() {
        for(int i = 0; i < 25*6; i++) {
            dla2.Step(grid2D);
        }
        text.text = grid2D.Inverse().Print();
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
