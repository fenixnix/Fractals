using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Map
{
    public class NMap
    {
        public static byte Live = 255;
        public static byte Dead = 0;

        public int Width = 1;
        public int Height = 1;
        public List<byte> Datas = new List<byte>();

        public NMap(int w, int h, byte data = 0)
        {
            Width = w;
            Height = h;
            Datas = new List<byte>();
            for(int i = 0; i < DataSize(); i++)
            {
                Datas.Add(data);
            }
        }

        public NMap(NMap map)
        {
            Width = map.Width;
            Height = map.Height;
            Datas.Clear();
            for (int i = 0; i < DataSize(); i++)
            {
                Datas.Add(map.GetBlock(i));
            }
        }

        public NMap InverseVal()
        {
            var res = new NMap(Width, Height);
            for(int i = 0; i < DataSize(); i++)
            {
                res.SetBlock(i,((byte)(255 - Datas[i])));
            }
            return res;
        }

        public NMap MirrorY()
        {
            var res = new NMap(Width, Height);
            for(int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    res.SetBlock(Width - col-1,row, GetBlock(col, row));
                }
            }
            return res;
        }

        public NMap MirrorYExpendRight()
        {
            var res = new NMap(Width + Width, Height);
            res.SetBlock(Width, 0, MirrorY());
            res.SetBlock(0, 0, this);
            return res;
        }

        public NMap MirrorX()
        {
            var res = new NMap(Width, Height);
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    res.SetBlock(col, Height - 1-row, GetBlock(col, row));
                }
            }
            return res;
        }

        public NMap MirrorXExpendDown()
        {
            var res = new NMap(Width, Height + Height);
            res.SetBlock(0, Height, MirrorX());
            res.SetBlock(0, 0, this);
            return res;
        }

        public void Noise(float rate,byte val = 255)
        {
            for (int i = 0; i < DataSize(); i++)
            {
                if (RandomNum.Roll(rate))
                {
                    Datas[i] = val;
                }
            }
        }

        public int DataSize()
        {
            return Width * Height;
        }

        internal int CountAround(int x, int y, byte val)
        {
            int sum = 0;
            var loc = new NLocate(x, y);
            foreach(var l in loc.Direction8())
            {
                if(GetBlock(l)== val)
                {
                    sum++;
                }
            }
            return sum;
        }

        public void Fill(byte mark,byte fillType)
        {
            for(int i = 0; i < DataSize(); i++)
            {
                if(GetBlock(i) == mark)
                {
                    SetBlock(i, fillType);
                }
            }
        }

        public byte GetBlock(int i)
        {
            return Datas[i];
        }

        internal NLocate Center()
        {
            return new NLocate(Width / 2, Height / 2);
        }

        public byte GetBlock(int x, int y)
        {
            return Datas[Index(x, y)];
        }

        public byte GetBlock(NLocate loc)
        {
            return GetBlock(loc.X, loc.Y);
        }

        public void SetBlock(int index,byte val)
        {
            Datas[index] = val;
        }

        public void SetBlock(NLocate loc,byte val)
        {
            SetBlock(loc.X, loc.Y, val);
        }

        public void SetBlock(int x, int y, byte val)
        {
            SetBlock(Index(x, y), val);
        }

        public void SetBlock(NRect rect, byte val)
        {
            SetBlock(rect.X,rect.Y,rect.W,rect.H, val);
        }

        public void SetBlock(int x, int y, int w, int h, byte val)
        {
            for(int row = y; row < y + h; row++)
            {
                for(int col = x; col < x + w; col++)
                {
                    SetBlock(col, row, val);
                }
            }
        }

        public void SetBlock(IEnumerable<NLocate> locs,byte val)
        {
            foreach(var l in locs)
            {
                SetBlock(l, val);
            }
        }

        public void SetBlock(int x, int y, NMap src)
        {
            for (int row = 0; row < src.Height; row++)
            {
                for (int col = 0; col < src.Width; col++)
                {
                    SetBlock(x+col, y+row, src.GetBlock(col,row));
                }
            }
        }

        public bool Match(int x,int y,string str)
        {
            if(str.Length == 9)
            {
                return Match3x3(x, y, str);
            }
            if(str.Length == 25)
            {
                return Match5x5(x, y, str);
            }
            return false;
        }

        public bool Match3x3(int x, int y, string str)
        {
            if (x < 1 || x >= Width - 1 || y < 1 || y >= Height - 1)
            {
                return false;
            }
            string src = "";
            for(int row = -1; row <= 1; row++)
            {
                for(int col = -1; col <= 1; col++)
                {
                    src += Alive(x+col, y+row) ? "1" : "0";
                }
            }
            return src == str;
        }

        public bool Match5x5(int x, int y, string str)
        {
            if (x < 2 || x >= Width - 2 || y < 2 || y >= Height - 2)
            {
                return false;
            }
            string src = "";
            for (int row = -2; row <= 2; row++)
            {
                for (int col = -2; col <= 2; col++)
                {
                    src += Alive(x+col, y+row) ? "1" : "0";
                }
            }
            return src == str;
        }

        public bool Alive(NLocate loc)
        {
            return Alive(loc.X, loc.Y);
        }

        public bool Alive(int x, int y)
        {
            return GetBlock(x, y) == Live;
        }

        public bool Alive(int index)
        {
            return GetBlock(index) == Live;
        }

        public bool IsDead(int x, int y)
        {
            return GetBlock(x, y) == Dead;
        }

        public bool IsDead(int index)
        {
            return GetBlock(index) == Dead;
        }

        int Index(int x, int y)
        {
            x = (x + Width) % Width;
            y = (y + Height) % Height;
            return x + y * Width;
        }

        public NLocate GetLoc(int index)
        {
            return new NLocate(index % Width, index / Width);
        }

        public bool WithIn(NLocate loc)
        {
            if ((loc.X < 0) || (loc.X >= Width) || (loc.Y < 0) || (loc.Y >= Height))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<NLocate> TopLocates(byte val = 255)
        {
            bool finded = false;
            for(int row = 0; row < Height; row++)
            {
                for(int col = 0; col < Width; col++)
                {
                    if(GetBlock(col,row) == val)
                    {
                        finded = true;
                        yield return new NLocate(col, row);
                    }
                }
                if (finded)
                {
                    yield break;
                }
            }
        }

        public IEnumerable<NLocate> BottomLocates(byte val = 255)
        {
            bool finded = false;
            for (int row = Height -1 ; row >=0; row--)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (GetBlock(col, row) == val)
                    {
                        finded = true;
                        yield return new NLocate(col, row);
                    }
                }
                if (finded)
                {
                    yield break;
                }
            }
        }

        public IEnumerable<NLocate> LeftLocates(byte val = 255)
        {
            bool finded = false;
            for (int col = 0; col < Width; col++)
            {
                for (int row = 0; row < Height; row++)
                {
                    if (GetBlock(col, row) == val)
                    {
                        finded = true;
                        yield return new NLocate(col, row);
                    }
                }
                if (finded)
                {
                    yield break;
                }
            }
        }

        public IEnumerable<NLocate> RightLocates(byte val = 255)
        {
            bool finded = false;
            for (int col = Width -1; col >= 0; col--)
            {
                for (int row = 0; row < Height; row++)
                {
                    if (GetBlock(col, row) == val)
                    {
                        finded = true;
                        yield return new NLocate(col, row);
                    }
                }
                if (finded)
                {
                    yield break;
                }
            }
        }

        public string Print()
        {
            ASCIIArt.Init();
            string txt = "";
            txt += ASCIIArt.DrawUpFrame(Width);
            for (int i = 0; i < Datas.Count; i++)
            {
                if (i % Width == 0)
                {
                    txt += ASCIIArt.DrawUnicode(187);
                }

                if (Datas[i] == Live)
                {
                    //txt += ASCIIArt.DrawUnicode(220);
                    txt += "#";
                }
                else
                {
                    if(Datas[i] == Dead)
                    {
                        //txt += ASCIIArt.DrawUnicode(198);
                        txt += ".";
                    }
                    else
                    {
                        txt += Datas[i];
                        //txt += ASCIIArt.DrawUnicode(179);
                    }
                }

                if (i % Width == Width - 1)
                {
                    txt += ASCIIArt.DrawUnicode(187) + "\n";
                }
            }
            txt += ASCIIArt.DrawDownFrame(Width);
            return txt;
        }

        public static void SelfTest()
        {
            Console.WriteLine("Test:NMap\n");
            var map = new NMap(16, 16);
            map.Noise(0.5f);
            Console.WriteLine("OriMap:");
            Console.WriteLine(map.Print());
            map = map.InverseVal();
            Console.WriteLine("InverseValMap:");
            Console.WriteLine(map.Print());
            map = map.MirrorY();
            Console.WriteLine("MirrorY Map:");
            Console.WriteLine(map.Print());

            map = map.MirrorYExpendRight();
            Console.WriteLine("MirrorY Expend Right Map:");
            Console.WriteLine(map.Print());

            map = map.MirrorX();
            Console.WriteLine("MirrorX Map:");
            Console.WriteLine(map.Print());

            map = map.MirrorXExpendDown();
            Console.WriteLine("MirrorX Expend Right Map:");
            Console.WriteLine(map.Print());

            map = NDungeon.CaveWallMap(32, 32, 0.5f, 12);
            List<NLocate> locs = new List<NLocate>();
            locs.AddRange(map.TopLocates());
            locs.AddRange(map.BottomLocates());
            locs.AddRange(map.LeftLocates());
            locs.AddRange(map.RightLocates());
            map.SetBlock(locs, 3);
            Console.WriteLine(map.Print());
        }
    }
}
