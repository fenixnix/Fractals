using UnityEngine;
using System.Collections;

[System.Serializable]
public class AffineMatix{
    public float probability;
    public Quaternion mtx;
    public Vector3 translate;

    public Matrix4x4 Matrix {
        get {
            return new Matrix4x4(
                new Vector4(mtx.x, mtx.z, 0, 0),
                new Vector4(mtx.y, mtx.w, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(translate.x, translate.y, translate.z, 1f)
                );
        }
    }
}

[CreateAssetMenu(fileName ="IFSCode",menuName ="Fractals/IFSCode")]
public class IFSCode : ScriptableObject {
    public AffineMatix[]code;
    public AffineMatix this[int index]=>code[index];
    public AffineMatix Get() {
        return code[Random.Range(0, code.Length)];
    }
    public int Size => code.Length;
}
