using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();

    void OnObjectSpawn(Transform transform);
}
