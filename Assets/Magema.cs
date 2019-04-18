using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magema : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Emit();
    }

    public float up = 1;
    public float range = 0.5f;

    public GameObject obj;
    [ContextMenu("Emit")]
    public void Emit() {
        var go = Instantiate(obj, transform);
        var rg = go.AddComponent<Rigidbody>();
        var force = Random.insideUnitCircle;
        force = new Vector3(force.x * range, up, force.y * range);
        rg.AddRelativeForce(force);
        Destroy(go, 3f);
        Invoke("Emit",Random.Range(.5f, 5));
    }
}
