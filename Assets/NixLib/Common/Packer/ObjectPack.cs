using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPack<T> : ScriptableObject
{
    public enum GetMode { first, loop, random, randomNoReapeat }
    public GetMode mode = GetMode.random;
    public T[] objs;

    int loopIndex = 0;
    int lastIndex = 0;
    T lastOne;

    public T this[int index] => objs[index];

    public T get()
    {
        if(objs.Length == 0)
        {
            return default(T);
        }
        switch (mode)
        {
            case GetMode.first: return objs[0];
            case GetMode.loop: loopIndex++;
                loopIndex = (int)Mathf.Repeat(loopIndex, objs.Length);
                return objs[loopIndex];
            case GetMode.random: return objs[Random.Range(0, objs.Length)];
            case GetMode.randomNoReapeat:
                if (objs.Length < 2)
                {
                    Debug.Log("No match Get Mode");
                    return default(T);
                }
                int currentIndex = Random.Range(1,objs.Length);
                lastOne = objs[currentIndex];
                objs[currentIndex] = objs[0];
                objs[0] = lastOne;
                return lastOne;
            default:
                Debug.Log("No match Get Mode");
                return default(T);

        }
    }
}
