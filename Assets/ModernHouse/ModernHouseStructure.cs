using Nixlib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModernHouseStructure : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    int minSize = 3;
    int maxSize = 11;
    public void Generate() {
        ObjBuilder.tileSize = 10;
        var floor = ObjBuilder.AddNode("floorRoot", transform);
        int w = Random.Range(minSize, maxSize);
        int h = Random.Range(minSize, maxSize);
        for(int y = 0; y < h; y++) {
            for(int x = 0; x < w; x++) {
                ObjBuilder.AddObj(floorPrefab, new Vector3(x, 0, y), floor);
            }
        }

        var wall = ObjBuilder.AddNode("wallRoot", transform);
        var Nwall = ObjBuilder.AddNode("wall", wall);
        var gos = ObjBuilder.AddWall(wallPrefab, Nwall, w, 1);

        var Swall = ObjBuilder.AddNode("wall", wall);
        Swall.Translate(new Vector3((w+1) * ObjBuilder.tileSize,0, h * ObjBuilder.tileSize));
        ObjBuilder.AdjFace(Swall, ObjBuilder.FaceToward.S);
        ObjBuilder.AddWall(wallPrefab, Swall, w, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
