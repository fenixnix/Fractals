using System;
using UnityEngine;

namespace NixLib.WorldMap{
    public class PolygonEdge {
        public Vector3 A;
        public Vector3 B;
        public PolygonEdge(Vector3 a, Vector3 b) {
            A = a;
            B = b;
        }

        static public bool operator ==(PolygonEdge A,PolygonEdge B) {
            return (A.A == B.A && A.B == B.B) || (A.A == B.B && A.B == B.A);
        }
        static public bool operator !=(PolygonEdge A, PolygonEdge B) {
            return !((A.A == B.A && A.B == B.B) || (A.A == B.B && A.B == B.A));
        }
    }
}
