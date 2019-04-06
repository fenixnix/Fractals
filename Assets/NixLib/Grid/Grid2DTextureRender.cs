using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid2DTextureRender : MonoBehaviour
{
    public Image image;
    public Texture2D texture;
    public void Draw(Grid2D grid,float MaxValue = 1f) {
        texture = new Texture2D(grid.Width, grid.Height);
        texture.filterMode = FilterMode.Point;
        Color[] colors = new Color[grid.Width * grid.Height];
        int index = 0;
        for(int row = 0; row < grid.Height; row++) {
            for(int col = 0; col < grid.Width; col++) {
                float gray = grid[col, row] / MaxValue;
                colors[index] = new Color(gray,gray,gray);
                index++;
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0,0,grid.Width,grid.Height), Vector2.zero);
    }
}
