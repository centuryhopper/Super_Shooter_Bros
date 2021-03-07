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
        WaitForEndOfFrame waitForEndOfFrame;

        [ReadOnly]
        public Vector3 startOffMeshPosition, endOffMeshPosition;

        // We need multiple coroutines to avoid stopping the main thing that
        // would occur if we just used one coroutine
        // new coroutines get appended to the end of the list
        // and finshed ones get removed from the head.
        Queue<Coroutine> moveRoutines = new Queue<Coroutine>();
        public bool hasReachedADestination;


        void OnDrawGizmos()
        {
            Gizmos.DrawSphere(startOffMeshPosition, .2f);
            Gizmos.DrawSphere(endOffMeshPosition, .2f);
        }


        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            waitForEndOfFrame = new WaitForEndOfFrame();
        }

        public void GoToTarget()
        {
            #region move the agent towards destination
            agent.enabled = true;
            agent.isStopped = false;
            hasReachedADestination = false;

            if (target == null)
            {
                UnityEngine.Debug.LogWarning($"no target found. Setting target to player character");
                target = GameObject.FindWithTag("Player").transform;
                return;
            }
            agent.SetDestination(target.position);
            #endregion

            // if the coroutine is already running
            if (moveRoutines.Count != 0)
            {
                Coroutine tmp = moveRoutines.Dequeue();
                if (tmp != null) StopCoroutine(tmp);
            }

            moveRoutines.Enqueue(StartCoroutine(Move()));
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

                    // stop the path finding agent from moving and
                    // declare reaching a checkpoint
                    agent.isStopped = true;
                    hasReachedADestination = true;
                    yield break;
                }
                else
                {
                    UnityEngine.Debug.Log($"agent is not on off mesh link");
                }

                // once the agent reaches destination
                var dist = transform.position - agent.destination;
                if (Vector3.SqrMagnitude(dist) < 2)
                {
                    startOffMeshPosition = transform.position;
                    endOffMeshPosition = transform.position;
                    agent.isStopped = true;
                    hasReachedADestination = true;
                    yield break;
                }

                yield return waitForEndOfFrame;
            }
        }

    }
}
