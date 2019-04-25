using HullDelaunayVoronoi.Primitives;
using HullDelaunayVoronoi.Voronoi;
using Noise;
using System.Collections.Generic;
using UnityEngine;

namespace Nixlib.WorldMap {
    public class PolygonWorld {
        NoiseAtom na = new NoiseAtom();

        public Rect roi = new Rect(-1, -1, 2, 2);
        public List<Vector3> vertexs = new List<Vector3>();
        public List<PolygonEdgeIndex> edgesIndex = new List<PolygonEdgeIndex>();
        public List<PolygonRegionIndex> regionsIndex = new List<PolygonRegionIndex>();

        public List<PolygonEdge> edges = new List<PolygonEdge>();
        public List<RegionPolygon> regions = new List<RegionPolygon>();
        
        public void InitWorld(VoronoiMesh2 voronoi) {
            regions.Clear();
            edgesIndex.Clear();
            regionsIndex.Clear();
            edges.Clear();
            vertexs.Clear();

            foreach(var r in voronoi.Regions) {
                AddRegion(r);
            }

            CalcVertex();
        }

        public void CalcVertex() {
            for(int i = 0; i < vertexs.Count; i++) {
                for(int j = i; j < vertexs.Count; j++) {
                    if(i != j) {
                        if(Vector3.Distance(vertexs[i], vertexs[j]) < 0.001f) {
                            Debug.Log(i + "*" + j + ":" + vertexs[i] + "*" + vertexs[j]);
                        }
                    }
                }
            }
        }

        public void AddRegion(VoronoiRegion<Vertex2> region) {
            PolygonRegionIndex ri = new PolygonRegionIndex();


            RegionPolygon r = new RegionPolygon();

            //添加多边形顶点，计算多边形中心点
            Vector3 center = Vector3.zero;
            foreach(var c in region.Cells) {
                var p = Contain(new Vector3(c.CircumCenter.X, c.CircumCenter.Y));
                center += p;
                r.Vertexs.Add(p);
                if(!vertexs.Contains(p)) {
                    vertexs.Add(p);
                }
            }

            r.position = center / region.Cells.Count;
            ri.center = center / region.Cells.Count;

            //添加多边形边
            foreach(var e in region.Edges) {
                var pA = Contain(new Vector3(e.From.CircumCenter.X, e.From.CircumCenter.Y));
                var pB = Contain(new Vector3(e.To.CircumCenter.X, e.To.CircumCenter.Y));
                var edge = Contain(new PolygonEdge(pA, pB));
                r.Edges.Add(edge);
                if(!edges.Contains(edge)) {
                    edges.Add(edge);
                }
            }

            foreach(var e in region.Edges) {
                var pA = ContainVertexIndex(new Vector3(e.From.CircumCenter.X, e.From.CircumCenter.Y));
                var pB = ContainVertexIndex(new Vector3(e.To.CircumCenter.X, e.To.CircumCenter.Y));
                var edge = new PolygonEdgeIndex(pA, pB);
                var index = Contain(edge);
                if(index == -1) {
                    edgesIndex.Add(edge);
                    index = edgesIndex.Count-1;
                }
                ri.EdgesIndex.Add(index);
            }

            regions.Add(r);
            regionsIndex.Add(ri);
        }

        public void CalcNeighbor() {
            //连接临近区域
            foreach(var regionExsit in regions) {
                regionExsit.Neighbors.Clear();
                foreach(var e in regionExsit.Edges) {
                    foreach(var de in regions) {
                        if(regionExsit != de) {
                            if(de.Edges.Contains(e)) {
                                regionExsit.Neighbors.Add(de);
                            }
                        }
                    }
                }
            }
        }

        PolygonEdge Contain(PolygonEdge edge) {
            foreach(var e in edges) {
                if(edge == e) return e;
            }
            return edge;
        }

        int Contain(PolygonEdgeIndex edge) {
            for(int i = 0; i < edgesIndex.Count; i++) {
                if(edge == edgesIndex[i]) {
                    return i;
                }
            }
            return -1;
        }

        Vector3 Contain(Vector3 pos) {
            foreach(var p in vertexs) {
                if((pos.x == p.x) && (pos.y == p.y) && (pos.z == p.z)) return p;
            }
            return pos;
        }

        int ContainVertexIndex(Vector3 pos) {
            for(int i = 0; i < vertexs.Count; i++) {
                var cp = vertexs[i];
                if((pos.x == cp.x)&&(pos.y == cp.y) && (pos.z == cp.z)) {
                    return i;
                }
            }
            Debug.LogError("Not find in Vertex!!!");
            return -1;
        }
    }
}
