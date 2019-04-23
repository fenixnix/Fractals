using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMesh : MonoBehaviour {
    Mesh mesh;
    MeshFilter meshFilter;
    //MeshRenderer render;

    public void Init() {
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        //render = gameObject.AddComponent<MeshRenderer>();
    }

    public void AddPolygon(Vector3[] vertex) {
        List<int> triangle = new List<int>();
        List<Color> colors = new List<Color>();
        for(int i = 0; i < vertex.Length; i++) {
            colors.Add(Color.HSVToRGB(vertex[i].z, 1, 1));
        }

        for(int i = 1; i < vertex.Length; i++) {
            triangle.Add(0);
            
            if(i == vertex.Length - 1) {
                triangle.Add(1);
            }
            else {
                triangle.Add(i + 1);
            }

            triangle.Add(i);
        }
        mesh.vertices = vertex;
        mesh.triangles = triangle.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }

    public void SetupMesh() {
        Vector3[] vertex = {new Vector3(0, 0, 1f),
            new Vector3(0, 1, .2f), new Vector3(1, 0, 0.3f),
        new Vector3(0, -1, 0.6f), new Vector3(-1, 0, .7f)};
        AddPolygon(vertex);
    }

    private void Start() {
        Init();
        SetupMesh();
    }
}
