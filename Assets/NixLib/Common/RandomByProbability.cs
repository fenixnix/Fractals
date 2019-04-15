using System.Collections.Generic;
using UnityEngine;
namespace Nixlib.Common {
    public class RandomByProbability<T>{
        private List<T> objs = new List<T>();
        private List<float> probabilitys = new List<float>();

        public void Clear() {
            objs.Clear();
            probabilitys.Clear();
        }

        public void AddObject(T obj,float rate) {
            objs.Add(obj);
            probabilitys.Add(rate);
        }

        public T Roll() {
            float sumOfProbability = 0;
            foreach(var p in probabilitys) {
                sumOfProbability += p;
            }
            var rollVal = Random.Range(0, sumOfProbability);
            float hitVal = 0;
            for(int i = 0; i < objs.Count; i++) {
                hitVal += probabilitys[i];
                if(rollVal < hitVal) {
                    return objs[i];
                }
            }
            return objs[objs.Count - 1];
        }

        static public string SelfTest() {
            RandomByProbability<int> rbp = new RandomByProbability<int>();
            rbp.AddObject(0, 3.0f);
            rbp.AddObject(1, 2.0f);
            rbp.AddObject(2, .5f);

            int[] Cnt = new int[3];

            for(int i = 0; i < 1000; i++) {
                var res = rbp.Roll();
                Cnt[res]++;
            }
            string text = "Random By Probability:\n";
            for(int i = 0; i < Cnt.Length; i++) {
                text += i + ":" + Cnt[i] +" " +Cnt[i]/10f + "%"+ "\n";
            }
            return text;
        }
    }
}
