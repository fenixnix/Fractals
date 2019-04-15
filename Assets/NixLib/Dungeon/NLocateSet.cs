using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Map
{
    public class NLocateSet
    {
        public List<NLocate> Set = new List<NLocate>();

        public double DistanceTo(NLocateSet set, out NLocate srcLoc, out NLocate dstLoc)
        {
            double min = double.MaxValue;
            srcLoc = new NLocate(0, 0);
            dstLoc = new NLocate(0, 0);
            foreach(var l in set.Set)
            {
                NLocate nearestLoc;
                var dis = DistanceTo(l,out nearestLoc);
                if (dis < min)
                {
                    min = dis;
                    srcLoc = nearestLoc;
                    dstLoc = l;
                }
            }
            return min;
        }

        public double DistanceTo(NLocate loc, out NLocate nearestLoc)
        {
            double min = double.MaxValue;
            nearestLoc = loc;
            foreach (var l in Set)
            {
                var dis = l.DistanceTo(loc);
                if (dis < min)
                {
                    min = dis;
                    nearestLoc = l;
                }
            }
            return min;
        }

        public void Fill(NMap map, byte val)
        {
            foreach( var l in Set)
            {
                map.SetBlock(l.X, l.Y, val);
            }
        }

        public int AreaSize()
        {
            return Set.Count;
        }
    }
}
