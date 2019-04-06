using System.Collections.Generic;
using UnityEngine;
using Grid;

namespace Fractals{
    public class DLA2D {
        public void SetSeedPoint(Grid2D grid,Vector2Int seed, Grid2DWalker.WalkType type = Grid2DWalker.WalkType.Direction4) {
            Grid2DPainter.DrawDiamond(grid, seed.x, seed.y, 2);
            Grid2DWalker.InitMovTab(type);
        }

        public void Step(Grid2D grid,Grid2D.CorridorType type = Grid2D.CorridorType.Ortho) {
            //var walkerPos = OutLineGenerate(grid);
            var walkerPos = RandomGenerate(grid);
            Grid2DWalker walker = new Grid2DWalker(walkerPos);
            Vector2Int curPos = Vector2Int.zero;
            do {
                curPos = walker.Step(grid);
            } while(!grid.NextTo(curPos,type));
            Grid2DPainter.DrawDiamond(grid, curPos.x, curPos.y, 2);
            //grid[curPos.x,curPos.y] = 255;
        }

        Vector2Int OutLineGenerate(Grid2D grid) {
            var HV = Random.Range(0, 2);
            int pos = 0;
            var TBLR = Random.Range(0, 2);
            Vector2Int pointPos = new Vector2Int();
            if(HV == 0) {
                pos = Random.Range(1, grid.Width - 1);
            }
            else {
                pos = Random.Range(1, grid.Height - 1);
            }
            if((HV == 0) && (TBLR == 0)) {
                pointPos = new Vector2Int(pos, 0);
            }
            if((HV == 0) && (TBLR == 1)) {
                pointPos = new Vector2Int(pos, grid.Height - 1);
            }

            if((HV == 1) && (TBLR == 0)) {
                pointPos = new Vector2Int(0, pos);
            }
            if((HV == 1) && (TBLR == 1)) {
                pointPos = new Vector2Int(grid.Width - 1, pos);
            }
            return pointPos;
        }

        Vector2Int RandomGenerate(Grid2D grid) {
            return new Vector2Int(Random.Range(0, grid.Width),
                Random.Range(0,grid.Height));
        }
    }
}
