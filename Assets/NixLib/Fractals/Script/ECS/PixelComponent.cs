using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct PixelComponentData : IComponentData {
    public Vector3 position;
}
public class PixelComponent : ComponentDataWrapper<PixelComponentData>{ }
