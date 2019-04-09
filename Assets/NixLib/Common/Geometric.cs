using UnityEngine;

namespace Geometric {
    [System.Serializable]
    public class Line {
        public Line(Line ori) {
            a = ori.a;
            b = ori.b;
        }

        public Line(Line2DInt ori) {
            a = new Vector3(ori.a.x, ori.a.y);
            b = new Vector3(ori.b.x, ori.b.y);
        }

        public Line(Vector3 a, Vector3 b) {
            this.a = a; this.b = b;
        }
        public Vector3 a;
        public Vector3 b;
        public Vector3 MidPoint => (a + b) / 2;
        public float Length => (a - b).magnitude;

        public void StepForward() {
            a += b;
        }

        public void Rotate(Vector3 Eular) {
            b = RotateVector(b, Eular);
        }

        static Vector3 RotateVector(Vector3 src, Vector3 Eular) {
            return Matrix4x4.Rotate(Quaternion.Euler(Eular)).MultiplyPoint3x4(src);
        }
    }

    [System.Serializable]
    public class Line2DInt {
        public Vector2Int a;
        public Vector2Int b;

        public Line2DInt(Vector2Int pre, Vector2Int cur) {
            a = pre;
            b = cur;
        }

        public Vector2Int Dir => b - a;
    }

    public class GeoObj {
        public GeoObj(Vector3 center, float size) {
            this.center = center;
            this.size = size;
        }
        public Vector3 center;
        public float size;
    }

    public class Triangle {
        public Vector3[] points;
        public Vector3 a;
        public Vector3 b;
        public Vector3 c;

        public Triangle(Vector3[] p) {
            points = new Vector3[3];
            for(int i = 0; i < 3; i++) {
                points[i] = p[i];
            }
        }

        public Vector3 this[int index] => points[index];

        public Triangle(Vector3 A, Vector3 B, Vector3 C) {
            a = A;
            b = B;
            c = C;
            points = new Vector3[3];
            points[0] = A; points[1] = B; points[2] = C;
        }

        public Triangle SubTriangleA() {
            return new Triangle(a, (a + b) / 2, (a + c) / 2);
        }

        public Triangle SubTriangleB() {
            return new Triangle((a + b) / 2, b, (b + c) / 2);
        }

        public Triangle SubTriangleC() {
            return new Triangle((a + c) / 2, (b + c) / 2, c);
        }
    }
}
