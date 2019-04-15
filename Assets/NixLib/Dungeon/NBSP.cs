using Nixlib.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Nixlib.Dungeon {
    public class NBSP {
        public List<RectInt> rooms = new List<RectInt>();
        public List<Vector2Int> doors = new List<Vector2Int>();

        public static RectInt RandomSubRect(RectInt rect) {
            int w = Random.Range(rect.width * 2 / 3, rect.width - 2);
            int h = Random.Range(rect.height * 2 / 3, rect.height - 2);
            int x = Random.Range(rect.x + 1, rect.x + (rect.width - w) / 2);
            int y = Random.Range(rect.y + 1, rect.y + (rect.height - h) / 2);
            var r = new RectInt(x, y, w, h);
            Debug.Log(rect + "*" + r);
            return r;
        }

        public static RectInt SubRect(RectInt rect) {
            var r = new RectInt(rect.x + 1, rect.y + 1, rect.width - 2, rect.height - 2);
            Debug.Log(rect + "*" + r);
            return r;
        }

        public void Step() {
            List<RectInt> tmp = new List<RectInt>();
            foreach(var r in rooms) {
                var x = Random.Range(r.x+r.width/4, r.x + r.width - r.width/4);
                var y = Random.Range(r.y + r.height/4, r.y + r.height - r.height/4);
                if(Random.value > .5f) {//Vertical
                    var firstRoom = new RectInt(r.x, r.y, r.width, y - r.y);
                    tmp.Add(firstRoom);
                    tmp.Add(new RectInt(firstRoom.x,y+1,firstRoom.width,r.height-1-firstRoom.height));
                }
                else {//Horizontal
                    var firstRoom = new RectInt(r.x, r.y, x - r.x -1 , r.height);
                    tmp.Add(firstRoom);
                    tmp.Add(new RectInt(x+1, firstRoom.y, r.width - 1 - firstRoom.width, firstRoom.height));
                }
                doors.Add(new Vector2Int(x, y));
            }
            rooms = tmp;
        }

        public string PrintBSP(Grid2DInt map) {
            foreach(var r in rooms) {
                Grid2DPaint.Rect<int>(map, r, 0);
            }
            return map.Print();
        }

        public static void Division(Grid2DInt map, RectInt rect, int minSideSize = 12, int maxSideSize = 24) {
            if(rect.width <= maxSideSize && rect.height <= maxSideSize) {
                if(Random.value < 0.5f) {
                    Grid2DPaint.Rect(map, RandomSubRect(rect), 0);
                    return;
                }
            }

            if(rect.width <= minSideSize && rect.height <= minSideSize) {
                Grid2DPaint.Rect(map, RandomSubRect(rect), 0);
                return;
            }

            bool isVer = Random.value > .5f;
            isVer = (float)rect.width / (float)rect.height > 1.25f;

            int posX = Random.Range(rect.width / 4, rect.width * 3 / 4) + rect.x;
            int posY = Random.Range(rect.height / 4, rect.height * 3 / 4) + rect.y;
            if(isVer) {
                //map.SetBlock(posX, rect.Y, 1, rect.H, 0);
                Division(map, new RectInt(rect.x, rect.x, posX - rect.x, rect.height));
                Division(map, new RectInt(posX + 1, rect.y, rect.x + rect.width - posX - 1, rect.height));
            }
            else {
                //map.SetBlock(rect.X, posY, rect.W, 1, 0);
                Division(map, new RectInt(rect.x, rect.y, rect.width, posY - rect.y));
                Division(map, new RectInt(rect.x, posY + 1, rect.width, rect.height - posY + rect.y - 1));
            }
        }

        public static string SelfTest() {
            Grid2DInt map = new Grid2DInt(64, 64, 255);
            if(map == null) {
                Debug.LogError("null map");
            }
            Division(map, new RectInt(0, 0, 64, 64));
            return map.Print();
        }
    }
}