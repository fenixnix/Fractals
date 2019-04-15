using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nixlib.Dungeon {
    public class NFillDeadEnd
    {
        static public void FillOnce(NMap map,byte fillType)
        {
            NLocationRecogition.FindDeadEnd(map, 5);
            map.Fill(5, fillType);
        }

        static public void Fill(NMap map, byte fillType, int cnt)
        {
            for(int i = 0; i < cnt; i++)
            {
                FillOnce(map, fillType);
            }
        }

        public static void SelfTest()
        {
            var map = NDungeon.DrunkMazeWallMap(64, 64);
            Console.WriteLine(map.Print());
            for(int i = 0; i < 10; i++)
            {
                NFillDeadEnd.FillOnce(map, 255);
                Console.WriteLine(map.Print());
            }
        }
    }
}
