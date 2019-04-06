using System;
namespace Grid {
    public class Grid2DPainter {
        static public void DrawRect(Grid2D grid,int x, int y, int size = 3, byte data = 255) {
            for(int row = -size; row < size+1; row++) {
                for(int col = -size; col < size+1; col++) {
                    grid[x + col, y + row] = data;
                }
            }
        }

        static public void DrawDiamond(Grid2D grid, int x, int y, int size = 3, byte data = 255) {
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    int dis = Math.Abs(row) + Math.Abs(col);
                    if(dis <= size) {
                        grid[x + col, y + row] = data;
                    }
                }
            }
        }
    }
}
