using System.Collections.Generic;
using UnityEngine;

namespace Grid {
    public class Walker2D {
        public enum WalkType {Direction4,Direction8};

        Vector2Int curPos;
        public Vector2Int CurrentPosition => curPos;
        public void Init(Vector2Int pos) {
            curPos = pos;
        }

        public Walker2D(Vector2Int pos) {
            Init(pos);
        }

        static List<Vector2Int> moveTable = new List<Vector2Int>();
        public static void InitMovTab(WalkType type) {
            moveTable.Clear();
            foreach(var p in Grid2D.ortho) {
                moveTable.Add(p);
            }
            if(type == WalkType.Direction8) {
                foreach(var p in Grid2D.diago) {
                    moveTable.Add(p);
                }
            }
        }

        public Vector2Int Step(Grid2D grid) {
            Vector2Int nextPos;
            do {
                nextPos = curPos + moveTable[Random.Range(0, moveTable.Count)];
            } while(!grid.Inside(nextPos.x, nextPos.y));
            curPos = nextPos;
            //nextPos = curPos + moveTable[Random.Range(0, moveTable.Count)];
            //if(grid.Inside(nextPos.x, nextPos.y)) {
            //    curPos = nextPos;
            //}
            return curPos;
        }
    }
}
