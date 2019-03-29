using System.Collections.Generic;
using UnityEngine;

namespace Fractals {
    public class FractalFace : MonoBehaviour {
        public int n = 5;
        public List<Triangle> triangles = new List<Triangle>();

        [ContextMenu("Sier_Gasket")]
        public void Test_Sier_Gasket() {
            triangles.Clear();
            Generate_Sier_Gasket(n, new Triangle(Vector2.zero, new Vector2(.5f, 1), new Vector2(-.5f, 1)));
        }
        public void Generate_Sier_Gasket(int n, Triangle tri) {
            if(n == 0) {
                triangles.Add(tri);
                return;
            }
            var next = n - 1;
            Generate_Sier_Gasket(next,tri.SubTriangleA());
            Generate_Sier_Gasket(next, tri.SubTriangleB());
            Generate_Sier_Gasket(next, tri.SubTriangleC());
        }

        List<GeoObj> objs = new List<GeoObj>();
        [ContextMenu("Sier_Carpet")]
        public void Test_Sier_Carpet() {
            objs.Clear();
            Generate_Sier_Carpet(n, new GeoObj(Vector2.zero, 1));
        }

        public void Generate_Sier_Carpet(int n, GeoObj obj) {
            if(n == 0) {
                objs.Add(obj);
                return;
            }
            var next = n - 1;
            var nextSize = obj.size / 3;
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(-nextSize, -nextSize), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(0, -nextSize), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(nextSize, -nextSize), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(-nextSize, 0), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(nextSize, 0), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(-nextSize, nextSize), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(0, nextSize), nextSize));
            Generate_Sier_Carpet(next, new GeoObj(obj.center - new Vector3(nextSize, nextSize), nextSize));
        }

        public bool draw = false;
        public bool trigger = false;
        private void OnDrawGizmos() {
            if(!draw) return;

            if(trigger) {
                trigger = false;
                Test_Sier_Gasket();
            }

            foreach(var t in triangles) {
                Gizmos.DrawLine(t.a, t.b);
                Gizmos.DrawLine(t.b, t.c);
                Gizmos.DrawLine(t.c, t.a);
            }

            foreach(var obj in objs) {
                Gizmos.DrawWireCube(obj.center, new Vector3(obj.size, obj.size));
            }
        }
    }
}
