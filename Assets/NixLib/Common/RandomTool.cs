using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox
{
    public class RandomTool
    {
        public static List<int> GetRndList(int cnt)
        {
            List<int> ls = new List<int>();
            for(int i = 0; i < cnt; i++)
            {
                ls.Add(i);
            }
            return ls;
        }
    }
}
