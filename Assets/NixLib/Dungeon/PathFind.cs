using System;
using System.Collections.Generic;

namespace Nixlib.Dungeon {
    public class PathFind
    {
        static List<NLocate> GPath = new List<NLocate>();

        static public List<NLocate> AStar(NLocate src, NLocate dst, NMap map)
        {
            GPath.Clear();
            NPathNode rootNode = new NPathNode(src.X, src.Y);
            List<int> closeList = new List<int>();
            Dictionary<int, NPathNode> openList = new Dictionary<int, NPathNode>();
            openList.Add(src.ToIndex(map.Width),rootNode);
            //检查curPos可移动格（openList中尚未存在的，closeList中没有的）
            //并加入routeData OpenList（将其父类设置为curPos）;
            //如果openList包含目标则从目标格开始返回结果
            //int count = 3;
            //float maxCost = start.distanceToPoint(goal)*4;
            while (openList.Count>0)
            {
                //Console.WriteLine("CurrentLocate:" + rootNode.Locate.X + "*" + rootNode.Locate.Y);
                foreach (var l in rootNode.Locate.Direction4()){

                    if (l.Equals(dst))
                    {
                        return new List<NLocate>(rootNode.GetPath());
                    }

                    if (map.Alive(l)||(!map.WithIn(l)))
                    {
                        continue;
                    }

                    int index = l.ToIndex(map.Width);

                    if (!closeList.Contains(index)){
                        if (!openList.ContainsKey(index))
                        {
                            openList.Add(index,rootNode.AddChild(l.X,l.Y));
                        }
                    }
                }
                int curIndex = rootNode.Locate.ToIndex(map.Width);
                openList.Remove(curIndex);
                closeList.Add(curIndex);

                //qDebug()<<__FUNCTION__<<__LINE__;
                //检查openList最小损耗格，将curPos加入closeList;将最小消耗格设curPos
                int minTotelCost = int.MaxValue;
                foreach (var l in openList.Values){
                    if (l.TotelCost < minTotelCost)
                    {
                        minTotelCost = l.TotelCost;
                        rootNode = l;
                    }
                }
            }

            if ((GPath != null)&&GPath.Count>0)
            {
                Console.WriteLine("find route!!!");
                return GPath;
            }
            return new List<NLocate>();
        }

        static public List<NLocate> BStar(NLocate src,NLocate dst, NMap map)
        {
            GPath.Clear();
            NPathNode rootNode = new NPathNode(src.X, src.Y);
            List<int> bufferIndex = new List<int>();
            //return 
            BStarStep(src, dst, map, bufferIndex, rootNode);
            if (GPath != null)
            {
                return GPath;
            }
            Console.WriteLine("Fail to Find Path!!!");
            return new List<NLocate>();
        }

        static public void BStarStep(NLocate src, NLocate dst,NMap map,List<int> bufferIndex,NPathNode pathList)
        {
            if (src.Equals(dst)) { Console.WriteLine("FindPath with B*");
                GPath = new List<NLocate>(pathList.GetPath());
                return;
            }
            var dstLoc = src.NearLocTo(dst);
            if (!map.Alive(dstLoc))
            {
                bufferIndex.Add(dstLoc.ToIndex(map));
                BStarStep(dstLoc, dst, map, bufferIndex, pathList.AddChild(dstLoc.X, dstLoc.Y));
            }
            else
            {
                foreach(var l in src.Branch(dstLoc))
                {
                    if (!bufferIndex.Contains(l.ToIndex(map.Width))&& !map.Alive(l))
                    {
                        bufferIndex.Add(l.ToIndex(map));
                        BStarStep(l, dst, map, bufferIndex, pathList.AddChild(l.X, l.Y));
                    }
                }
            }
        }

        static public void SelfTest()
        {
            {
                Console.WriteLine("B* SelfTest:");
                var map = new NMap(20, 20);
                NLocate loc = new NLocate(10, 5);
                map.SetBlock(loc.Square(4), 255);
                loc = new NLocate(10, 10);
                map.SetBlock(loc.Square(3), 255);
                map.SetBlock(loc.LineTo(new NLocate(10, 17)), 255);
                loc = new NLocate(10, 17);
                map.SetBlock(loc.LineTo(new NLocate(2, 17)), 255);
                Console.WriteLine(map.Print());
                var path = BStar(new NLocate(0, 12), new NLocate(18, 9), map);
                foreach (var p in path)
                {
                    map.SetBlock(p, 1);
                }
                Console.WriteLine(map.Print());
            }
            {
                Console.WriteLine("A* SelfTest:");
                var map = new NMap(20, 20);
                NLocate loc = new NLocate(10, 5);
                map.SetBlock(loc.Square(4), 255);
                loc = new NLocate(10, 10);
                map.SetBlock(loc.Square(3), 255);
                map.SetBlock(loc.LineTo(new NLocate(10, 17)), 255);
                loc = new NLocate(10, 17);
                map.SetBlock(loc.LineTo(new NLocate(2, 17)), 255);
                Console.WriteLine(map.Print());
                var path = AStar(new NLocate(0, 12), new NLocate(18, 9), map);
                foreach (var p in path)
                {
                    map.SetBlock(p, 1);
                }
                Console.WriteLine(map.Print());
            }
        }
    }
}
