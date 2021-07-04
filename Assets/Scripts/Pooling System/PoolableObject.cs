using UnityEngine;

namespace Game.Pooling
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool parent;

        public virtual void OnDisable()
        {
            if (parent == null) UnityEngine.Debug.LogError($"{this.gameObject.name} doesn't have an object pool parent'");
            UnityEngine.Debug.Log($"RETURNING {gameObject.name} to pool");
            parent?.ReturnObjectToPool(this);
        }
    }
}