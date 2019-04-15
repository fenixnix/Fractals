using System;
using System.Collections.Generic;

namespace Nixlib.CellularAutomata
{
    public class CAClassicRules
    {
        const string classicRules =
            "2x2:s125|b36:有許多種擺動體與固定結構，但是幾乎沒有滑翔機;" +
            "34Life:s34|b34:有許多種擺動體，較少固定結構，群落有擴展的現象;" +
            "Amoeba:s1358|b357:密度高的群落會形成類似變形蟲的圖樣，隨機的變形;" +
            "Assimilation:s4567|b345:密度高的群落會容易形成穩定的菱形或矩形圖樣;" +
            "Coagulations:s235678|b378:群落有擴展的現象，而且擴展過程中內部會形成凝結物;" +
            "Conway's Life:s23|b3:最早被研究，也最有名的生命遊戲規則;" +
            "Coral:s45678|b3:密度高的群落會緩慢的成長，並形成類似珊瑚礁的組織;" +
            "Flakes:s012345678|b3:圓形的群落會擴展成美麗的雪花結構，並且持續成長;" +
            "HighLife:s23|b36:特定的結構會形成令人驚訝的（節狀）自我複製現象;" +
            "Logic:|b2:每次疊代活細胞都會死亡，並且會產生平移的滑翔機;" +
            "Maze:s12345|b3:群落在擴展的過程中，內部會形成類似迷宮的圖樣;" +
            "Mazectric:s1234|b3:類似上一個規則，但是更容易產生線性的迷宮圖樣;" +
            "Replicator:s1357|b1357:這是一個非常值得注意的規則，每經過 32 次疊代，初始圖樣都會被複製 8 次，這個規則具有明顯的碎形特徵;" +
            "Serviettes:|b234:對稱形狀的群落會形成美麗的磁磚花紋的圖樣;" +
            "Stains:s235678|b3678:群落會向外擴展，但是擴展到一定程度就不再擴展;" +
            "WalledCities:s2345|b45678:對於特定密度的隨機群落，其內部會形成聯結組織;" +
            "Banners:s2367|b3457|g3:對於特定密度的隨機群落，會形成類似鳥類拍翅的圖樣;" +
            "BelZhab:s23|b23|g6:隨機的群落會形成類似 Zhabotinsky 化學反應的圖樣;" +
            "BelZhab Sediment:s145678|b23|g6:類似上一個規則，但是會在角落產生沉澱物;" +
            "Bombers:s345|b24|g23:隨機的群落會形成戰火蔓延的氣勢，一發不可收拾;" +
            "Brian's Brain:|b2|g1:與 Conway's Life Rule 齊名的生命遊戲規則;" +
            "Brain 6:s6|b246|g1:Brain's Brain 的另一種變化，同樣常常被研究;" +
            "Burst:s0235678|b3468|g7:密度高的群落會形成爆裂的火花，同時產生點點餘燼;" +
            "Caterpillars:s124567|b378|g2:隨機的群落會形成類似蠕動的毛毛蟲，向前持續延伸;" +
            "Chenille:s05678|b24567|g4:隨機的群落會容易形成塊狀而直線前進的結構物;" +
            "Cooties:s23|b2|g6:隨機的群落會形成活潑的圖樣，像是一群蠕動的蝨子;" +
            "Ebb&Flow:s012478|b36|g16:形成如花開的圖樣，在結構內部有豐富的動態變化;" +
            "Faders:s2|b2|g23:對於特定密度的隨機群落，會形成火花散開的景象;" +
            "Fireworks:s2|b13|g19:任何初始狀態（例如幾個小點）都能夠產生壯觀的煙火;" +
            "Flaming Starbow:s347|b23|g6:任何初始狀態都能夠形成延伸的矩形螺旋結構;" +
            "Frogs:s12|b34|g1:隨機的群落會形成類似青蛙跳躍的滑翔機結構;" +
            "Frozen Spirals:s356|b23|g4:任何初始狀態都會形成延伸的單一中心的矩形螺旋結構;" +
            "Glisserati:s035678|b245678|g5:密度高的群落會形成美麗的萬花筒的圖樣;" +
            "Lava:s12345|b45678|g6:從區域的「角」落逐漸蔓延，像是熔岩般填滿整個區域;" +
            "Lines:s012345|b458|g1:隨機的群落會被自我組織成線性分布的結構;" +
            "Nova:s45678|b2478|g23:密度高的群落會容易形成持續前進的條狀結構;" +
            "Prairie on fire:s345|b34|g4:隨機的群落會產生像是四竄而無規則的火焰;" +
            "Rake:s3467|b2678|g4:容易形成前進的塊狀結構，但是往往在碰撞之後消失;" +
            "RailRoads:s345|b2|g4:特定的結構可以模擬一列列火車在鐵軌上前進的圖樣;" +
            "SediMental:s45678|b25678|g2:對於特定密度的隨機群落，會形成穩定的塊狀結構;" +
            "Snake:s03467|b25|g4:特定的結構可以形成類似蛇行的移動模式;" +
            "SoftFreeze:s13458|b38|g4:隨機的群落會形成像是液態結構的四竄現象;" +
            "Spirals:s2|b234|g3:隨機的群落會形成漂亮而穩定的螺旋結構;" +
            "Sticks:s3456|b2|g4:容易形成多種型態的棍狀（延伸）結構，與滑翔機;" +
            "Swirl:s23|b34|g6:對於特定密度的隨機群落，會形成持續打轉的螺旋體;" +
            "Worms:s3467|b25|g4:對於特定密度的隨機群落，容易形成許多小蟲亂竄;" +
            "Xtasy:s1456|b2356|g14:隨機的群落會形成不停漲落的矩形的螺旋結構;";

        static bool IsInited = false;
        static Dictionary<string, string> classicRulesMap = new Dictionary<string, string>();
        static Dictionary<string, string> classicDescMap = new Dictionary<string, string>();
        static public Dictionary<string, string> ClassicRulesMap { get{ Init(); return classicRulesMap; } }
        static public Dictionary<string, string> ClassicDescMap { get { Init(); return classicDescMap; } }

        static public void Init()
        {
            if (!IsInited)
            {
                string[] rules = classicRules.Split(';');
                foreach (var s in rules)
                {
                    if (s == "")
                    {
                        continue;
                    }
                    string[] ruleSet = s.Split(':');
                    classicRulesMap.Add(ruleSet[0], ruleSet[1]);
                    classicDescMap.Add(ruleSet[0], ruleSet[2]);
                    Console.Write(ruleSet[0] + "\t" + ruleSet[1] + "\t" + ruleSet[2] + "\n");
                }
                IsInited = true;
            }
        }

        static public string GetClassicRule(string name)
        {
            Init();
            if (ClassicRulesMap.ContainsKey(name))
            {
                return ClassicRulesMap[name];
            }
            else
            {
                return "";
                throw new Exception("no rule!!");
            }
        }
    }
}
