using UnityEngine;
using System.Collections;

public class TerrainTree : MonoBehaviour {
    public Terrain t;

    [ContextMenu("Test")]
    public void Test() {
        var tree = new TreeInstance();
        tree.prototypeIndex = 0;
        tree.heightScale = 10;
        tree.widthScale = 10;
        tree.color = Color.green;
        tree.lightmapColor = Color.white;
        tree.position = new Vector3(0, 1, 0);
        t.AddTreeInstance(tree);
    }
}
