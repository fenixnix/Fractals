using Nixlib.Grid;
using UnityEngine;

namespace Nixlib.Dungeon {
    public class NBSP
    {
        public static RectInt RandomSubRect(RectInt rect) {
            int w = Random.Range(rect.width / 2, rect.width);
            int h = Random.Range(rect.height / 2, rect.height);
            int x = Random.Range(rect.x, rect.x + (rect.width - w) / 2);
            int y = Random.Range(rect.y, rect.y + (rect.height - h) / 2);
            return new RectInt(x, y, w, h);
        }

        public static void Division(Grid2DInt map, RectInt rect, int minSideSize = 12, int maxSideSize = 24)
        {
            if (rect.width <= maxSideSize && rect.height <= maxSideSize)
            {
                if ( Random.value<0.25f)
                {
                    Grid2DPaint.Rect(map, RandomSubRect(rect), 0);
                    return;
                }
            }

            if (rect.width <= minSideSize && rect.height <= minSideSize)
            {
                Grid2DPaint.Rect(map, RandomSubRect(rect), 0);
                return;
            }

            bool isVer = Random.value > .5f;
            isVer = (float)rect.width / (float)rect.height > 1.25f;

            int posX = Random.Range(rect.width / 4, rect.width * 3 / 4) + rect.x;
            int posY = Random.Range(rect.height / 4, rect.height * 3 / 4) + rect.y;
            if (isVer)
            {
                //map.SetBlock(posX, rect.Y, 1, rect.H, 0);
                Division(map, new RectInt(rect.x, rect.x, posX - rect.x, rect.height));
                Division(map, new RectInt(posX + 1, rect.y, rect.x + rect.width - posX - 1, rect.height));
            }
            else
            {
                //map.SetBlock(rect.X, posY, rect.W, 1, 0);
                Division(map, new RectInt(rect.x, rect.y, rect.width, posY - rect.y));
                Division(map, new RectInt(rect.x, posY + 1, rect.width, rect.height - posY + rect.y - 1));
            }
        }

        public static string SelfTest()
        {
            Grid2DInt map = Grid2D<int>.Create(64, 64, 255) as Grid2DInt;
            Division(map, new RectInt(0, 0, 64, 64));
            return map.Print();
        }
    }
}
