using Nixlib;
using Nixlib.Grid;
using UnityEngine;

public class CaveGenerator : MonoBehaviour {
    public GameObjectPack floorPack;
    public GameObjectPack floorMisc;
    public GameObject caveWall;
    public GameObject centerCorner;
    public GameObject light;
    public Grid2DInt grid;
    public int mapSize = 50;
    public float tileSize = 10f;
    public Material material;
    public float lavaRate = .3f;

    public void Start() {
        ObjBuilder.tileSize = tileSize;
        UCS.ClearChild(transform);
        Generator();
    }

    public Vector2 floorOffset = new Vector2(.5f, -.5f);

    [ContextMenu("Generator")]
    public void Generator() {
        grid = new Grid2DInt(mapSize, mapSize);
        grid.Load();

        Grid2D<byte> tmpGrid = Grid2D<byte>.Create(grid.Width, grid.Height, 0);

        //FillFlow ff = new FillFlow();
        //var BorderMap = ff.Fill(grid, Vector2Int.zero);
        //Debug.Log(BorderMap.Print());
        Grid2D<int> src = Grid2D<int>.Create(grid.Width, grid.Height, 255);
        src.data = grid.data;
        Grid2D<int> borderMap = Grid2D<int>.Create(grid.Width, grid.Height, 255);
        //FillFlow.WaterFlow<int>(src, borderMap, Vector2Int.zero,0);
        

        var allfloor = ObjBuilder.AddNode("Floor", transform);
        var allWall = ObjBuilder.AddNode("Wall", transform);
        for(int y = 0; y < grid.Height; y++) {
            for(int x = 0; x < grid.Width; x++) {
                if(grid[x, y] == 255) {
                    var pos = new Vector3(x, 0, y);
                    var floor = ObjBuilder.AddNode("floor", allfloor, pos);
                    //ObjBuilder.AddObject(floorPack.get(), new Vector3(floorOffset.x, 0, floorOffset.y), floor);
                    var f = ObjBuilder.AddObj(floorPack.get(), Vector3.zero, floor);
                    ObjBuilder.AdjFace2(f.transform, (ObjBuilder.FaceToward)Random.Range(0,4));
                    tmpGrid[x, y] = 1;


                    //ToDoAddWall;
                    if(grid.Is(new Vector2Int(x, y) + Vector2Int.up, 0)) {
                        var wall = ObjBuilder.AddObj(caveWall, pos + new Vector3(0, 0, .5f), allWall.transform);
                        wall.transform.Rotate(new Vector3(0, 180, 0));
                        tmpGrid[x, y] = 2;
                    }
                    if(grid.Is(new Vector2Int(x, y) + Vector2Int.down, 0)) {
                        var wall = ObjBuilder.AddObj(caveWall, pos + new Vector3(0, 0, -.5f), allWall.transform);
                        tmpGrid[x, y] = 3;
                    }
                    if(grid.Is(new Vector2Int(x, y) + Vector2Int.left, 0)) {
                        var wall = ObjBuilder.AddObj(caveWall, pos + new Vector3(-.5f, 0, 0), allWall.transform);
                        wall.transform.Rotate(new Vector3(0, 90, 0));
                        tmpGrid[x, y] = 4;
                    }
                    if(grid.Is(new Vector2Int(x, y) + Vector2Int.right, 0)) {
                        var wall = ObjBuilder.AddObj(caveWall, pos + new Vector3(.5f, 0, 0), allWall.transform);
                        wall.transform.Rotate(new Vector3(0, -90, 0));
                        tmpGrid[x, y] = 5;
                    }
                }
                else {
                    if(grid.Is(new Vector2Int(x, y) + Vector2Int.up, 255)
                        && grid.Is(new Vector2Int(x, y) + Vector2Int.down, 255)
                        && grid.Is(new Vector2Int(x, y) + Vector2Int.left, 255)
                        && grid.Is(new Vector2Int(x, y) + Vector2Int.right, 255)) {
                        var floor = ObjBuilder.AddNode("floor", allfloor, new Vector3(x, 0, y));
                        ObjBuilder.AddObj(floorPack.get(), new Vector3(floorOffset.x, 0, floorOffset.y), floor);
                        tmpGrid[x, y] = 6;
                    }
                }
            }
        }
        var renders = allfloor.GetComponentsInChildren<MeshRenderer>();
        foreach(var r in renders) {
            r.gameObject.AddComponent<MeshCollider>().convex = true;
            if(Random.value < lavaRate) {
                r.material = material;
                var l = Instantiate(light, r.transform);
                l.transform.Translate(new Vector3(-tileSize / 2, 0, tileSize / 2));
            }
            r.material.color = new Color(0.3f, 0.3f, 0.3f, 1);
        }

        for(int raw = 0; raw < tmpGrid.Height; raw++) {
            for(int col = 0; col < tmpGrid.Width; col++) {
                var pos = new Vector3(col, 0, raw);
                if(tmpGrid[col,raw] == 6) {
                    ObjBuilder.AddObj(centerCorner, pos, transform);
                }
                if(tmpGrid[col, raw] == 1) {
                    if(Random.value < .6f) {
                        var misc = ObjBuilder.AddObj(floorMisc.get(), pos, transform);
                        misc.transform.Translate(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * tileSize / 2);
                    }
                }
            }
        }
    }
}
