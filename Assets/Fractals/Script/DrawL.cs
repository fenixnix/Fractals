using Fractals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawL : MonoBehaviour {
    Lindenmayer L;
    private void Start() {
        L = GetComponent<Lindenmayer>();
    }

    Vector3 RotateVector(Vector3 src, Vector3 Eular) {
        return Matrix4x4.Rotate(Quaternion.Euler(Eular)).MultiplyPoint3x4(src);
    }

    public void DrawVonKoch(string LChain) {
        Vector3 dir = new Vector3(0.1f, 0, 0);
        Vector3 point = Vector3.zero;
        foreach(var c in LChain) {
            if(c == 'F') {
                Debug.DrawLine(point, point + dir);
                point = point + dir;
            }
            if(c == '+') {
                dir = RotateVector(dir, new Vector3(0, 0, -60));
            }
            if(c == '-') {
                dir = RotateVector(dir, new Vector3(0, 0, 60));
            }
        }
    }

    public void DrawPeano(string LChain) {
        Vector3 dir = new Vector3(0.1f, 0, 0);
        Vector3 point = Vector3.zero;
        foreach(var c in LChain) {
            if(c == 'F') {
                Debug.DrawLine(point, point + dir);
                point = point + dir;
            }
            if(c == '+') {
                dir = RotateVector(dir, new Vector3(0, 0, -60));
            }
            if(c == '-') {
                dir = RotateVector(dir, new Vector3(0, 0, 60));
            }
        }
    }

    private void Update() {
        string chain = L.current;
        DrawVonKoch(chain);
    }
}
