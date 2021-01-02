using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Pooling;

namespace Game.PlayerCharacter
{
    public class Shooting : MonoBehaviour
    {
        public Transform firePoint;
        public string bulletTag = "bullet";
        ObjectPooler bulletPooler;

        void Start()
        {
            // initialize bullet gameobject pooler
            bulletPooler = ObjectPooler.Instance;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // spawn from pool
                GameObject bullet = bulletPooler.InstantiateFromPool(bulletTag, firePoint);
            }
        }
    }
}
