using System;
using UnityEngine;
namespace Nixlib.Grid {
    public class Grid2DPaint {
        static public void Rect(Grid2DInt grid, int x, int y, int size = 3, int data = 255) {
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    grid[x + col, y + row] = data;
                }
            }
        }

        static public void Diamond(Grid2DInt grid, int x, int y, int size = 3, int data = 255) {
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    int dis = Math.Abs(row) + Math.Abs(col);
                    if(dis <= size) {
                        grid[x + col, y + row] = data;
                    }
                }
            }
        }

        static public void Line(Grid2DInt grid, Vector2Int p1,Vector2Int p2, int width = 0, int data = 255) {
            Diamond(grid, p1.x, p1.y, width, data);
            Diamond(grid, p2.x, p2.y, width, data);

            Vector2 dif = new Vector2(p2.x - p1.x, p2.y - p1.y);

            int xLength = Math.Abs(p2.x - p1.x);
            for(int i = 0; i < xLength; i++) {
                for(int w = -width; w <= width; w++) {
                    grid[p1.x + (int)(i*dif.x/ xLength), p1.y + (int)(i * dif.y/ xLength) + w] = data;
                }
            }

            int yLength = Math.Abs(p2.y - p1.y);
            for(int i = 0; i < yLength; i++) {
                for(int w = -width; w <= width; w++) {
                    grid[p1.x + (int)(i * dif.x / yLength) + w, p1.y + (int)(i * dif.y / yLength)] = data;
                }
            }
        }
    }
}
