using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pooling
{
    public class BulletSpawner : MonoBehaviour
    {
        ObjectPooler objectPooler;

        // private void Start()
        // {
        //     objectPooler = ObjectPooler.Instance;
        // }

        // private void FixedUpdate()
        // {
        //     objectPooler.InstantiateFromPool("Cube", transform.position, Quaternion.identity);
        // }
    }
}
