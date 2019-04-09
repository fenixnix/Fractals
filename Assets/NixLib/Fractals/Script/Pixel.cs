using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour {
    static Pixel G;
    public GameObject prefab;

    private void Start() {
        G = this;
    }

    public static void Draw(Vector3 p, Color color) {
        var go = Instantiate(G.prefab, G.transform);
        go.transform.position = p;
    }

    public void Clear() {
        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public static void ClearG() {
        G.Clear();
    }
}
