using UnityEngine;

[System.Serializable]
public class MapSite {
    public enum Size {Small, Middle, Large }

    public string id = "none";
    public string nameTextChs = "无";
    public string county = "郡";
    public int people = 0;
    public Size size = Size.Small;
    public Vector3 position = new Vector3();

    public string Print() {
        string txt = id + ":";
        txt += nameTextChs + " " + county;
        return txt;
    }
}
