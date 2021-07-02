using System.Collections;
using System.Collections.Generic;
using Game.singleton;
using UnityEngine;
using Game.Interfaces;
using System;

namespace Game.Pooling
{
    /// <summary>
    /// Stores a bunch of objects to be used and recycled for use again
    /// </summary>
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        [Serializable]
        public struct Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        [Tooltip("Will be populated in Start()")]
        public List<Pool> pools;

        /// <summary>
        /// Will be used to identify the proper Queue based on the pool tag
        /// </summary>
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        private int poolSize = 10;
        void Awake()
        {
            pools = new List<Pool>(3);

            pools.Add(new Pool
            {
                tag = "bullet",
                prefab = Resources.Load<GameObject>("Bullet"),
                size = poolSize
            });

            // pools.Add(new Pool
            // {
            //     tag = "particle",
            //     prefab = Resources.Load<GameObject>("BulletImpactFleshSmallEffect"),
            //     size = poolSize
            // });
        }

        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            // ready the objects and disable them by default
            foreach (Pool pool in pools)
            {
                // every pool in our list will have a queue that can be accessed with a tag
                Queue<GameObject> objectPoolQueue = new Queue<GameObject>();

                // pool.size will be the number of objects to spawn and recycled for use
                // again during runtime
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPoolQueue.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPoolQueue);
            }

            // Debug.Log($"your bullet pool size is {poolDictionary["bullet"].Count}");
        }

        /// <summary>
        /// This method takes the object's transform features into account when
        /// instantiating one at runtime
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="trans"></param>
        /// <returns>The gameObject to be instantiated at runtime</returns>
        public GameObject InstantiateFromPool(string tag, Transform trans)
        {
            // codes defensively to check if user spelled the tag correctly
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag" + tag + "dosen't exist");
                return null;
            }

            // Should not be empty because of Start() method populating it
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            // make it active in the game
            if (objectToSpawn != null)
            {
                // reset bullet velocity if it has already been used before
                Rigidbody rb = objectToSpawn.GetComponent<Rigidbody>();
                if (rb != null) rb.velocity = Vector3.zero;
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = trans.position;
                objectToSpawn.transform.rotation = trans.rotation;
            }

            // search for the interface component in the game object spawned, which happens to be
            // from the Bullet.cs since that script implements IPooledObject, check that it's not null
            // and call its spawn method
            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
                pooledObj.OnObjectSpawn(trans);
            }

            // once spawned, we want to enqueue it again for reuse
            poolDictionary[tag].Enqueue(objectToSpawn);
            UnityEngine.Debug.Log($"recycling {objectToSpawn.name}");


            return objectToSpawn;
        }


        /// <summary>
        /// This method takes only the object's position into account when
        /// instantiating one at runtime
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="trans"></param>
        /// <returns>The gameObject to be instantiated at runtime</returns>
        public GameObject InstantiateFromPool(string tag, Vector3 position)
        {
            // codes defensively to check if user spelled the tag correctly
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag" + tag + "dosen't exist");
                return null;
            }

            // Should not be empty because of Start() method populating it
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            // make it active in the game
            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
            }

            // once spawned, we want to enqueue it again for reuse
            poolDictionary[tag].Enqueue(objectToSpawn);
            UnityEngine.Debug.Log($"recycling {objectToSpawn.name}");

            return objectToSpawn;
        }
    
    
    }
}
