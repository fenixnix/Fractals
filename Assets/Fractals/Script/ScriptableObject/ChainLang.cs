using UnityEngine;
using System.Collections.Generic;

namespace Fractals {

    [System.Serializable]
    public class LRule {
        public char src;
        public string dst;
    }

    [CreateAssetMenu(fileName = "ChainLang", menuName = "Fractals/ChainLang")]
    public class ChainLang : ScriptableObject {
        public char[] symbels;
        public string begin;
        public LRule[] rules;

        Dictionary<char, string> ruleMap = new Dictionary<char, string>();

        public void OnEnable() {
            foreach(var r in rules) {
                ruleMap.Add(r.src, r.dst);
            }
        }

        public string Switch(char c) {
            return (ruleMap.ContainsKey(c)) ? ruleMap[c]: c.ToString();
        }
    }
}
