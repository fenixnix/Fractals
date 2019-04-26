using HullDelaunayVoronoi.Voronoi;
using HullDelaunayVoronoi.Primitives;
using NixLib;
using System.Collections.Generic;
using UnityEngine;
using HullDelaunayVoronoi.Delaunay;
using Nixlib.WorldMap;
using System.Linq;

public class WorldGenerator : MonoBehaviour {
    public int PointCount = 512;
    public Rect size;
    List<Vector3> points = new List<Vector3>();
    List<float> latitude = new List<float>();
    VoronoiMesh2 voronoi;

    [ContextMenu("Start")]
    public void Generate() {
        points.Clear();
        for(int i = 0; i < PointCount; i++) {
            points.Add(new Vector2(Random.Range(size.xMin, size.xMax), Random.Range(size.yMin, size.yMax)));
        }
        var render = GetComponent<GLRender>();
        render.points.Clear();
        render.points.AddRange(points);
    }

    [ContextMenu("Improve")]
    public void Improve() {
        points = Improve(points);
        var render = GetComponent<GLRender>();
        render.points.Clear();
        render.points.AddRange(points);
        render.lines.Clear();
        foreach(var v in voronoi.Regions) {
            foreach(var e in v.Edges) {
                Vector2 a = new Vector2(e.From.CircumCenter.X, e.From.CircumCenter.Y);
                Vector2 b = new Vector2(e.To.CircumCenter.X, e.To.CircumCenter.Y);
                render.lines.Add(new Geometric.Line(a, b));
            }
        }
    }

    public List<Vector3> Improve(IEnumerable<Vector3> src) {
        voronoi = new VoronoiMesh2();
        List<Vertex2> vertexs = new List<Vertex2>();
        foreach(var v in src) {
            vertexs.Add(new Vertex2(v.x, v.y));
        }
        voronoi.Generate(vertexs);
        Debug.Log("Voronoi:" + voronoi.Cells.Count + " Regions:" + voronoi.Regions.Count);
        List<Vector3> tmp = new List<Vector3>();
        foreach(var v in voronoi.Regions) {
            Vector2 vcenter = new Vector2();

            foreach(var e in v.Cells) {
                vcenter.x += e.CircumCenter.X;
                vcenter.y += e.CircumCenter.Y;
            }
            vcenter = vcenter / v.Cells.Count;
            if(size.Contains(vcenter)) {
                tmp.Add(vcenter);
            }
        }
        return tmp;
    }

    [ContextMenu("EraseBorder")]
    public void EraseBorder() {
        List<VoronoiRegion<Vertex2>> tmp = new List<VoronoiRegion<Vertex2>>();
        foreach(var v in voronoi.Regions) {
            foreach(var e in v.Edges) {
                Vector2 vcenter = new Vector2();

                foreach(var c in v.Cells) {
                    vcenter.x += c.CircumCenter.X;
                    vcenter.y += c.CircumCenter.Y;
                }
                vcenter = vcenter / v.Cells.Count;
                if(!size.Contains(vcenter)) {
                    tmp.Add(v);
                }
            }
        }
        foreach(var t in tmp) {
            voronoi.Regions.Remove(t);
        }

        var render = GetComponent<GLRender>();
        render.points.Clear();
        render.points.AddRange(points);
        render.lines.Clear();
        foreach(var v in voronoi.Regions) {
            foreach(var e in v.Edges) {
                Vector2 a = new Vector2(e.From.CircumCenter.X, e.From.CircumCenter.Y);
                Vector2 b = new Vector2(e.To.CircumCenter.X, e.To.CircumCenter.Y);
                render.lines.Add(new Geometric.Line(a, b));
            }
        }
    }

    [ContextMenu("FindNeighbor")]
    public void FindNeighbor() {
        List<VoronoiRegion<Vertex2>> tmp = new List<VoronoiRegion<Vertex2>>();
        int index = 0;
        foreach(var v in voronoi.Regions) {
            int cnt = 0;
            foreach(var e in v.Edges) {
                var oneEdgeCnt = 0;
                foreach(var vi in voronoi.Regions) {
                    if(vi == v) continue;
                    if(vi.Edges.Contains(e)) {
                        cnt++;
                        oneEdgeCnt++;
                    }
                }
                //Debug.Log("OneEdge:" + oneEdgeCnt);
            }
            if(v.Edges.Count != cnt / 2) {
                tmp.Add(v);
            }
            Debug.Log(index + "Region:" + v.Edges.Count + " Find!" + cnt);

            index++;
        }
        foreach(var t in tmp) {
            voronoi.Regions.Remove(t);
        }

        var render = GetComponent<GLRender>();
        render.points.Clear();
        render.points.AddRange(points);
        render.lines.Clear();
        foreach(var v in voronoi.Regions) {
            foreach(var e in v.Edges) {
                Vector2 a = new Vector2(e.From.CircumCenter.X, e.From.CircumCenter.Y);
                Vector2 b = new Vector2(e.To.CircumCenter.X, e.To.CircumCenter.Y);
                render.lines.Add(new Geometric.Line(a, b));
            }
        }
    }

    public float scale = 1;
    public Vector2 offset;
    public float seaLevel = 0.3f;
    public Vector3 noisePosition = Vector3.one;

    

    public void FillSea() {
        var render = GetComponent<GLRender>();
        render.lines.Clear();

        for(int i = 0; i < voronoi.Regions.Count; i++) {
            if(latitude[i] < seaLevel) {

            }
        }
    }

    public PolygonMesh worldMesh;
    public PolygonWorld polyWorld = new PolygonWorld();
    [ContextMenu("Draw Poly World")]
    public void DrawPolyWorld() {
        Noise.NoiseAtom na = new Noise.NoiseAtom();
        na.Init(4, 0.5f, 2f,scale,offset);
        polyWorld.InitWorld(voronoi,na);
        worldMesh.Init();
        foreach(var r in polyWorld.regions) {
            //var ll = na.NoiseValue((r.position.x + noisePosition.x) / scale, (r.position.y + noisePosition.y) / scale);
            List<Vector3> p = new List<Vector3>();
            foreach(var v in r.Vertexs) {
                //var l = na.NoiseValue((v.x + noisePosition.x) / scale, (v.y + noisePosition.y) / scale);
                p.Add(v);
            }
            if(r.position.z < 0.5f) {
                worldMesh.AddPolygon(p.ToArray(), Color.blue);
            }
            else {
                worldMesh.AddPolygon(p.ToArray(), Color.green);
            }
        }
    }
}
