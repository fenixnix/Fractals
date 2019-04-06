using System;
using System.Collections.Generic;

namespace ToolBox
{
    public class RandomSelect<T>
    {
        static public T Select(List<T> set)
        {
            if(set.Count == 0)
            {
                return default(T);
            }
            int index = RandomNum.Value(0, set.Count);
            //UnityEngine.Debug.Log("Random Select index:" + set.Count + " " + index);
            return set[index];
        }

        static public List<T> Select(List<T> set, int count)
        {
            List<T> res = new List<T>();
            List<T> tmp = new List<T>();
            tmp.AddRange(set);
            for(int i = 0; i < count; i++)
            {
                if (tmp.Count == 0)
                {
                    continue;
                }
                T t = Select(tmp);
                res.Add(t);
                tmp.Remove(t);
            }
            return res;
        }

        static public void SelfTest()
        {
            List<int> testSet = new List<int>();
            for(int i = 0; i < 10; i++)
            {
                testSet.Add(i);
            }

            Console.WriteLine("test Set Val:");
            foreach(int i in testSet)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Random Selected Val:");
            Console.WriteLine(RandomSelect<int>.Select(testSet));

            Console.WriteLine("Random Selected 3 Val:");
            List<int> testRes = RandomSelect<int>.Select(testSet, 3);
            foreach (int i in testRes)
            {
                Console.WriteLine(i);
            }
        }
    }

    public class RandomNum
    {
        static Random rng = new Random(Guid.NewGuid().GetHashCode());

        static public byte Bit8()
        {
            return (byte)Value(0, 255);
        }

        static public int Value(int min,int max)
        {
            //Random rng = new Random(Guid.NewGuid().GetHashCode());
            int v = rng.Next(min,max);
            return v;
        }

        static public int Value(int min, int max, int times)
        {
            int sum = 0;
            for(int i = 0; i < times; i++)
            {
                sum += Value(min, max);
            }
            return sum;
        }

        static public float Value(float min, float max)
        {
            float dif = max - min;
            int difInt = (int)(dif * 1000);
            int resInt = rng.Next(difInt);
            float resFloat = min + (resInt / 1000.0f);
            return resFloat;
        }

        static public bool Roll(float rate)
        {
            float val = Value(0.0f, 1.0f);
            return val < rate;
        }
    }
}
