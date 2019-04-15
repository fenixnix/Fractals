using System;
using System.Collections.Generic;

namespace Nixlib.Dungeon {
    public class NMove
    {
        public static void Noise(NMap map, NLocate src,NLocate dst,byte movType, byte digType)
        {
            if (src.Equals(dst))
            {
                return;
            }
            if (map.WithIn(src))
            {
                map.SetBlock(src.X,src.Y,digType);
            }

            List<NLocate> mos = new List<NLocate>();
            mos.AddRange(src.Direction4());
            mos.Sort((x, y) => x.DistanceTo(dst).CompareTo(y.DistanceTo(dst)));

            Dictionary<NLocate, double> rateDict = new Dictionary<NLocate, double>();
            if (map.WithIn(mos[0]))
            {
                rateDict.Add(mos[0], 0.6);
            }
            if (map.WithIn(mos[1]))
            {
                rateDict.Add(mos[1], 0.3);
            }
            if (map.WithIn(mos[2]))
            {
                rateDict.Add(mos[2], 0.1);
            }
            if (map.WithIn(mos[3]))
            {
                rateDict.Add(mos[3], 0.1);
            }

            NLocate mov = RandomSelectByRate<NLocate>.Select(rateDict);

            Noise(map,mov, dst, movType, digType);
        }

        public static void Line(NMap map, NLocate src, NLocate dst,byte digType)
        {
            if (src.Equals(dst))
            {
                return;
            }
            if (map.WithIn(src))
            {
                map.SetBlock(src.X, src.Y, digType);
            }

            List<NLocate> mos = new List<NLocate>();
            mos.AddRange(src.Direction4());
            mos.Sort((x, y) => x.DistanceTo(dst).CompareTo(y.DistanceTo(dst)));
            Line(map, mos[0], dst, digType);
        }

        public static void AxisLine(NMap map, NLocate src, NLocate dst, byte digType)
        {
            if (src.Equals(dst))
            {
                return;
            }
            int dx = dst.X - src.X;
            int dy = dst.Y - src.Y;
            NLocate crossLoc = Math.Abs(dx) > Math.Abs(dy) ? new NLocate(dst.X, src.Y) : new NLocate(src.X, dst.Y);
            Line(map, src, crossLoc, digType);
            Line(map, dst, crossLoc, digType);
            map.SetBlock(crossLoc, digType);
        }

        public static void DrunkardWalk(NMap map, NLocate start, byte val)
        {
            NLocate currentLocation = start;
            Stack<NLocate> rounte = new Stack<NLocate>();
            rounte.Push(start);
            DrunkWalkItr(map,currentLocation, rounte, val);
        }

        static void DrunkWalkItr(NMap map, NLocate currentLoc, Stack<NLocate> rounte, byte val, int step = 1)
        {
            if (rounte.Count > map.DataSize()/(4*step))
            {
                Console.WriteLine("I am awake !!!");
                return;
            }

            map.SetBlock(currentLoc, val);
            List<NLocate> locs = new List<NLocate>();
            List<NLocate> stepAroundLocs = new List<NLocate>();
            stepAroundLocs.Add(new NLocate(currentLoc.X + step, currentLoc.Y));
            stepAroundLocs.Add(new NLocate(currentLoc.X - step, currentLoc.Y));
            stepAroundLocs.Add(new NLocate(currentLoc.X, currentLoc.Y + step));
            stepAroundLocs.Add(new NLocate(currentLoc.X, currentLoc.Y - step));
            //Console.WriteLine(map.Print());
            foreach(var l in stepAroundLocs)
            {
                if ((map.GetBlock(l) != val)&&(map.WithIn(l)))
                {
                    locs.Add(l);
                }
            }
            if (locs.Count > 0)
            {
                var dLoc = RandomSelect<NLocate>.Select(locs);
                Line(map, currentLoc, dLoc, val);
                rounte.Push(dLoc);
                DrunkWalkItr(map, dLoc, rounte, val);
            }
            else
            {
                rounte.Pop();
                if (rounte.Count > 0)
                {
                    
                    DrunkWalkItr(map, rounte.Peek(), rounte, val);
                }
                else
                {
                    Console.WriteLine("no way!!!");
                    return;
                }
            }



        }

        public static void SelfTest()
        {
            NLocate src = new NLocate(20, 20);
            NLocate dst = new NLocate(39,39);

            NMap map = new NMap(40, 40,255);

            Noise(map, src, dst, 0, 0);
            Noise(map, dst, new NLocate(0, 39), 0, 0);
            Console.WriteLine(map.Print());

            map = new NMap(40, 40, 255);
            Line(map, src, dst, 0);
            Console.WriteLine(map.Print());

            map = new NMap(40, 40, 255);
            AxisLine(map, src, dst,0);
            Console.WriteLine(map.Print());

            map = new NMap(40, 40, 255);
            DrunkardWalk(map, src, 0);
            Console.WriteLine(map.Print());
        }
    }
}
