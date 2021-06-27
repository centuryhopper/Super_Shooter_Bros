using System.Collections;
using System.Collections.Generic;
using Game.singleton;
using UnityEngine;
using Game.Interfaces;

namespace Game.Pooling
{
    public class ObjectPooler : Singleton<ObjectPooler>
    {
        [System.Serializable]
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

        private Vector3 zeroVector;

        private int poolSize = 10;
        void Awake()
        {
            zeroVector = Vector3.zero;
            pools = new List<Pool>(3);

            pools.Add(new Pool
            {
                tag = "bullet",
                prefab = Resources.Load<GameObject>("Bullet"),
                size = poolSize
            });
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
                objectToSpawn.GetComponent<Rigidbody>().velocity = zeroVector;
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = trans.position;
                objectToSpawn.transform.rotation = trans.rotation;
            }

            #region I May need to move or delete this

            // search for the interface component in the game object spawned, which happens to be
            // from the Bullet.cs since that script implements IPooledObject, check that it's not null
            // and call its spawn method
            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
                pooledObj.OnObjectSpawn(trans);
            }
            #endregion

            // once spawned, we want to enqueue it again for reuse
            poolDictionary[tag].Enqueue(objectToSpawn);
            // print("recycling");


            return objectToSpawn;
        }
    }
}
