using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 0;
}

class RotatorSystem : ComponentSystem {
    protected override void OnUpdate() { 
        var t = Time.deltaTime;
        foreach(var e in GetEntities<Components>()) {
            e.transform.Rotate(0, e.rotator.speed * t, 0);
        }
    }

    struct Components {
        public Rotator rotator;
        public Transform transform;  
    }
}
