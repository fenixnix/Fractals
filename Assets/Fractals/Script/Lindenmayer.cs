using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fractals {
    public class Lindenmayer : MonoBehaviour {
        public ChainLang chainLang;
        public string current;

        public void Init() {
            current = chainLang.begin;
        }

        [ContextMenu("StepTest")]
        public void Step() {
            List<string> tmp = new List<string>();
            foreach(var c in current) {
                tmp.Add(chainLang.Switch(c));
            }
            current = string.Join("", tmp);
        }

        private void Start() {
            Init();
        }
    }
}
