using UnityEngine;
using Noise;

public class NoiseGUI : MonoBehaviour
{
    public TextureRender render;
    public bool autoUpdate = false;

    public FractalsNoise noise = new FractalsNoise();
    public Vector2Int size = new Vector2Int(512, 512);
    public float scale = 1;
    public int octave = 1;
    public float persistance = 1;
    public float lacunarity = 1;
    public Vector2 offset;

    public enum DrawMode { Gray,Color};
    public DrawMode drawMode = DrawMode.Gray;

    [ContextMenu("Draw")]
    public void Draw() {
        noise.Generate(size, scale,octave,persistance,lacunarity,offset);
        switch(drawMode) {
            case DrawMode.Gray:render.DrawRawDataGrayLevel(noise.rawData);break;
            case DrawMode.Color:render.DrawRawData(noise.rawData);break;
            default: break;
        }
    }
}
