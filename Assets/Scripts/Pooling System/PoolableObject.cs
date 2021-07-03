using UnityEngine;

namespace Game.Pooling
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool Parent;

        public virtual void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}