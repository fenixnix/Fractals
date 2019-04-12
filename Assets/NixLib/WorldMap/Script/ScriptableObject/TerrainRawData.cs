using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainRawData", menuName ="Terrain/RawData")]
public class TerrainRawData : ScriptableObject
{
    public string path;
    public RawData[] data;
}
