using Fractals;
using System.Collections.Generic;
using UnityEngine;

public class DrawL : MonoBehaviour {
    L_System L;
    public Vector3 RotateAngle = new Vector3(0,0,60);
    private void Start() {
        L = GetComponent<L_System>();
    }

    Stack<Line> points = new Stack<Line>();
    public void DrawVonKochOrPeano(string LChain) {
        points.Clear();
        Line line = new Line(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
        foreach(var c in LChain) {
            switch(c) {
                case 'F':Debug.DrawLine(line.a, line.a + line.b);
                    line.StepForward(); break;
                case '+': line.Rotate(RotateAngle); break;
                case '-': line.Rotate(-RotateAngle); break;
                case '[': points.Push(new Line(line)); break;
                case ']': line = points.Pop(); break;
            }
        }
    }

    [ContextMenu("Draw")]
    public void Draw() {
        string chain = L.current;
        DrawVonKochOrPeano(chain);
    }

    private void Update() {
        Draw();
    }
}
