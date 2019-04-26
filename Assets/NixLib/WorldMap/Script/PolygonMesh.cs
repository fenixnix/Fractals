using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PolygonMesh : MonoBehaviour {
    Mesh mesh;
    MeshFilter meshFilter;
    //MeshRenderer render;

    List<Vector3> vertexs = new List<Vector3>();
    List<Color> colors = new List<Color>();
    List<int> triangles = new List<int>();

    public void Init() {

        //render = gameObject.AddComponent<MeshRenderer>();

        vertexs.Clear();
        colors.Clear();
        triangles.Clear();
    }

    public void AddPolygonWithCenter(Vector3[] vtx, Vector3 center) {
        int offset = vertexs.Count;
        Dictionary<float, Vector3> sortTmp = new Dictionary<float, Vector3>();
        for(int i = 0; i < vtx.Length; i++) {
            var angle = Vector3.Angle(Vector3.up, vtx[i] - center);
            float dir = (Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, vtx[i] - center)) < 0 ? -1 : 1);
            angle *= dir;
            sortTmp.Add(angle, vtx[i]);
        }
        var res = from pair in sortTmp orderby pair.Key select pair;
        List<Vector3> tmp = new List<Vector3>();
        tmp.Add(center);
        foreach(var r in res) {
            //Debug.Log(r.Key + "*" + r.Value);
            tmp.Add(r.Value);
        }

        Vector3[] vertex = tmp.ToArray();

        for(int i = 0; i < vertex.Length; i++) {
            vertexs.Add(vertex[i]);
            colors.Add(Color.HSVToRGB(vertex[i].z, 1, 1));
        }
        AddTriangles(vertex, offset);
        RefreshMesh();
    }


    public void AddPolygon(Vector3[] vtx) {
        AddPolygonWithCenter(vtx, Center(vtx));
    }

    public void AddPolygon(Vector3[] vtx,Color color) {
        var center = Center(vtx);
        int offset = vertexs.Count;
        Dictionary<float, Vector3> sortTmp = new Dictionary<float, Vector3>();
        for(int i = 0; i < vtx.Length; i++) {
            var angle = Vector3.Angle(Vector3.up, vtx[i] - center);
            float dir = (Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, vtx[i] - center)) < 0 ? -1 : 1);
            angle *= dir;
            sortTmp.Add(angle, vtx[i]);
        }
        var res = from pair in sortTmp orderby pair.Key select pair;
        List<Vector3> tmp = new List<Vector3>();
        tmp.Add(center);
        foreach(var r in res) {
            //Debug.Log(r.Key + "*" + r.Value);
            tmp.Add(r.Value);
        }

        Vector3[] vertex = tmp.ToArray();

        for(int i = 0; i < vertex.Length; i++) {
            vertexs.Add(vertex[i]);
            colors.Add(color);
        }

        AddTriangles(vertex, offset);
        RefreshMesh();
    }

    void AddTriangles(Vector3[] vtxs,int offset) {
        for(int i = 1; i < vtxs.Length; i++) {
            triangles.Add(offset);
            triangles.Add(i + offset);
            if(i == vtxs.Length - 1) {
                triangles.Add(offset + 1);
            }
            else {
                triangles.Add(i + offset + 1);
            }
        }
    }

    void RefreshMesh() {
        mesh.vertices = vertexs.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    Vector3 Center(Vector3[] vtxs) {
        Vector3 sum = Vector3.zero;
        foreach(var v in vtxs) {
            sum += v;
        }
        return sum / vtxs.Length;
    }

    private void Start() {
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;


        //List<Vector3> tmp = new List<Vector3>();
        //tmp.Add(Vector3.zero);
        //for(int i = 0; i < 7; i++) {
        //    tmp.Add(Random.insideUnitCircle.normalized);
        //}
        //AddPolygon(tmp.ToArray());

    }

    public bool drawVertex = false;
    private void OnDrawGizmos() {
        if(!drawVertex) return;
        foreach(var v in vertexs) {
            Gizmos.DrawCube(v, Vector3.one * 0.05f);
        }
    }
}
