using UnityEngine;
using Fractals;
using Nixlib.Grid;
using UnityEngine.UI;

public class DrawDLA : MonoBehaviour
{
    public TextureRender render;
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

    public Grid2DInt grid2D;
    public int GridSize = 50;
    public FillFlow fillFlow = new FillFlow();
    public GridCountMap cntMap = new GridCountMap();
    public DLA2D dla2;

    public Vector2Int SrcPoint;
    public Vector2Int DstPoint;
    public int LineWidth = 0;

    [ContextMenu("Init2D")]
    public void Init2D() {
        grid2D = new Grid2DInt(GridSize, GridSize,0);
        dla2 = new DLA2D();
        dla2.SetSeedPoint(grid2D, grid2D.Center);

        //Grid2DPaint.Line(grid2D, SrcPoint,DstPoint,LineWidth);

        text.text = grid2D.Print();
        render.Draw(grid2D);
    }

    public bool DrawBorder = false;
    public bool DrawCountMap = true;
    public bool DrawFillFlow = false;

    [ContextMenu("Step2D")]
    public void Step2D() {
        for(int i = 0; i < GridSize * GridSize / 32; i++) {
            dla2.Step(grid2D);
        }
        grid2D.Save();

        if(DrawFillFlow) {
            fillFlow.Fill(grid2D, grid2D.Center);
            render.Draw(fillFlow.flowGrid, fillFlow.maxLength);
        }

        if(DrawBorder) {
            var tmpSrcGrid = grid2D.GetGrid();
            var tmpDstGrid = WaterShed.Run(tmpSrcGrid, Vector2Int.zero, 0, 1);
            render.Draw(new Grid2DInt(tmpDstGrid));
        }

        if(DrawCountMap) {
            cntMap.Count(grid2D, 255);
            render.Draw(cntMap.CountMap, cntMap.MaxCount);
        }
    }

    [ContextMenu("TestLoad")]
    public void TestLoadFile() {
        Init2D();
        grid2D.Load();
        cntMap.Count(grid2D, 255);
        render.Draw(cntMap.CountMap, cntMap.MaxCount);
    }
}
