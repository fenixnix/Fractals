using System;
using System.Collections.Generic;

namespace ToolBox.Map
{
    public enum Direction { Up, Down, Left, Right };

    public class NLocate
    {
        int x = 0;
        int y = 0;

        public NLocate()
        {

        }

        public NLocate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public NLocate(NLocate src)
        {
            x = src.X;
            y = src.Y;
        }

        public bool Equals(NLocate loc)
        {
            if ((X == loc.X) && (Y == loc.Y))
            {
                return true;
            }
            return false;
        }

        public double DistanceTo(NLocate loc)
        {
            double dx = x - loc.X;
            double dy = y - loc.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public NLocate Dir(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Up(); break;
                case Direction.Down: return Down(); break;
                case Direction.Left: return Left(); break;
                case Direction.Right: return Right(); break;
                default: return new NLocate(); break;
            }
            return new NLocate();
        }

        public int ToIndex(NMap map)
        {
            return x + y * map.Width;
        }

        public int ToIndex(int mapWidth)
        {
            return x + y * mapWidth;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public NLocate Up() { return new NLocate(X, Y - 1); }
        public NLocate Down() { return new NLocate(X, Y + 1); }
        public NLocate Left() { return new NLocate(X - 1, Y); }
        public NLocate Right() { return new NLocate(X + 1, Y); }

        public NLocate Up(int length) { return new NLocate(X, Y - length); }
        public NLocate Down(int length) { return new NLocate(X, Y + length); }
        public NLocate Left(int length) { return new NLocate(X - length, Y); }
        public NLocate Right(int length) { return new NLocate(X + length, Y); }

        public NLocate TL() { return new NLocate(X - 1, Y - 1); }
        public NLocate TR() { return new NLocate(X + 1, Y - 1); }
        public NLocate BL() { return new NLocate(X - 1, Y + 1); }
        public NLocate BR() { return new NLocate(X + 1, Y + 1); }

        public NLocate TL(int length) { return new NLocate(X - length, Y - length); }
        public NLocate TR(int length) { return new NLocate(X + length, Y - length); }
        public NLocate BL(int length) { return new NLocate(X - length, Y + length); }
        public NLocate BR(int length) { return new NLocate(X + length, Y + length); }

        public IEnumerable<NLocate> Direction4()
        {
            yield return Up();
            yield return Down();
            yield return Left();
            yield return Right();
            yield break;
        }

        public IEnumerable<NLocate> Direction8()
        {
            yield return Up();
            yield return Down();
            yield return Left();
            yield return Right();
            yield return TL();
            yield return TR();
            yield return BL();
            yield return BR();
            yield break;
        }

        public IEnumerable<NLocate> Square(int dim)
        {
            for (int row = y - dim; row <= y + dim; row++)
            {
                for (int col = x - dim; col <= x + dim; col++)
                {
                    //if(!((x==col)&&(y==row)))
                    yield return new NLocate(col, row);
                }
            }
            yield break;
        }

        public IEnumerable<NLocate> Diamond(int dim)
        {
            for (int row = y - dim; row <= y + dim; row++)
            {
                for (int col = x - dim; col <= x + dim; col++)
                {
                    //if (!((x == col) && (y == row)))
                    //{
                    int dxy = Math.Abs(col - x) + Math.Abs(row - y);
                    if (dxy <= dim)
                    {
                        yield return new NLocate(col, row);
                    }
                    //}
                }
            }
            yield break;
        }

        public IEnumerable<NLocate> Circle(int dim)
        {
            for (int row = y - dim; row <= y + dim; row++)
            {
                for (int col = x - dim; col <= x + dim; col++)
                {
                    //if (!((x == col) && (y == row)))
                    //{
                    int dxy = (int)Math.Round(Math.Sqrt((col - x) * (col - x) + (row - y) * (row - y)));
                    if (dxy <= dim)
                    {
                        yield return new NLocate(col, row);
                    }
                    //}
                }
            }
            yield break;
        }

        public IEnumerable<NLocate> LineTo(NLocate dst)
        {
            NLocate curLoc = this;
            while (true)
            {
                curLoc = curLoc.NearLocTo(dst);
                if (curLoc.Equals(dst))
                {
                    yield break;
                }
                yield return curLoc;
            }
        }

        public IEnumerable<NLocate> Branch(NLocate taget)
        {
            List<NLocate> branch = new List<NLocate>(Direction4());
            branch.RemoveAll((x) => x.Equals(taget));
            branch.RemoveAll((x) => x.Equals(Reverse(taget)));
            return branch;
        }

        public NLocate Reverse(NLocate loc)
        {
            return new NLocate(x - (loc.x - x), y - (loc.y - y));
        }

        public NLocate NearLocTo(NLocate goal)
        {
            List<NLocate> mos = new List<NLocate>(Direction4());
            mos.Sort((x, y) => x.DistanceTo(goal).CompareTo(y.DistanceTo(goal)));
            return mos[0];
        }

        public static void SelfTest()
        {
            var loc = new NLocate(10, 10);
            var revLoc = loc.Reverse(new NLocate(11, 10));
            Console.WriteLine("Reverse:" + revLoc.x + "," + revLoc.y);

            var branch = loc.Branch(new NLocate(11, 10));
            foreach (var b in branch)
            {
                Console.WriteLine("Branch:" + b.x + "," + b.y);
            }

            NMap map = new NMap(20, 20);
            var rect = new NLocate(9, 9).Square(5);
            map.SetBlock(rect, 255);
            Console.WriteLine(map.Print());

            map = new NMap(20, 20);
            var diam = new NLocate(9, 9).Diamond(5);
            map.SetBlock(diam, 255);
            Console.WriteLine(map.Print());

            map = new NMap(20, 20);
            var circle = new NLocate(9, 9).Circle(5);
            map.SetBlock(circle, 255);
            Console.WriteLine(map.Print());

            map = new NMap(20, 20);
            var line = new NLocate(9, 9).LineTo(new NLocate(19, 0));
            map.SetBlock(line, 255);
            Console.WriteLine(map.Print());
        }
    }
}
