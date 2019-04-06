using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox
{
    public class RandomSelectByRate<T>
    {
        static public T Select(Dictionary<T,double> set)
        {
            if(set.Count == 0)
            {
                Console.WriteLine("Null Select");
                return default(T);
            }

            double totleRate = TotleRate(set);
            double rollValue = RandomNum.Value(0.0f, (float)totleRate);

            double accRate = 0;
            foreach(var k in set.Keys)
            {
                accRate += set[k];
                if (rollValue < accRate)
                {
                    return k;
                }
            }

            throw new Exception("random no hit!!!");
            return default(T);
        }

        static double TotleRate(Dictionary<T, double> set)
        {
            double totleRate = 0;
            foreach (var v in set.Values)
            {
                totleRate += v;
            }
            return totleRate;
        }

        static public string SelfTest()
        {
            Dictionary<string, double> testSet = new Dictionary<string, double>();
            testSet.Add("A", 0.2);
            testSet.Add("B", 0.3);
            testSet.Add("C", 0.5);
            string SResult = RandomSelectByRate<string>.Select(testSet);

            int cntA = 0;
            int cntB = 0;
            int cntC = 0;

            for(int i = 0; i < 1000; i++)
            {
                switch (RandomSelectByRate<string>.Select(testSet))
                {
                    case "A": cntA++; break;
                    case "B": cntB++; break;
                    case "C": cntC++; break;
                }
            }

            return "Result:\n" + "A:" + cntA + "\nB:" + cntB + "\nC:" + cntC + "\n";
        }
    }


}
