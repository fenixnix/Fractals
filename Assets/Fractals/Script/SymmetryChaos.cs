using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymmetryChaos : MonoBehaviour
{
    public float a = .25f, b = -.3f,c=.2f,d=.3f;
    public double x = .6f, y = .4f;
    public Vector2Int Amp = new Vector2Int();
    public Vector2Int vt = new Vector2Int();

    public void Generate() {
        for(int i = 0; i < 100; i++) {
            golubit_3(ref x,ref y,a,b,c,d);
        }

        for(int i = 0; i < 100; i++) {
            for(int j = 0; j < 1000; j++) {
                golubit_3(ref x, ref y, a, b, c, d);
            }
        }
        vt.x = (int)(Amp.x * x);
        vt.y = (int)(Amp.y * y);
    }

    void golubit_3(ref double x,ref double y,float a, float b, float c, float d) {
        var xx = x * x;var yy = y * y; var xy = x * y;
        var tmp = a + b * (xx + yy) + c * (xx * x - 3.0f * xy * y);
        x = tmp * x + d * (xx - yy);
        y = tmp * y - 2 * d * xy;
    }

    [ContextMenu("Test")]
    public void SelfTest() {
        //Debug.Log(Nixlib.Common.RandomByProbability<int>.SelfTest());
    }

    Vector3 prePoint = Vector3.zero;
    private void Update() {
        Generate();
        Debug.DrawLine(prePoint, new Vector3((float)x, (float)y));
        Debug.Log(vt);
    }
}
