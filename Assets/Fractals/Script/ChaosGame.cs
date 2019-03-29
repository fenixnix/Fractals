using Fractals;
using UnityEngine;

public class ChaosGame : MonoBehaviour {
    public Vector3[] Points;
    public float RndRange = 3;
    Vector3 tmpPoint;

    void Start() {
        tmpPoint = new Vector3(RndVal(), RndVal(), RndVal());
    }

    float RndVal() {
        return Random.Range(-RndRange, RndRange);
    }

    [ContextMenu("Step")]
    public void RunStep() {
        var p = Random.Range(0, 3);
        var tp = Points[Random.Range(0, Points.Length)];
        tmpPoint = (tp + tmpPoint) / 2;
        Pixel.Draw(tmpPoint, Color.white);
    }

    private void Update() {
        RunStep();
    }
}
