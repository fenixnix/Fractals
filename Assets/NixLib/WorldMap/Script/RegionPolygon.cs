using System.Collections.Generic;
using UnityEngine;

namespace NixLib.WorldMap{
    public class RegionPolygon {
        public enum Biomes {SEA,BEACH,LAKE,ICE_LAKE,GRASSLAND, MARSH, TEMPERATE_DECIDUOUS_FOREST,
            TEMPERATE_RAIN_FOREST, TROPICAL_SEASONAL_FOREST, TROPICAL_RAIN_FOREST, TEMPERATE_DESERT,
            SUBTROPICAL_DESERT,SHRUBLAND, TAIGA, TUNDRA, BARE, SCORCHED,SNOW
        }

        public string NameText = "Unknow Region";
        public Vector3 position = Vector3.one;
        public List<Vector3> Vertexs = new List<Vector3>();
        public List<PolygonEdge> Edges = new List<PolygonEdge>(); 
        public List<RegionPolygon> Neighbors = new List<RegionPolygon>();
    }
}
