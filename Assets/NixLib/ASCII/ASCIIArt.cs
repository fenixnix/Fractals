using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToolBox
{
    public class ASCIIArt
    {
        static bool isInit = false;

        static string CodePage437UnicodeMapString = "\u0000,\u263A,\u263B,\u2665,\u2666,\u2663,\u2660,\u2022,\u25D8,\u25CB,\u25D9,\u2642,\u2640,\u266A,\u266B,\u263C,\u25BA,\u25C4,\u2195,\u203C,\u00B6,\u00A7,\u25AC,\u21A8,\u2191,\u2193,\u2192,\u2190,\u221F,\u2194,\u25B2,\u25BC,\u0020,\u0021,\u0022,\u0023,\u0024,\u0025,\u0026,\u0027,\u0028,\u0029,\u002A,\u002B,\u002C,\u002D,\u002E,\u002F,\u0030,\u0031,\u0032,\u0033,\u0034,\u0035,\u0036,\u0037,\u0038,\u0039,\u003A,\u003B,\u003C,\u003D,\u003E,\u003F,\u0040,\u0041,\u0042,\u0043,\u0044,\u0045,\u0046,\u0047,\u0048,\u0049,\u004A,\u004B,\u004C,\u004D,\u004E,\u004F,\u0050,\u0051,\u0052,\u0053,\u0054,\u0055,\u0056,\u0057,\u0058,\u0059,\u005A,\u005B,\u005C,\u005D,\u005E,\u005F,\u0060,\u0061,\u0062,\u0063,\u0064,\u0065,\u0066,\u0067,\u0068,\u0069,\u006A,\u006B,\u006C,\u006D,\u006E,\u006F,\u0070,\u0071,\u0072,\u0073,\u0074,\u0075,\u0076,\u0077,\u0078,\u0079,\u007A,\u007B,\u007C,\u007D,\u007E,\u2302,\u00C7,\u00FC,\u00E9,\u00E2,\u00E4,\u00E0,\u00E5,\u00E7,\u00EA,\u00EB,\u00E8,\u00EF,\u00EE,\u00EC,\u00C4,\u00C5,\u00C9,\u00E6,\u00C6,\u00F4,\u00F6,\u00F2,\u00FB,\u00F9,\u00FF,\u00D6,\u00DC,\u00A2,\u00A3,\u00A5,\u20A7,\u0192,\u00E1,\u00ED,\u00F3,\u00FA,\u00F1,\u00D1,\u00AA,\u00BA,\u00BF,\u2310,\u00AC,\u00BD,\u00BC,\u00A1,\u00AB,\u00BB,\u2591,\u2592,\u2593,\u2502,\u2524,\u2561,\u2562,\u2556,\u2555,\u2563,\u2551,\u2557,\u255D,\u255C,\u255B,\u2510,\u2514,\u2534,\u252C,\u251C,\u2500,\u253C,\u255E,\u255F,\u255A,\u2554,\u2569,\u2566,\u2560,\u2550,\u256C,\u2567,\u2568,\u2564,\u2565,\u2559,\u2558,\u2552,\u2553,\u256B,\u256A,\u2518,\u250C,\u2588,\u2584,\u258C,\u2590,\u2580,\u03B1,\u00DF,\u0393,\u03C0,\u03A3,\u03C3,\u00B5,\u03C4,\u03A6,\u0398,\u03A9,\u03B4,\u221E,\u03C6,\u03B5,\u2229,\u2261,\u00B1,\u2265,\u2264,\u2320,\u2321,\u00F7,\u2248,\u00B0,\u2219,\u00B7,\u221A,\u207F,\u00B2,\u25A0,\u00A0";
        public static List<string> CodePage437UnicodeMap = new List<string>();

        static List<char> Symmetry = new List<char>();
        static List<char> Dissymmetry = new List<char>();
        static Dictionary<char, char> SymmetryPair = new Dictionary<char, char>();

        static public string DrawUpFrame(int length)//without sidePoint
        {
            return DrawLine(length, 202, 206, 188);
        }

        static public string DrawDownFrame(int length)//without sidePoint
        {
            return DrawLine(length, 201, 206, 189);
        }

        static public string DrawUnicode(int code)
        {
            return CodePage437UnicodeMap[code];
        }

        static public string DrawLine(int length, int left, int mid, int right)//without sidePoint
        {
            string txt = CodePage437UnicodeMap[left];
            for (int i = 0; i < length; i++)
            {
                txt += CodePage437UnicodeMap[mid];
            }
            txt += CodePage437UnicodeMap[right] + "\n";
            return txt;
        }

        static public string DrawASCIIArtList()
        {
            string txt = "";
            for(int i = 0; i < 255; i++)
            {
                txt += CodePage437UnicodeMap[i];
                if(i%16 == 15)
                {
                    txt += "\n";
                }
            }
            Console.WriteLine(txt.Length);
            Console.WriteLine(txt);
            return txt;
        }

        //public static void OutputByEncoding(Encoding srcEncoding, Encoding dstEncoding, string srcStr)
        //{
        //    byte[] srcBytes = srcEncoding.GetBytes(srcStr);
        //    Console.WriteLine("Encoding.GetBytes: {0}", BitConverter.ToString(srcBytes));
        //    byte[] bytes = Encoding.Convert(srcEncoding, dstEncoding, srcBytes);
        //    Console.WriteLine("Encoding.GetBytes: {0}", BitConverter.ToString(bytes));
        //    string result = dstEncoding.GetString(bytes);
        //    Console.WriteLine("Encoding.GetString: {0}", result);
        //}


        //public static void PrintCodePage()
        //{
        //    EncodingInfo[] info = Encoding.GetEncodings();
        //    Console.Write("编码名称" + "\t" + "编码代码页标识符" + "\t" + "编码说明" + "\n");
        //    for (int i = 0; i < info.Length; i++)
        //    {
        //        Console.Write(info[i].Name + "\t\t" + info[i].CodePage + "\t\t\t" + info[i].DisplayName + "\n");
        //    }
        //}

        public static void Init()
        {
            if (!isInit)
            {
                //Console.OutputEncoding = Encoding.Unicode;
                CodePage437UnicodeMap = new List<string>(CodePage437UnicodeMapString.Split(','));
                LoadASCIISet("assets/ASCIIPart0.txt");
                LoadASCIISet("assets/ASCIIPart1.txt");
                LoadASCIISet("assets/ASCIIPart2.txt");
                isInit = true;
            }
        }

        public static void LoadASCIISet(string fileName)
        {
            var dats = File.ReadAllLines(fileName);
            if (dats.Length >= 1)
            {
                var cs = dats[0].Split(',');
                foreach(var c in cs)
                {
                    int v = int.Parse(c);
                    Symmetry.Add((char)v);
                }
            }
            if (dats.Length >= 2)
            {
                var cs = dats[1].Split(',');
                foreach (var c in cs)
                {
                    int v = int.Parse(c);
                    Dissymmetry.Add((char)v);
                }
            }

            if (dats.Length >= 3)
            {
                var cs = dats[2].Split(',');
                foreach (var c in cs)
                {
                    var pair = c.Split(':');
                    var key = int.Parse(pair[0]);
                    var val = int.Parse(pair[1]);
                    SymmetryPair.Add((char)key, (char)val);
                    SymmetryPair.Add((char)val, (char)key);
                }
            }
        }

        public static string PrintSet()
        {
            string txt = "";
            txt += "Symmetry:\n";
            foreach(var c in Symmetry)
            {
                txt += c;
            }
            txt += "\n";
            txt += "Dissymmetry:\n";
            foreach (var c in Dissymmetry)
            {
                txt += c;
            }
            txt += "\n";
            txt += "SymmetryPair:\n";
            foreach (var pair in SymmetryPair)
            {
                txt += pair.Key;
                txt += pair.Value;
            }
            txt += "\n";
            return txt;
        }

        //15*8
        public static ASCIIPage Monster()
        {
            ASCIIPage page = new ASCIIPage(15, 8);
            page.Draw(4, 3, Arm());
            page.Draw(5, 5, Leg());
            page.Draw(5, 0, Head());
            page.Draw(5, 3, Body());
            Console.WriteLine(page.Print());
            //SaveString("d:/asciiArt", page.Print());
            return page;
        }


/*    05%20%10%
 *    05%99%80%
 *    00%50%99%
 * 
 */
        public static ASCIIPage Head()//5*3 Size
        {
            List<char> bl = new List<char>();
            bl.AddRange(SymmetryPair.Keys);
            bl.AddRange(SymmetryPair.Values);
            char c1 = RandomSelect<char>.Select(bl);
            char c2 = RandomSelect<char>.Select(Symmetry);
            char m1 = RandomSelect<char>.Select(Symmetry);
            char m2 = RandomSelect<char>.Select(Symmetry);

            ASCIIPage page = new ASCIIPage(5, 3);
            var color = ASCIIColor.RndColor();
            var lcolor = color.Lighter();
            var dcolor = color.Darker();
            page.Set(1, 0, c1); page.SetColor(1, 0, lcolor); page.Set(3, 0, SymmetryPair[c1]); page.SetColor(3, 0, lcolor);
            page.Set(1, 1, c2); page.SetColor(1, 1, color); page.Set(2, 1, m1); page.SetColor(2, 1, color); page.Set(3, 1,c2); page.SetColor(3, 1, color);
            page.Set(2, 2, m2); page.SetColor(2, 2, dcolor);

            //Console.WriteLine(page.Print());
            return page;
        }

        public static ASCIIPage Body()//5*2 Size
        {
            List<char> bl = new List<char>();
            bl.AddRange(SymmetryPair.Keys);
            bl.AddRange(SymmetryPair.Values);
            char c1 = RandomSelect<char>.Select(bl);
            char c2 = RandomSelect<char>.Select(bl);
            char c3 = RandomSelect<char>.Select(bl);
            char m1 = RandomSelect<char>.Select(Symmetry);
            char m2 = RandomSelect<char>.Select(Symmetry);

            ASCIIPage page = new ASCIIPage(5, 2);
            page.Set(0, 0, c1); page.Set(1, 0, c2); page.Set(2, 0, m1); page.Set(3, 0, SymmetryPair[c2]); page.Set(4, 0, SymmetryPair[c1]);
            page.Set(1, 1, c3); page.Set(2, 1, m2); page.Set(3, 1, SymmetryPair[c3]);

            //Console.WriteLine(page.Print());
            return page;
        }

        public static ASCIIPage Arm()//7*3 Size
        {
            List<char> bl = new List<char>();
            bl.AddRange(SymmetryPair.Keys);
            bl.AddRange(SymmetryPair.Values);
            char c1 = RandomSelect<char>.Select(bl);
            char c2 = RandomSelect<char>.Select(bl);
            char c3 = RandomSelect<char>.Select(bl);

            ASCIIPage page = new ASCIIPage(7, 3);
            page.Set(0, 0, c1); page.Set(6, 0, SymmetryPair[c1]);
            page.Set(0, 1, c2); page.Set(6, 1, SymmetryPair[c2]);
            page.Set(0, 2, c3); page.Set(6, 2, SymmetryPair[c3]);

            //Console.WriteLine(page.Print());
            return page;
        }

        public static ASCIIPage Leg()//5*3 Size
        {
            List<char> bl = new List<char>();
            bl.AddRange(SymmetryPair.Keys);
            bl.AddRange(SymmetryPair.Values);
            char c1 = RandomSelect<char>.Select(bl);
            char c2 = RandomSelect<char>.Select(bl);
            char c3 = RandomSelect<char>.Select(bl);
            char c4 = RandomSelect<char>.Select(bl);

            ASCIIPage page = new ASCIIPage(5, 3);
            page.Set(1, 0, c1); page.Set(3, 0, SymmetryPair[c1]);
            page.Set(1, 1, c2); page.Set(3, 1, SymmetryPair[c2]);
            page.Set(1, 2, c3); page.Set(3, 2, SymmetryPair[c3]);
            page.Set(0, 2, c4); page.Set(4, 2, SymmetryPair[c4]);

            //Console.WriteLine(page.Print());
            return page;
        }

        public static void SaveString(string file,string str)
        {
            //FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            File.WriteAllText(file, str,Encoding.ASCII);
            
            
        }
    }
}
