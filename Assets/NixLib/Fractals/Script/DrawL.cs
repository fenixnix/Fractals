using Fractals;
using Geometric;
using System.Collections.Generic;
using UnityEngine;

public class DrawL : MonoBehaviour {
    L_System L;
    LineRenderer lineRender;
    public Vector3 startVector = Vector3.up;
    public Vector3 RotateAngle = new Vector3(0, 0, 60);
    private void Start() {
        L = GetComponent<L_System>();
        lineRender = GetComponent<LineRenderer>();
    }

    Stack<Line> lines = new Stack<Line>();
    List<Vector3> points = new List<Vector3>();
    public void DrawL_System(string LChain) {
        lines.Clear();
        points.Clear();
        Line line = new Line(Vector3.zero, startVector);
        //points.Add(Vector3.zero);
        points.Add(line.a);
        foreach(var c in LChain) {
            switch(c) {
                case 'F':
                case 'X':
                case 'Y':
                    Debug.DrawLine(line.a, line.a + line.b, Color.white, 5);
                    line.StepForward();
                    Debug.Log(line.a);
                    points.Add(line.a);
                    break;
                case '+': line.Rotate(RotateAngle); break;
                case '-': line.Rotate(-RotateAngle); break;
                case '[': lines.Push(new Line(line)); break;
                case ']': line = lines.Pop(); break;
            }
        }
        //lineRender.positionCount = points.Count;
        //lineRender.SetPositions(points.ToArray());
    }

    [ContextMenu("Draw")]
    public void Draw() {
        L.Step();
        string chain = L.current;
        DrawL_System(chain);
    }
}
