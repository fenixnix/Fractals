using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainModify))]
public class TerrainGenerateInspector : Editor {
    public TerrainModify terrain;
    private void OnEnable() {
        terrain = (TerrainModify)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Random Height")) {
            terrain.SetPerlinHeight();
        }
        if(GUILayout.Button("Auto Fill Layer")) {
            terrain.SetLayer();
        }
        if(GUILayout.Button("Generate")) {
            terrain.Generate();
        }
    }
}