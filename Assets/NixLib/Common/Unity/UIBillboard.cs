using UnityEngine;

public class UIBillboard : MonoBehaviour
{
    void Update()
    {
        var cam = Camera.main;
        if (cam == null) return;
        transform.rotation = cam.transform.rotation;
    }
}
