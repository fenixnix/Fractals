using System;
using UnityEngine;

namespace Nixlib.WorldMap {
    public class PolygonEdgeIndex {
        public int A;
        public int B;
        public PolygonEdgeIndex(int a, int b) {
            A = a;
            B = b;
        }

        static public bool operator ==(PolygonEdgeIndex A, PolygonEdgeIndex B) {
            return (A.A == B.A && A.B == B.B) || (A.A == B.B && A.B == B.A);
        }
        static public bool operator !=(PolygonEdgeIndex A, PolygonEdgeIndex B) {
            return !((A.A == B.A && A.B == B.B) || (A.A == B.B && A.B == B.A));
        }
    }
}
