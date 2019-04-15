using Nixlib.Grid;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Nixlib.CellularAutomata {
    /*
    這個規則是由 Brian Silverman 於 1984 年所提出，它與 Logic Rule 幾乎一模一樣，
    唯一不同的是它定義細胞有三個狀態：死亡、存活與一個幽靈狀態。
    幽靈狀態指的是細胞在不符合存活的條件時，並不會馬上消失，而會以第一級的鬼魂狀態繼續存留著，
    下一次疊代時，便以第二級的鬼魂狀態繼續存留……直到所有的鬼魂狀態都輪完了，
    它才會永遠的消失在畫面中。幽靈狀態介於存活與死亡之間，它並非正常存活，也不算是死亡的細胞，
    而我們通常會以不同的顏色來表示不同級數的幽靈狀態。以 Brain Rule 為例，
    你會看見除了空方格外，還有另外兩種顏色，一種顏色是表示正常存活的細胞，
    而另一種顏色便是（第一級）幽靈了。於是，我們便必須在規則中多加入一條，
    它的規則以規則通用表示法敘述如下：

    　◆ 對於一個存活的細胞：
    　　 任何狀況都無法繼續存活，即 Survivals＝（空集合）
    　◆ 對於一個死亡的細胞（未塗色的方格）：
    　　 當恰有二個存活的鄰近細胞時，則誕生活細胞，即 Births＝2
    　◆ 存在一個幽靈狀態：即 Ghosts＝1

    　當這三項合起來表示時，即 Brain Rule＝Survivals/Births/Ghosts
     　　　　　　　　　　　　　　　　　 ＝S/B/G＝/2/1

    　　下面是大小 50x50 的 Brain Rule 生命遊戲的簡單範例 ， 前三個範例與 Logic Rule 的範例是一樣的 ，但是你會發現它們呈現的結果卻有所不同 ， 除了色彩的表現不一樣之外，Brain Rule 比 Logic Rule 更為容易達到穩定的狀態，畫面也不會那麼紊亂，你可以選擇「Random」的選項來做這樣的比較 。 Brain Rule 與 Life Rule 是生命遊戲中最常被提及的兩個規則，因此這兩個規則都發展出了許多著名的範例。
         */
    class CellularAutomata2D {
        //cave s45678|b678  

        List<int> SurvivalsMap = new List<int>();
        List<int> BirthsMap = new List<int>();
        byte ghost = 0;

        public CellularAutomata2D() {

        }

        public CellularAutomata2D(string rule) {
            SetRule(rule);
        }

        public void SetRule(string rule)//s***b***g*
        {
            string survivalsStr = GetRuleVal(rule, "s");
            SurvivalsMap = new List<int>(GetMap(survivalsStr));

            string birthStr = GetRuleVal(rule, "b");
            BirthsMap = new List<int>(GetMap(birthStr));

            string ghostStr = GetRuleVal(rule, "g");
            if(ghostStr.Length != 0) {
                ghost = byte.Parse(ghostStr);
            }
        }

        IEnumerable<int> GetMap(string rule) {
            foreach(var c in rule) {
                yield return Convert.ToInt32(c) - 48;
            }
            yield break;
        }

        string GetRuleVal(string rule, string para) {
            var match = Regex.Match(rule, para + @"\d*");
            if(match.Success) {
                return match.Value.Substring(1);
            }
            return "";
        }

        static int Live = 255;
        static int Dead = 0;

        public Grid2D<int> RunStep(Grid2D<int> src) {
            Grid2D<int> next = Grid2D<int>.Create(src.Width, src.Height, 0);
            for(int y = 0; y < src.Height; y++) {
                for(int x = 0; x < src.Width; x++) {
                    var liveCnt = src.CountAround(new Vector2Int(x, y), Live);
                    if(src[x, y] == Dead) {
                        if(BirthsMap.Contains(liveCnt)) {
                            next[x, y] = Live;
                        }
                        else {
                            next[x, y] = Dead;
                        }
                        continue;
                    }

                    if(src[x, y] == Live) {
                        if(SurvivalsMap.Contains(liveCnt)) {
                            next[x, y] = Live;
                        }
                        else {
                            next[x, y] = ghost;
                        }
                        continue;
                    }

                    if(src[x, y] > 0) {
                        next[x, y] = src[x, y] - 1;
                    }
                }
            }
            return next;
        }

        public Grid2D<int> Run(Grid2D<int> map, int cnt) {
            var tmp = Grid2D<int>.Create(map);
            for(int i = 0; i < cnt; i++) {
                tmp = RunStep(tmp);
            }
            return tmp;
        }

        public static string SelfTest(string rule = "s2367b3457g3") {
            Grid2D<int> world = Grid2D<int>.Create(40, 40);
            world.Noise(255, 0.3f);
            CellularAutomata2D ca = new CellularAutomata2D(rule);
            string tmp = "";
            for(int i = 0; i < 20; i++) {
                

                for(int y = 0; y < world.Height; y++) {
                    for(int x = 0; x < world.Width; x++) {
                        if(world[x, y] == 255) {
                            tmp += "#";
                        }
                        else {
                            tmp += "_";
                        }
                    }
                    tmp += "\n";
                }
                tmp += "***\n";

                world = ca.RunStep(world);
            }
            return tmp;
        }
    }
}
