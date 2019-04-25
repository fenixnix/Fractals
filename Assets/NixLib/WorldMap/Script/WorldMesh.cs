using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldMesh : MonoBehaviour {
    Mesh mesh;
    MeshFilter meshFilter;
    //MeshRenderer render;

    List<Vector3> vertexs = new List<Vector3>();
    List<Color> colors = new List<Color>();
    List<int> triangles = new List<int>();

    public void Init() {
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        //render = gameObject.AddComponent<MeshRenderer>();

        vertexs.Clear();
        colors.Clear();
        triangles.Clear();
    }

    public void AddPolygon(Vector3[] vtx) {
        int offset = vertexs.Count;

        Dictionary<float, Vector3> sortTmp = new Dictionary<float, Vector3>();
        for(int i = 1; i < vtx.Length; i++) {
            var angle = Vector3.Angle(Vector3.up, vtx[i] - vtx[0]);
            float dir = (Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, vtx[i] - vtx[0])) < 0 ? -1 : 1);
            angle *= dir;
            sortTmp.Add(angle, vtx[i]);
        }
        var res = from pair in sortTmp orderby pair.Key select pair;
        List<Vector3> tmp = new List<Vector3>();
        tmp.Add(vtx[0]);
        foreach(var r in res) {
            //Debug.Log(r.Key + "*" + r.Value);
            tmp.Add(r.Value);
        }

        Vector3[] vertex = tmp.ToArray();

        for(int i = 0; i < vertex.Length; i++) {
            vertexs.Add(vertex[i]);
            colors.Add(Color.HSVToRGB(vertex[i].z, 1, 1));
        }
        for(int i = 1; i < vertex.Length; i++) {
            triangles.Add(offset);
            triangles.Add(i + offset);
            if(i == vertex.Length - 1) {
                triangles.Add(offset+1);
            }
            else {
                triangles.Add(i + offset+1);
            }
        }

        //foreach(var v in triangles) {
        //    Debug.Log(v);
        //}

        mesh.vertices = vertexs.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    //private void Start() {
    //    Init();
    //    List<Vector3> tmp = new List<Vector3>();
    //    tmp.Add(Vector3.zero);
    //    for(int i = 0; i < 7; i++) {
    //        tmp.Add(Random.insideUnitCircle.normalized);
    //    }
    //    AddPolygon(tmp.ToArray());

    //}

    public bool drawVertex = false;
    private void OnDrawGizmos() {
        if(!drawVertex) return;
        foreach(var v in vertexs) {
            Gizmos.DrawCube(v, Vector3.one * 0.05f);
        }
    }
}
