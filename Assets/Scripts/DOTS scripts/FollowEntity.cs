using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// This script will be used to track & follow an entity
/// </summary>
public class FollowEntity : MonoBehaviour
{
    public Entity target;

    // optimized by pushing to the stack
    // instead of assigning it to float3.zero
    public float3 offset = new float3(0,0,0);

    private EntityManager entityManager;

    void Awake()
    {
        // initialize entity
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void LateUpdate()
    {
        // check whether entity is assigned
        if (target == Entity.Null) { return; }

        // grab translation component of target entity
        Translation targetPos = entityManager.GetComponentData<Translation>(target);
        transform.position = targetPos.Value + offset;

        // set current gameobject position to the target's position with an offset added on to it.



    }
}
