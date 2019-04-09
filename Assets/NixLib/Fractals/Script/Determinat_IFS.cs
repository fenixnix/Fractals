using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Determinat_IFS : MonoBehaviour {
    public int imgSize;
    public IFSCode ifsCode;

    bool[,] tmp;
    bool[,] img;

    public void Init() {
        tmp = new bool[imgSize, imgSize];
        img = new bool[imgSize, imgSize];
        for(int i = 0; i < imgSize; i++) {
            for(int j = 0; j < imgSize; j++) {
                tmp[i, j] = true;
                img[i, j] = false;
            }
        }
        running = true;
    }

    private void Start() {
        Init();
    }

    public void RunStep() {
        Pixel.ClearG();
        var tmpAffine = ifsCode.Get();
        for(int i = 0; i < imgSize; i++) {
            for(int j = 0; j < imgSize; j++) {
                if(tmp[i, j]) {
                    Pixel.Draw(new Vector3(j, i), Color.white);
                    var point = new Vector3(j, i);
                    var newPoint = AffineTransform(point, tmpAffine);
                    //Debug.Log(point + "*" + newPoint);
                    img[(int)newPoint.x, (int)newPoint.y] = true;
                }
            }
        }

        for(int i = 0; i < imgSize; i++) {
            for(int j = 0; j < imgSize; j++) {
                tmp[i, j] = img[i, j];
                img[i, j] = false;
            }
        }
    }

    public Vector3 AffineTransform(Vector3 pointNew, AffineMatix trans) {
        //Debug.Log("Mtx:\n" + trans.Matrix);
        return trans.Matrix.MultiplyPoint3x4(pointNew);
    }

    bool running = false;
    private void Update() {
        //if(!running) return;
        if(Input.anyKeyDown) {
            Debug.Log("Step");
            RunStep();

            Debug.Log(ifsCode[2].Matrix.MultiplyPoint3x4(new Vector3(3, 4)));
        }
    }
}
