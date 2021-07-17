using System.Collections.Generic;
using UnityEngine;

namespace Game.Pooling
{
    public class ObjectPool
    {
        private PoolableObject prefab;
        private int size;
        private List<PoolableObject> availableObjectsPool;

        // private because we don't want other classes to instantiate a new object pool instance
        private ObjectPool(PoolableObject Prefab, int Size)
        {
            this.prefab = Prefab;
            this.size = Size;
            availableObjectsPool = new List<PoolableObject>(Size);
        }

        public static ObjectPool CreateInstance(PoolableObject Prefab, int Size)
        {
            ObjectPool pool = new ObjectPool(Prefab, Size);

            GameObject poolGameObject = new GameObject(Prefab + " Pool");
            pool.CreateObjects(poolGameObject);

            return pool;
        }

        private void CreateObjects(GameObject parent)
        {
            for (int i = 0; i < size; i++)
            {
                PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
                poolableObject.parent = this;
                poolableObject.gameObject.SetActive(false); // PoolableObject handles re-adding the object to the AvailableObjects
            }
        }

        public PoolableObject GetObject()
        {
            // can't get an object if there aren't any available
            if (availableObjectsPool.Count == 0) return null;

            PoolableObject instance = availableObjectsPool[0];
            availableObjectsPool.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public PoolableObject GetAndSetObject(Transform transform)
        {
            // can't get an object if there aren't any available
            if (availableObjectsPool.Count == 0) return null;

            PoolableObject instance = availableObjectsPool[0];
            instance.transform.SetPositionAndRotation(transform.position, transform.rotation);
            availableObjectsPool.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void ReturnObjectToPool(PoolableObject Object)
        {
            availableObjectsPool.Add(Object);
        }
    }
}