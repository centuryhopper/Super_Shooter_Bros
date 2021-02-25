using UnityEngine;

namespace Game.Pooling
{
    public interface IPooledObject
    {
        void OnObjectSpawn();

        void OnObjectSpawn(Transform transform);
    }
}
