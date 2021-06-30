using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowPlayer : MonoBehaviour
    {
        public Transform target;
        public float updateSpeed = 0.1f;
        private NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        IEnumerator Start()
        {
            WaitForSeconds wait = new WaitForSeconds(updateSpeed);

            while (this.enabled)
            {
                agent.SetDestination(target.position);
                yield return wait;
            }


        }
    }
}
