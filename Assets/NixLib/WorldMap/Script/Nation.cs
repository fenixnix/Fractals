using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct PathOfSite {
    public List<string> sites;
}

[CreateAssetMenu(fileName = "Nation",menuName = "Map/Nation")]
public class Nation : ScriptableObject {
    public string id = "none";
    public Sprite flag;
    public Color textColor = Color.white;
    public List<MapSite> sites = new List<MapSite>();
    public List<PathOfSite> paths = new List<PathOfSite>();

    public MapSite this[string id] {
        get {
            foreach(var s in sites) {
                if(s.id == id) {
                    return s;
                }
            }
            return null;
        }
    }
}