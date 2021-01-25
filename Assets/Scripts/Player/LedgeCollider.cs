using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    public class LedgeCollider : MonoBehaviour
    {
        [SerializeField] List<GameObject> collidedObjects = new List<GameObject>();

        public List<GameObject> CollidedObjects
        {
            get => collidedObjects;
        }

        void OnTriggerEnter(Collider other)
        {
            // add collided objects to list
            if (!collidedObjects.Contains(other.gameObject) && other.CompareTag("climbable"))
            {
                collidedObjects.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // remove collided objects to list
            // add collided objects to list
            if (collidedObjects.Contains(other.gameObject))
            {
                collidedObjects.Remove(other.gameObject);
            }
        }
    }
}
