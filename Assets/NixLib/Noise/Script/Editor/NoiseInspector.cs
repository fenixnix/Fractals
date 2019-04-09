using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoiseGUI))]
public class NoiseInspector : Editor {
    NoiseGUI noise;
    private void OnEnable() {
        noise = (NoiseGUI)target;
    }

    public override void OnInspectorGUI() {
        if(DrawDefaultInspector()) {
            if(noise.autoUpdate) {
                noise.Draw();
            }
        }
        if(GUILayout.Button("Generate")) {
            noise.Draw();
        }
    }
}