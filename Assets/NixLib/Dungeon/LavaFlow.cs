using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    public Material mat; 

    float x = 0;
    public float flowSpd = 0.5f;

    void Update()
    {
        x += Time.deltaTime* flowSpd;
        mat.SetTextureOffset("_MainTex", new Vector2(Mathf.PerlinNoise(x,0), Mathf.PerlinNoise(0, x)));
    }
}
