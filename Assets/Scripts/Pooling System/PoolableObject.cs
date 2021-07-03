using UnityEngine;

namespace Game.Pooling
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool parent;

        public virtual void OnDisable()
        {
            if (parent == null) UnityEngine.Debug.LogError($"{this.gameObject.name} doesn't have an object pool parent'");
            parent?.ReturnObjectToPool(this);
        }
    }
}