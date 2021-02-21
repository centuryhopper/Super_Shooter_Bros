using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

namespace Game.PathFind
{
    /// <summary>
    /// Takes a target gameobject parameter and follows it
    /// </summary>
    ///
    [RequireComponent(typeof(NavMeshAgent))]
    public class PathFindingAgent : MonoBehaviour
    {
        [SerializeField] Transform target;
        NavMeshAgent agent;
        Coroutine move;
        WaitForEndOfFrame waitForEndOfFrame;

        [ReadOnly]
        public Vector3 startOffMeshPosition, endOffMeshPosition;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            waitForEndOfFrame = new WaitForEndOfFrame();
        }

        public void GoToTarget()
        {
            agent.isStopped = false;

            if (target == null) { UnityEngine.Debug.LogWarning($"no target found"); return; }
            agent.SetDestination(target.position);

            // if the coroutine is already running
            if (move != null)
            {
                StopCoroutine(move);
            }

            move = StartCoroutine(Move());
        }

        // pause the agent every time it reaches the end of an off mesh link before reaching the
        // target
        IEnumerator Move()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    startOffMeshPosition = transform.position;

                    agent.CompleteOffMeshLink();

                    yield return waitForEndOfFrame;

                    endOffMeshPosition = transform.position;

                    agent.isStopped = true;
                    yield break;
                }
                else
                {
                    UnityEngine.Debug.Log($"agent is not on off mesh link");
                }

                // once the agent reaches destination
                var dist = transform.position - agent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startOffMeshPosition = transform.position;
                    endOffMeshPosition = transform.position;
                    agent.isStopped = true;
                    yield break;
                }

                yield return waitForEndOfFrame;
            }
        }

    }
}
