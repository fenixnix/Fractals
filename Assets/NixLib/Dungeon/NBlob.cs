using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nixlib.CellularAutomata;

namespace Nixlib.Dungeon
{
    class NBlob
    {
        public List<NLocateSet> Blobs = new List<NLocateSet>();
        
        public void Draw(NMap map)
        {
            byte cnt = 0;
            foreach (var b in Blobs)
            {
                b.Fill(map, cnt);
                cnt++;
            }
        }

        public int LagestBlob()
        {
            int max = 0;
            int maxIndex = 0;

            for (int index = 0; index < Blobs.Count; index++)
            {
                if (Blobs[index].Set.Count > max)
                {
                    max = Blobs[index].Set.Count;
                    maxIndex = index;
                }
            }
            return maxIndex;
        }

        public static NBlob Find(NMap map, byte valid)
        {
            var res = new NBlob();
            var tmp = new NMap(map);
            for (int i = 0; i < tmp.DataSize(); i++)
            {
                if (tmp.GetBlock(i) == valid)
                {
                    var set = new NLocateSet();
                    FloodFillBlob(tmp, new NLocate(i % tmp.Width, i / tmp.Width), set, valid);
                    res.Blobs.Add(set);
                }
            }
            return res;
        }

        static void FloodFillBlob(NMap map, NLocate loc, NLocateSet set, byte valid)
        {
            if (!map.WithIn(loc))
            {
                return;
            }
            if (map.GetBlock(loc.X, loc.Y) == valid)
            {
                map.SetBlock(loc.X, loc.Y, (byte)(255 - valid));
                set.Set.Add(loc);
                foreach (var l in loc.Direction4())
                {
                    FloodFillBlob(map, l, set, valid);
                }
            }
        }

        public delegate void ConnectOnceType(NMap map, byte val);

        public void LineConnect(NMap map, byte val)
        {
            Connect(map, val, LineConnectOnce);
        }

        public void AxisConnect(NMap map, byte val)
        {
            Connect(map, val, AxisConnectOnce);
        }

        public void NoiseConnect(NMap map, byte val)
        {
            Connect(map, val, NoiseConnectOnce);
        }

        public void Connect(NMap map, byte val,ConnectOnceType connectOnceType)
        {
            var cnt = 0;

            while (Blobs.Count > 1)
            {
                connectOnceType(map, val);
                Blobs = Find(map, val).Blobs;


                if (cnt > 100)
                {
                    break;
                }
                cnt++;
            }
        }

        public void LineConnectOnce(NMap map, byte val)
        {
            NLocate loc1, loc2;
            FindLocationPair(map, val, out loc1, out loc2);
            NMove.Line(map, loc1, loc2, val);
        }

        public void AxisConnectOnce(NMap map, byte val)
        {
            NLocate loc1, loc2;
            FindLocationPair(map, val, out loc1, out loc2);
            NMove.AxisLine(map, loc1, loc2, val);
        }

        public void NoiseConnectOnce(NMap map, byte val)
        {
            NLocate loc1, loc2;
            FindLocationPair(map, val, out loc1, out loc2);
            NMove.Noise(map, loc1, loc2, 0, val);
        }

        public void FindLocationPair(NMap map, byte val, out NLocate loc1, out NLocate loc2)
        {
            double minVal = double.MaxValue;
            NLocate minLoc1 = new NLocate(0, 0);
            NLocate minLoc2 = new NLocate(0, 0);
            for (int i = 1; i < Blobs.Count; i++)
            {
                NLocate tloc1, tloc2;
                var dis = Blobs[0].DistanceTo(Blobs[i], out tloc1, out tloc2);
                if (dis < minVal)
                {
                    minVal = dis;
                    minLoc1 = tloc1;
                    minLoc2 = tloc2;
                }
            }
            loc1 = minLoc1;
            loc2 = minLoc2;
        }


        void FillSmallBlob(NMap map, int lessThan, byte val = 255)
        {
            foreach (var blob in Blobs)
            {
                if (blob.AreaSize() <= lessThan)
                {
                    blob.Fill(map, val);
                }
            }
        }

        public void FillByLeftBlob(NMap map, int cnt = 1, byte val = 255)
        {
            while (Blobs.Count > cnt)
            {
                Blobs.Sort((x, y) => x.AreaSize().CompareTo(y.AreaSize()));
                Blobs[0].Fill(map, val);
                Blobs.RemoveRange(0, 1);
            }
        }

        static public void SelfTest()
        {
            NMap map = new NMap(64, 32);
            map.Noise(0.45f);
            CellularAutomata2D rule = new CellularAutomata2D("s45678b678");
            map = rule.RunStep(map);
            map = rule.RunStep(map);
            Console.WriteLine(map.Print());
            var blobs = Find(map, 255);
            blobs.Blobs[blobs.LagestBlob()].Fill(map, 0);
            blobs = Find(map, 255);
            Console.WriteLine(map.Print());
            blobs.FillByLeftBlob(map, 5, 0);
            Console.WriteLine(map.Print());
            Console.WriteLine("Start Connect Blob!");
            blobs.NoiseConnect(map, 255);
            Console.WriteLine(map.Print());
            Console.WriteLine("Pass!");
        }
    }
}
