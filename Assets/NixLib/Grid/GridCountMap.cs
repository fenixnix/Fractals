using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nixlib.Grid {
    public class GridCountMap {
        public Grid2DInt CountMap;
        public int MaxCount = 0;
        public void Count(Grid2DInt grid,int value) {
            CountMap = new Grid2DInt(grid.Width,grid.Height);
            MaxCount = 0;
            for(int y = 0; y < grid.Height; y++) {
                for(int x = 0; x < grid.Width; x++) {
                    if(grid[x, y] != 255) continue;
                    var cnt = grid.CountRect(new UnityEngine.Vector2Int(x, y), 2, 0);
                    MaxCount = Math.Max(MaxCount, cnt);
                    CountMap[x, y] = cnt;
                }
            }
        }
    }
}
