using Geometric;
using System.Collections.Generic;
using UnityEngine;

namespace Fractals {
    public class SubDividLine {
        static public List<Vector3> Generate(Line line, int n = 5, float w = .2f) {
            List<Vector3> tmp = new List<Vector3>();
            Step(tmp, line, n, w);
            return tmp;
        }

        static public void Step(List<Vector3> points, Line line, int n = 5, float w = .2f) {
            if(n == 0) {
                points.Add(line.b);
                return;
            }
            var next = n - 1;
            var W = w;
            var tmpPoint = line.MidPoint + Random.insideUnitSphere * w;
            Step(points, new Line(line.a, tmpPoint), next, W / 2);
            Step(points, new Line(tmpPoint, line.b), next, W / 2);
        }
    }
}
