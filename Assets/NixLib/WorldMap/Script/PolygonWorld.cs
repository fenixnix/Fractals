using HullDelaunayVoronoi.Primitives;
using HullDelaunayVoronoi.Voronoi;
using System.Collections.Generic;
using UnityEngine;

namespace NixLib.WorldMap {
    public class PolygonWorld {
        public Rect roi = new Rect(-1, -1, 2, 2);
        public List<RegionPolygon> regions = new List<RegionPolygon>();
        public List<PolygonEdge> edges = new List<PolygonEdge>();
        public List<Vector3> vertexs = new List<Vector3>();

        public void InitWorld(VoronoiMesh2 voronoi) {
            regions.Clear();
            edges.Clear();
            vertexs.Clear();

            foreach(var r in voronoi.Regions) {
                AddRegion(r);
            }
        }

        public void AddRegion(VoronoiRegion<Vertex2> region) {
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

            //添加多边形边
            foreach(var e in region.Edges) {
                var pA = Contain(new Vector3(e.From.CircumCenter.X, e.From.CircumCenter.Y));
                var pB = Contain(new Vector3(e.To.CircumCenter.X, e.To.CircumCenter.Y));
                var edge = Contain(new PolygonEdge(pA,pB));
                r.Edges.Add(edge);
                if(!edges.Contains(edge)) {
                    edges.Add(edge);
                }
            }
            regions.Add(r);
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

        Vector3 Contain(Vector3 pos) {
            foreach(var p in vertexs) {
                if((pos.x == p.x) && (pos.y == p.y) && (pos.z == p.z)) return p;
            }
            return pos;
        }
    }
}
