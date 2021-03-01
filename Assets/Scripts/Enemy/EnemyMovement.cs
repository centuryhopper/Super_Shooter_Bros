using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EnemyAI
{
    public class EnemyMovement : MonoBehaviour
    {
        [HideInInspector]
        public AIProgress aiProgress;

        void Awake()
        {
            aiProgress = GetComponentInChildren<AIProgress>();
            // transform.root
        }
    }
}
