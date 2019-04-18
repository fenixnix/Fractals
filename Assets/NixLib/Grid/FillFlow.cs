using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nixlib.Grid {
    public class FillFlow {
        public Grid2DInt flowGrid;
        public int maxLength = 0;
        public List<Vector2Int> lastEdges = new List<Vector2Int>();

        public Grid2DInt Fill(Grid2DInt grid,Vector2Int point) {
            flowGrid = new Grid2DInt(grid.Width, grid.Height);
            maxLength = 0;
            lastEdges.Clear();
            lastEdges.Add(point);
            Flow(grid, 0);
            return flowGrid;
        }

        void Flow(Grid2DInt grid,int length) {
            if(lastEdges.Count <= 0) return;
            foreach(var point in lastEdges) {
                flowGrid[point] = length;
            }
            length++;
            maxLength = Math.Max(length, maxLength);
            List<Vector2Int> tmpPoints = new List<Vector2Int>();
            foreach(var point in lastEdges) {
                foreach(var p in Grid2DInt.ortho) {
                    var pp = point + p;
                    if(Contain(pp, tmpPoints))continue;
                    if((grid[pp] != 255) || (flowGrid[pp] != 0)) {
                        continue;
                    }
                    tmpPoints.Add(pp);
                }
            }
            lastEdges = tmpPoints;
            Flow(grid, length);
        }

        bool Contain(Vector2Int p,List<Vector2Int> list) {
            foreach(var v in list) {
                if((v.x == p.x) && (v.y == p.y)) return true;
            }
            return false;
        }
    }
}
