using Geometric;
using HullDelaunayVoronoi.Primitives;
using System.Collections.Generic;
using UnityEngine;

namespace NixLib {
    public class GLRender: MonoBehaviour {
        private Material lineMaterial;
        public List<Vector2> points = new List<Vector2>();
        public List<Line> lines = new List<Line>();

        private void Start() {
            Init();
        }

        public void Init() {
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
            //points.Clear();
            //lines.Clear();
        }

        private void OnPostRender() {
            GL.PushMatrix();

            GL.LoadIdentity();
            GL.MultMatrix(GetComponent<Camera>().worldToCameraMatrix);
            GL.LoadProjectionMatrix(GetComponent<Camera>().projectionMatrix);

            lineMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Color(Color.red);
            //Draw Lines;
            foreach(var l in lines) {
                GL.Vertex3(l.a.x, l.a.y, 0.0f);
                GL.Vertex3(l.b.x, l.b.y, 0.0f);
            }
            GL.End();
            GL.Begin(GL.QUADS);
            GL.Color(Color.yellow);
            //Draw Points;
            foreach(var p in points) {
                DrawPoint(new Vertex2(p.x, p.y));
            }
            GL.End();
            GL.PopMatrix();
        }

        private void DrawSimplex(Simplex<Vertex3> f) {
            GL.Vertex3(f.Vertices[0].X, f.Vertices[0].Y, f.Vertices[0].Z);
            GL.Vertex3(f.Vertices[1].X, f.Vertices[1].Y, f.Vertices[1].Z);

            GL.Vertex3(f.Vertices[0].X, f.Vertices[0].Y, f.Vertices[0].Z);
            GL.Vertex3(f.Vertices[2].X, f.Vertices[2].Y, f.Vertices[2].Z);

            GL.Vertex3(f.Vertices[0].X, f.Vertices[0].Y, f.Vertices[0].Z);
            GL.Vertex3(f.Vertices[3].X, f.Vertices[3].Y, f.Vertices[3].Z);

            GL.Vertex3(f.Vertices[1].X, f.Vertices[1].Y, f.Vertices[1].Z);
            GL.Vertex3(f.Vertices[2].X, f.Vertices[2].Y, f.Vertices[2].Z);

            GL.Vertex3(f.Vertices[3].X, f.Vertices[3].Y, f.Vertices[3].Z);
            GL.Vertex3(f.Vertices[1].X, f.Vertices[1].Y, f.Vertices[1].Z);

            GL.Vertex3(f.Vertices[3].X, f.Vertices[3].Y, f.Vertices[3].Z);
            GL.Vertex3(f.Vertices[2].X, f.Vertices[2].Y, f.Vertices[2].Z);
        }

        private void DrawPoint(Vertex2 v) {
            float x = v.X;
            float y = v.Y;
            float s = 0.05f;

            GL.Vertex3(x + s, y + s, 0.0f);
            GL.Vertex3(x + s, y - s, 0.0f);
            GL.Vertex3(x - s, y - s, 0.0f);
            GL.Vertex3(x - s, y + s, 0.0f);
        }

        private void DrawCircle(Vertex2 v, float radius, int segments) {
            float ds = Mathf.PI * 2.0f / (float)segments;

            for(float i = -Mathf.PI; i < Mathf.PI; i += ds) {
                float dx0 = Mathf.Cos(i);
                float dy0 = Mathf.Sin(i);

                float x0 = v.X + dx0 * radius;
                float y0 = v.Y + dy0 * radius;

                float dx1 = Mathf.Cos(i + ds);
                float dy1 = Mathf.Sin(i + ds);

                float x1 = v.X + dx1 * radius;
                float y1 = v.Y + dy1 * radius;

                GL.Vertex3(x0, y0, 0.0f);
                GL.Vertex3(x1, y1, 0.0f);
            }
        }
    }
}

