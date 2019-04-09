using Geometric;
using System.Collections.Generic;
using UnityEngine;

namespace Fractals {
    public class FractalLine : MonoBehaviour {

        public int n = 5;
        public float[] turnList;
        float[] Koch01 = { 60, -120, 60 };
        float[] Koch02 = { 85, -170, 85 };
        float[] Koch03 = { 90, -90, -90, -90, -90, 90, 90, 90, -90 };
        float[] Peano_Hilbert = { 90, -90, -90, 0, -90, 90, -90 };

        List<List<Vector3>> pointList = new List<List<Vector3>>();

        [ContextMenu("Generate_Koch")]
        public void Test_Generate_Koch() {
            th = 0;
            pointList.Clear();
            pointList.Add(new List<Vector3>());
            Vector3 tmpPoints = Vector3.zero;
            Generate_Koch(n,ref tmpPoints,Vector3.right, pointList[0]);
        }

        float th = 0;
        public void Generate_Koch(int n, ref Vector3 startPoint,Vector3 dir, List<Vector3> points) {       
            if(n == 0) {
                float arc = th * Mathf.Deg2Rad;
                startPoint += new Vector3(Mathf.Cos(arc), Mathf.Sin(arc));
                points.Add(startPoint);
                return;
            }

            var next = n - 1;
            foreach(var ta in turnList) {
                Generate_Koch(next, ref startPoint, Vector3.zero,points);
                th += ta;
            }
            Generate_Koch(next, ref startPoint, Vector3.zero, points);
        }

        List<Line> lines = new List<Line>();
        [ContextMenu("Cantor")]
        public void TestCantor() {
            lines.Clear();
            Generate_Cantor(n, Vector2.zero, new Vector2(1, 0));
        }

        public void Generate_Cantor(int n, Vector2 a, Vector2 b) {
            lines.Add(new Line(a, b));
            if(n == 0) {
                return;
            }
            var offset = (a - b).magnitude * .1f;
            var offsetVec = new Vector2(0, offset);
            Generate_Cantor(n - 1, a + offsetVec, a + (b - a) / 3 + offsetVec);
            Generate_Cantor(n - 1, b + offsetVec, b + (a - b) / 3 + offsetVec);
        }

        public float Amplify = .2f;
        [ContextMenu("SubDivid")]
        public void Test_SubDivid() {
            pointList.Clear();
            var points = SubDividLine.Generate(new Line(Vector3.zero, Vector3.up), 5,Random.Range(Amplify / 2, Amplify));
            pointList.Add(points);
        }

        [ContextMenu("Cayley")]
        public void Test_Cayley() {
            lines.Clear();
            pointList.Clear();
            Generate_Cayley(n, Vector3.zero, Vector3.up);
        }

        public void Generate_Cayley(int n, Vector3 startPoint, Vector3 vector) {
            if(n == 0) return;
            var endPoint = startPoint + vector;
            var line = new Line(startPoint, endPoint);
            lines.Add(line);
            var points = SubDividLine.Generate(line, n, line.Length * .2f);

            pointList.Add(points);

            var cnt = Random.Range(2, 4);

            var v = RandomRotate(vector);
            var q = Quaternion.Euler(0, 180f / cnt, 0);
            var minLen = .5f;
            var maxLen = .8f;
            for(int i = 0; i < cnt; i++) {
                Generate_Cayley(n - 1, endPoint, v * Random.Range(minLen, maxLen));
                v = q * v;
            }
        }

        public Vector3 RandomRotate(Vector3 src) {
            float angleRate = 45;
            Quaternion q = Quaternion.Euler(Random.Range(-angleRate, angleRate), 0,
                Random.Range(-angleRate, angleRate));
            return q * src;
        }

        public bool draw = false;
        public bool trigger = false;
        private void OnDrawGizmos() {
            if(!draw) return;

            if(trigger) {
                trigger = false;
                Test_SubDivid();
            }

            foreach(var l in lines) {
                Gizmos.DrawLine(l.a, l.b);
            }

            foreach(var points in pointList) {
                Vector3 prePoint = points[0];
                foreach(var p in points) {

                    //Gizmos.color = Random.ColorHSV();
                    //= new Color(Random.Range(0.0f, 1f),
                    //Random.Range(0.0f, 1f), Random.Range(0f, 1f));

                    Gizmos.DrawLine(prePoint, p);
                    prePoint = p;
                }
            }
        }
    }
}
