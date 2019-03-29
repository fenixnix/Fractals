using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovTest : MonoBehaviour
{
    public Vector3 mov;
    [ContextMenu("Mov")]
    public void Mov() {
        transform.Translate(mov);
    }

    public Vector3 rot;
    [ContextMenu("Rot")]
    public void Rot() {
        transform.Rotate(rot);
    }
}
