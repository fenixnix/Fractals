using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.CellularAutomata
{
    public class CellularAutomata1D
    {
        byte rule = 0;
        List<bool> ruleMap = new List<bool>();

        public CellularAutomata1D(int rule)
        {
            SetRuleMap(rule);
        }

        public void SetRuleMap(int rule)
        {
            this.rule = (byte)rule;
            this.ruleMap = GetRuleMap(this.rule);
        }

        List<bool> GetRuleMap(byte rule)
        {
            string binaryString = Convert.ToString(rule, 2);
            binaryString = binaryString.PadLeft(8, '0');
            Console.WriteLine("1D Rule "+rule+":" + binaryString);
            List<bool> ruleMap = new List<bool>();
            for(int i = 0; i < binaryString.Length; i++)
            {
                ruleMap.Insert(0, binaryString[i] == '1' ? true : false);
            }
            return ruleMap;
        }

        public string RunStep(string data)
        {
            string dstString = "";
            string inData = "0" + data + "0";
            for(int i = 0; i < data.Length; i++)
            {
                string binaryString = inData.Substring(i, 3);
                int srcVal = Convert.ToInt32(binaryString, 2);
                dstString += (ruleMap[srcVal]==true) ?"1":"0";
            }
            return dstString;
        }

        public string Run(string data, int cnt)
        {
            string res = data;
            for(int i = 0; i < cnt; i++)
            {
                res = RunStep(res);
            }
            return res;
        }

        public static void SelfTest()
        {
            CellularAutomata1D ca = new CellularAutomata1D(30);
            string res = "000000000000100000000000000";
            for(int i = 0; i < 100; i++)
            {
                Console.WriteLine(res);
                res = ca.RunStep(res);
            }
        }
    }
}
