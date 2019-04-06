using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid {
    public class GridCountMap {
        public Grid2D CountMap;
        public int MaxCount = 0;
        public void Count(Grid2D grid,int value) {
            CountMap = Grid2D.Create(grid.Width, grid.Height);
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
