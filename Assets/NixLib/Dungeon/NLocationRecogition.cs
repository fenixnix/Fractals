using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nixlib.Dungeon {
    public class NLocationRecogition
    {
        static public void FindDoor(NMap map, byte mark)
        {
            List<string> types = new List<string>();
            types.Add(
                "000" +
                "101" +
                "101");


            types.Add(
                "101" +
                "101" +
                "000");

 
            types.Add(
                "110" +
                "000" +
                "110");



            types.Add(
                "011" +
                "000" +
                "011");

 
            MatchTypes(map, mark, types);
        }

        static public void FindTreasure(NMap map, byte mark)
        {
            List<string> types = new List<string>();
            types.Add(
                "00000" +
                "00000" +
                "00000" +
                "11111" +
                "11111");

            types.Add(
                "11000" +
                "11000" +
                "11000" +
                "11000" +
                "11000");

            types.Add(
                "00011" +
                "00011" +
                "00011" +
                "00011" +
                "00011");

            types.Add(
                "11111" +
                "11111" +
                "00000" +
                "00000" +
                "00000");

            MatchTypes(map, mark, types);
        }

        static public void FindDeadEnd(NMap map, byte mark)
        {
            List<string> types = new List<string>();
            types.Add(
                "111" +
                "101" +
                "101");

            types.Add(
                "101" +
                "101" +
                "111");

            types.Add(
                "111" +
                "100" +
                "111");

            types.Add(
                "111" +
                "001" +
                "111");
            types.Add(
                "111" +
                "101" +
                "100");

            types.Add(
                "100" +
                "101" +
                "111");

            types.Add(
                "110" +
                "100" +
                "111");

            types.Add(
                "011" +
                "001" +
                "111");
            types.Add(
                "111" +
                "101" +
                "001");

            types.Add(
                "001" +
                "101" +
                "111");

            types.Add(
                "111" +
                "100" +
                "110");

            types.Add(
                "111" +
                "001" +
                "011");
            MatchTypes(map, mark, types);
        }

        static public void FindPassage(NMap map, byte mark)
        {
            List<string> types = new List<string>();
            types.Add(
                "101" +
                "101" +
                "101");

            types.Add(
                "111" +
                "000" +
                "111");
            
            MatchTypes(map, mark, types);
        }

        static public void MatchCountAround(NMap map, byte valid, byte mark, int cnt)
        {
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Width; col++)
                {
                    if (map.CountAround(col, row, valid) == cnt) ;
                    {
                        map.SetBlock(col, row, mark);
                    }
                }
            }
        }

        private static void MatchTypes(NMap map, byte mark, List<string> types)
        {
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Width; col++)
                {
                    foreach (var t in types)
                    {
                        if (map.Match(col, row, t))
                        {
                            map.SetBlock(col, row, mark);
                        }
                    }
                }
            }
        }

        public static void SelfTest()
        {
            var map = NDungeon.RoomWallMap(64, 64);
            //FindDoor(map, (byte)'M');
            //var map = new NMap(16, 16,0);
            //map.SetBlock(new NRect(0,0,8,16), 255);
            FindTreasure(map, (byte)'M');
            Console.WriteLine(map.Print());
        }
    }
}
