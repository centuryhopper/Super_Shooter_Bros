using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pooling
{
    public class AutoDestroyPoolableObject : PoolableObject
    {
        private const string disableMethodName = "Disable";
        public float autoDestroyTime = 5f;

        public virtual void OnEnable()
        {
            // really important that we cancel invoke befoirehand. Otherwise all objects will after 'autoDestroyTime' seconds get disabled
            CancelInvoke(disableMethodName);
            Invoke(disableMethodName, autoDestroyTime);
        }

        public virtual void Disable()
        {
            this.gameObject.SetActive(false);
        }
    }
}
