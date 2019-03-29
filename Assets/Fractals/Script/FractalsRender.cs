using Fractals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FractalsRender : MonoBehaviour
{
    public JuliaSet fsb = new JuliaSet();
    public Sprite sprite;
    public int N = 100;
    public int pixelSize = 512;

    public Vector3 center;
    [Range(0, 40)]
    public float zoomSize = 4f;

    public Vector3 JuliaC;

    private void Start() {
        Init();
    }

    [ContextMenu("Init")]
    public void Init() {
        texture = new Texture2D(pixelSize, pixelSize);
        pixels = new Color[pixelSize * pixelSize];
        sprite = Sprite.Create(texture, new Rect(0, 0, pixelSize, pixelSize), new Vector2(.5f, .5f));
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public TransferFunctionEnum jsbTraFun;

    [ContextMenu("M")]
    public void DrawM() {
        fsb.GenerateMandelbrot(N, center, zoomSize, pixelSize);
        SetPixels();
    }

    [ContextMenu("Julia")]
    public void DrawJulia() {
        fsb.tf = jsbTraFun;
        fsb.c = JuliaC;
        fsb.GenerateJulia(N, center, zoomSize ,pixelSize);
        SetPixels();
    }

    Color[] pixels;
    Texture2D texture;
    public Color startColor = Color.green;
    public Color EndColor = Color.blue;
    public void SetPixels() {
        int index = 0;
        while(index < fsb.pixels.Length) {
            //var color = Color.Lerp(startColor, EndColor, fsb.pixels[index]);
            var color = ColorCurve(1- fsb.pixels[index]);
            pixels[index] = color;
            index++;
        }
        texture.SetPixels(pixels);
        texture.Apply();
    }

    Color ColorCurve(float level) {
        return new Color(
            (Mathf.Cos(level * Mathf.PI) + 1) / 2,
            (Mathf.Cos(level * Mathf.PI + Mathf.PI * 0.33f) + 1) / 2,
            (Mathf.Cos(level * Mathf.PI + Mathf.PI * 0.66f) + 1) / 2
           // ,(Mathf.Cos(level * Mathf.PI + Mathf.PI * 0.75f + 1)) / 2
            );
    }

    public bool trigger = false;
    private void OnDrawGizmos() {
        if(trigger) 
            {
            //N++;
            DrawJulia();
            trigger = false;
        }
    }

    private void Update() {
        //DrawM();
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        if(wheel > 0) {
            zoomSize *= 0.9f;
        }
        if(wheel < 0) {
            zoomSize *= 1.1f;
        }
    }

    public void OnClick(BaseEventData data) {
        Debug.Log("Evt");
        PointerEventData pData = (PointerEventData)data;
        //if(pData.button != PointerEventData.InputButton.Right) return;
        var pos = pData.pointerCurrentRaycast.worldPosition;
        Debug.Log("click:" + pos);
        var graphicSize = (float)pixelSize / sprite.pixelsPerUnit;
        var newPos = PointTransfer(pos*zoomSize/graphicSize);
        center = newPos;
        DrawM();
    }

    Vector3 PointTransfer(Vector3 point) {
        var offset = new Vector3(point.x ,point.y);
        return center + offset;
    }
}
