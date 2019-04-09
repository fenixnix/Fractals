using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class DrawPixel : MonoBehaviour
{
    //public GameObject prefab;
    EntityManager eMngr;
    EntityArchetype entityArchetype;
    public Mesh mesh;
    public Material material;

    public void Draw(Vector3 position) {
        
        //Entity pixel = eMngr.Instantiate(prefab);
        //eMngr.SetComponentData(pixel, new Position { Value = position });

        eMngr = World.Active.GetOrCreateManager<EntityManager>();
        var entities = new NativeArray<Entity>(1, Allocator.Temp);
        //if(prefab) {
        //    eMngr.Instantiate(prefab, entities);
        //}
        //else 
        {
            var archeType = eMngr.CreateArchetype(
                typeof(PixelComponent),
                typeof(Position),
                typeof(MeshInstanceRenderer));
            eMngr.CreateEntity(archeType, entities);
        }

        var meshRenderer = new MeshInstanceRenderer() {
            mesh = mesh,
            material = material,
        };

        for(int i = 0; i < entities.Length; ++i) {
            var entity = entities[i];
            eMngr.SetComponentData(entity, new Position { Value = position });
            eMngr.SetSharedComponentData(entity, meshRenderer);
        }

        entities.Dispose();
    }

    [ContextMenu("Test")]
    public void Test() {
        Draw(Vector3.left);
    }
}
