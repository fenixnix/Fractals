using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Map
{
    public class NBSP
    {
        public static void Division(NMap map, NRect rect, int minSideSize = 12, int maxSideSize = 24)
        {

            if (rect.W <= maxSideSize && rect.H <= maxSideSize)
            {
                if (RandomNum.Roll(0.25f))
                {
                    map.SetBlock(rect.RndInside(), 0);
                    return;
                }
            }

            if (rect.W <= minSideSize && rect.H <= minSideSize)
            {
                map.SetBlock(rect.RndInside(), 0);
                return;
            }

            bool isVer = RandomNum.Roll(0.5f);
            if ((float)rect.W / (float)rect.H > 1.25)
            {
                isVer = true;
            }
            if ((float)rect.H / (float)rect.W > 1.25)
            {
                isVer = false;
            }

            int posX = RandomNum.Value(rect.W / 4, rect.W * 3 / 4) + rect.X;
            int posY = RandomNum.Value(rect.H / 4, rect.H * 3 / 4) + rect.Y;
            if (isVer)
            {
                //map.SetBlock(posX, rect.Y, 1, rect.H, 0);
                Division(map, new NRect(rect.X, rect.Y, posX - rect.X, rect.H));
                Division(map, new NRect(posX + 1, rect.Y, rect.X + rect.W - posX - 1, rect.H));
            }
            else
            {
                //map.SetBlock(rect.X, posY, rect.W, 1, 0);
                Division(map, new NRect(rect.X, rect.Y, rect.W, posY - rect.Y));
                Division(map, new NRect(rect.X, posY + 1, rect.W, rect.H - posY + rect.Y - 1));
            }
        }

        public static void SelfTest()
        {
            NMap map = new NMap(64, 64, 255);
            Division(map, new NRect(0, 0, 64, 64));
            Console.WriteLine(map.Print());
        }


    }
}
