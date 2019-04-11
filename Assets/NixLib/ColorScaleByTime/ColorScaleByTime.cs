using NixLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScaleByTime : MonoBehaviour
{
    public Image dst;
    Sprite sprite;
    public Texture2D src;
    Texture2D tmpTexture;

    [Range(0.5f,2f)]
    public float timeScale = 1;

    ColorShift colorShift = new ColorShift();
    public Color Src;
    public Color Dst;
    public float shift = 0;

    private void Start() {
        if((dst == null) || (src == null)) return;
        tmpTexture = new Texture2D(src.width, src.height);
        sprite = Sprite.Create(tmpTexture, new Rect(0, 0,src.width,src.height), Vector2.zero);
        dst.sprite = sprite;
        dst.preserveAspect = true;
    }

    void Update()
    {
        if((dst == null) || (src == null)) return;
        //Process();
    }

    [ContextMenu("Process")]
    public void Process() {
        Dst = colorShift.Shift(Src, shift);
        ImageColorShift(src, shift);
    }

    void ImageColorShift(Texture2D src, float shift) {
        Color[] colorMap = new Color[src.width * src.height];
        int index = 0;
        for(int y = 0; y < src.height; y++) {
            for(int x = 0; x < src.width; x++) {
                colorMap[index] = colorShift.Shift(src.GetPixel(x, y), shift);
                index++;
            }
        }
        tmpTexture.SetPixels(colorMap);
        tmpTexture.Apply();
        TextureToFile.Save(tmpTexture);
    }
}

public class ColorShift {
    public Color color;
    public float h;
    public float s;
    public float v;
    public Color Shift(Color src, float val) {
        Color.RGBToHSV(src, out h, out s, out v);
        h += val;
        if(h < 0) { h = 0; return Color.black; }
        if(h > 1) { h = 1; return Color.black; }
        return Color.HSVToRGB(h, s, v);
    }
}

