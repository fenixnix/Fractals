using System.Collections.Generic;
using UnityEngine;

namespace Nixlib.WorldMap {
    public class PolygonRegionIndex {
        public enum Biomes {
            SEA, BEACH, LAKE, ICE_LAKE, GRASSLAND, MARSH, TEMPERATE_DECIDUOUS_FOREST,
            TEMPERATE_RAIN_FOREST, TROPICAL_SEASONAL_FOREST, TROPICAL_RAIN_FOREST, TEMPERATE_DESERT,
            SUBTROPICAL_DESERT, SHRUBLAND, TAIGA, TUNDRA, BARE, SCORCHED, SNOW
        }

        public string NameText = "Unknow Region";
        public Vector3 center = Vector3.one;
        public List<int> EdgesIndex = new List<int>();
        public List<PolygonRegionIndex> Neighbors = new List<PolygonRegionIndex>();
    }
}
