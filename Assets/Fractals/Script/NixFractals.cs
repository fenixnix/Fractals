using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fractals {
    public class NixFractals : MonoBehaviour {
        public int n = 3;
        List<GeoObj> squares = new List<GeoObj>();

        private void Start() {
            Test_Sier_Sponge();
            GenerateObj();
        }

        [ContextMenu("Sier_Sponge")]
        public void Test_Sier_Sponge() {
            squares.Clear();
            Generate_Sier_Sponge(n, new GeoObj(Vector3.zero, 1));
        }

        public void Generate_Sier_Sponge(int n, GeoObj square) {
            if(n == 0) {
                squares.Add(square);
                return;
            }
            var next = n - 1;
            var nextSize = square.size / 3;
            for(int z = -1; z < 2; z++) {
                for(int y = -1; y < 2; y++) {
                    for(int x = -1; x < 2; x++) {
                        if(((x == 0) && (y == 0) && (z == 0)) ||
                            ((x == 0) && (y == 0)) ||
                            ((y == 0) && (z == 0)) ||
                            ((x == 0) && (z == 0))
                            ) {
                            continue;
                        }
                        Generate_Sier_Sponge(next,
                            new GeoObj(square.center + new Vector3(x * nextSize, y * nextSize, z * nextSize),
                            nextSize));
                    }
                }
            }
        }

        [ContextMenu("Combine")]
        public void GenerateObj() {
            for(int i = 0; i < transform.childCount; i++) {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            foreach(var s in squares) {
                var go = CreateCube(s);
            }
            var filters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[filters.Length];
            for(int i = 0; i < filters.Length; i++) {
                combine[i].mesh = filters[i].sharedMesh;
                combine[i].transform = filters[i].transform.localToWorldMatrix;
                filters[i].gameObject.SetActive(false);
            }
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);
        }

        public Mesh cube;
        public Material defaultMaterial;
        public GameObject CreateCube(GeoObj s) {
            System.Type[] types = { typeof(MeshFilter), typeof(MeshRenderer) };
            GameObject go = new GameObject("cube", types);
            var filter = go.GetComponent<MeshFilter>();
            filter.mesh = cube;
            var render = go.GetComponent<MeshRenderer>();
            render.material = defaultMaterial;
            go.transform.parent = transform;
            go.transform.localPosition = s.center;
            go.transform.localScale = new Vector3(s.size, s.size, s.size);
            return go;
        }

        public bool draw = true;
        public bool solid = false;
        private void OnDrawGizmos() {
            if(!draw) return;
            foreach(var s in squares) {
                if(solid) {
                    Gizmos.DrawCube(s.center, new Vector3(s.size, s.size, s.size));
                }
                else
                    Gizmos.DrawWireCube(s.center, new Vector3(s.size, s.size, s.size));
            }
        }
    }
}
