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
        ObjBuilder.AddPlane(floorPrefab, floor, w, h);

        var wall = ObjBuilder.AddNode("wallRoot", transform);

        var Nwall = ObjBuilder.AddNode("wall", wall);
        var gos = ObjBuilder.AddWall(wallPrefab, Nwall, w, 1);

        var Swall = ObjBuilder.AddNode("wall", wall);
        Swall.Translate(new Vector3((w+1) * ObjBuilder.tileSize,0, h * ObjBuilder.tileSize));
        ObjBuilder.AdjFace(Swall, ObjBuilder.FaceToward.S);
        ObjBuilder.AddWall(wallPrefab, Swall, w, 1);

        var Ewall = ObjBuilder.AddNode("wall", wall);
        Ewall.Translate(new Vector3((w +1) * ObjBuilder.tileSize, 0, -ObjBuilder.tileSize));
        ObjBuilder.AdjFace(Ewall, ObjBuilder.FaceToward.W);
        ObjBuilder.AddWall(wallPrefab, Ewall, h, 1);

        var Wwall = ObjBuilder.AddNode("wall", wall);
        Wwall.Translate(ObjBuilder.tileSize, 0, (h) * ObjBuilder.tileSize);
        ObjBuilder.AdjFace(Wwall, ObjBuilder.FaceToward.E);
        ObjBuilder.AddWall(wallPrefab, Wwall, h, 1);
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
