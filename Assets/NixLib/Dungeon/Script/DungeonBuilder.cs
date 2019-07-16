using Nixlib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Nixlib.ObjBuilder;

public class DungeonBuilder : MonoBehaviour {
    public float tileSize = 5;
    public Transform dungeonRoot;

    public GameObjectPack floorPack;
    public GameObject cornerWall;
    public GameObject wallPrefab;
    public GameObject doorPrefab;

    //public GameObjectPack wallGroundPrefab;

    public int width = 50;
    public int height = 50;
    void Start() {
        //GenerateFloor();
        DungeonRoom room = new DungeonRoom() {
            position = Vector3Int.zero,
            size = new Vector3Int(3, 4, 2),
            door = new Vector3Int(2, 0, 0)
        };
        AddRoom(room);
        AddCorridor(new Vector3Int(0, -4, 0), 0, 10);
    }

    public GameObject 光源;

    //public GameObject 墙角死人;
    //public GameObject 墙上死人;
    //public GameObject 地上死人;

    public void AddRoom(DungeonRoom room) {
        var roomRoot = ObjBuilder.AddNode("Room", dungeonRoot, room.position);

        var floorRoot = ObjBuilder.AddNode("Floor", roomRoot);
        for(int row = -room.size.y; row <= room.size.y; row++) {
            for(int col = -room.size.x; col <= room.size.x; col++) {
                var floor = AddFloor(new Vector3(col, 0, row), floorRoot);
                if((row%3 == 0)&&(col%2==0)) {
                    var light = Instantiate(光源, floor.transform);
                    light.transform.Translate(new Vector3(-tileSize / 2, 0, tileSize / 2));
                }
                else {
                    if(Random.value < 0.3f) {
                        //var dead = Instantiate(地上死人, floor.transform);
                        //dead.transform.Translate(new Vector3(-tileSize / 2, 0.1f, tileSize/ 2));
                        //dead.transform.Rotate(new Vector3(0, Random.Range(-180, 180), 0));
                    }
                }
            }
        }
        //Add Wall
        var wallRoot = ObjBuilder.AddNode("Wall", roomRoot);

        var nRoot = ObjBuilder.AddNode("N", wallRoot);
        nRoot.Translate(new Vector3(-room.size.x * tileSize, 0, -room.size.y * tileSize));
        var sRoot = ObjBuilder.AddNode("S", wallRoot);
        sRoot.Translate(new Vector3(room.size.x * tileSize, 0, (room.size.y + 1) * tileSize));
        AdjFace(sRoot, FaceToward.S);
        var eRoot = ObjBuilder.AddNode("E", wallRoot);
        eRoot.Translate(new Vector3(-room.size.x * tileSize, 0, room.size.y * tileSize));
        AdjFace(eRoot, FaceToward.E);


        var wRoot = ObjBuilder.AddNode("W", wallRoot);
        wRoot.Translate(new Vector3((room.size.x + 1) * tileSize, 0, -room.size.y * tileSize));
        AdjFace(wRoot, FaceToward.W);
        AddWall2(wallPrefab, nRoot, room.size.x * 2 + 1, room.size.z);
        AddWall2(wallPrefab, sRoot, room.size.x * 2 + 1, room.size.z);
        AddWall2(wallPrefab, eRoot, room.size.y * 2 + 1, room.size.z);
        AddWall2(wallPrefab, wRoot, room.size.y * 2 + 1, room.size.z);

        var miscRoot = ObjBuilder.AddNode("Misc", roomRoot);
        for(int row = 1; row < room.size.y - 1; row++) {
            for(int col = 1; col < room.size.x - 1; col++) {
                if(Random.value < 0.1f) {
                    //AddMisc(wallGroundPrefab.get(), new Vector3Int(col, row, 0), floorRoot);
                }
            }
        }
    }

    public Vector3Int AddCorridor(Vector3Int pos, int width = 0, int length = 3, FaceToward face = FaceToward.S) {
        var corridorRoot = ObjBuilder.AddNode("Corridor", dungeonRoot, new Vector3Int((int)(pos.x), (int)(pos.z), (int)(pos.y)));
        var floorRoot = ObjBuilder.AddNode("Floor", corridorRoot);
        for(int row = 0; row < length; row++) {
            for(int col = -width; col <= width; col++) {
                AddFloor(new Vector3(col, 0, row), floorRoot);
            }
        }
        var wallRoot = ObjBuilder.AddNode("Wall", corridorRoot);
        for(int i = 0; i < length; i++) {
            AddWall(wallPrefab, new Vector3Int(-width, 0, i), wallRoot, FaceToward.E);
            AddWall(wallPrefab, new Vector3Int(width + 1, 0, i), wallRoot, FaceToward.W);
        }
        ObjBuilder.AdjFace(corridorRoot, face);
        switch(face) {
            case ObjBuilder.FaceToward.S: return pos + Vector3Int.down * length;
            case ObjBuilder.FaceToward.N: return pos + Vector3Int.up * length;
            case ObjBuilder.FaceToward.E: return pos + Vector3Int.right * length;
            case ObjBuilder.FaceToward.W: return pos + Vector3Int.left * length;
        }
        return pos;
    }

    GameObject AddFloor(Vector3 pos, Transform parent) {
        return ObjBuilder.AddObj(floorPack.get(), pos, parent);
    }

    
    void AddWall(GameObject gObj, Vector3Int pos, Transform parent, FaceToward face = FaceToward.S) {
        var go = Instantiate(gObj, parent);
        go.transform.localPosition = new Vector3(pos.x * tileSize, pos.y * tileSize, pos.z * tileSize);
        AdjFace(go.transform, face);
    }

    //public GameObject torch;
    public void AddWall2(GameObject obj, Transform parent, int length, int height = 2) {
        for(int h = 0; h < height; h++) {
            ObjBuilder.AddObj(cornerWall, new Vector3Int(-1, h, 0), parent);
            for(int i = 0; i < length; i++) {
                if((h == 0) && (i == length / 2)) {
                    var door = ObjBuilder.AddObj(doorPrefab, new Vector3(i, h, 0), parent);
                    door.transform.Translate(new Vector3(-tileSize / 2, 0, 0));
                }
                else {
                    var wall = ObjBuilder.AddObj(obj, new Vector3(i, h, 0), parent);
                    if((h > 0)&&(i%2 == 1)) {
                        //Instantiate(torch, wall.transform);
                    }
                    if(Random.value < 0.3f) {
                        if(h == 0) {
                            //Instantiate(墙角死人, wall.transform).transform.Translate(2.5f, 0.1f, 0.5f); ;
                        }
                        else {
                            //Instantiate(墙上死人, wall.transform).transform.Translate(2.5f, 1.5f, 0.5f);
                        }
                    }
                }
            }
        }
    }

    void AddMisc(GameObject gObj, Vector3Int pos, Transform parent, FaceToward face = FaceToward.S) {
        var go = Instantiate(gObj, parent);
        go.transform.localPosition = new Vector3(pos.x * tileSize, pos.z * tileSize, pos.y * tileSize)
            + new Vector3(tileSize / 2, 0, tileSize / 2);
        go.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0,180f), 0));
        AdjFace(go.transform, face);
    }

    
}
