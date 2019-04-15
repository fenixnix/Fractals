using Nixlib.Grid;
using UnityEngine;
using UnityEngine.UI;

public class TextureRender : MonoBehaviour
{
    public Image image;
    public Texture2D texture;
    public Region region;

    public void Draw(Grid2DInt grid,float MaxValue = 1f) {
        texture = new Texture2D(grid.Width, grid.Height);
        texture.filterMode = FilterMode.Point;
        Color[] colors = new Color[grid.Width * grid.Height];
        int index = 0;
        for(int row = 0; row < grid.Height; row++) {
            for(int col = 0; col < grid.Width; col++) {
                float gray = grid[col, row] / MaxValue;
                colors[index] = Color.Lerp(Color.black, Color.white, gray);
                index++;
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0,0,grid.Width,grid.Height), Vector2.zero);
    }

    public void DrawRawDataGrayLevel(float[,] data) {
        texture = new Texture2D(data.GetLength(1), data.GetLength(0));
        texture.filterMode = FilterMode.Point;
        Color[] colors = new Color[data.Length];
        int index = 0;
        for(int row = 0; row < texture.height; row++) {
            for(int col = 0; col < texture.width; col++) {
                var sample = data[row, col];
                colors[index] = Color.Lerp(Color.black,Color.white,sample);
                index++;
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public void DrawRawData(float[,] data) {
        texture = new Texture2D(data.GetLength(1), data.GetLength(0));
        texture.filterMode = FilterMode.Point;
        Color[] colors = new Color[data.Length];
        int index = 0;
        for(int row = 0; row < texture.height; row++) {
            for(int col = 0; col < texture.width; col++) {
                var sample = data[row, col];
                colors[index] = region[sample];
                index++;
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}

[System.Serializable]
public struct RegionType {
    public string name;
    [Range(0,1f)]
    public float height;
    public Color color;
}

[System.Serializable]
public class Region {
    public RegionType[] region;

    public Color this[float val] {
        get {
            for(int i = 0; i < region.Length; i++) {
                if(val <= region[i].height) {
                    return region[i].color;
                }
            }
            return Color.black;
        }
    }

    public int GetIndex(float val) {
        for(int i = 0; i < region.Length; i++) {
            if(val <= region[i].height) {
                return i;
            }
        }
        return region.Length - 1;
    }
}
