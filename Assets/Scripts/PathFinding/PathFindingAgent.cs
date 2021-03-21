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

        // We need multiple coroutines to avoid stopping the main thing that
        // would occur if we just used one coroutine
        // new coroutines get appended to the end of the list
        // and finshed ones get removed from the head.
        Queue<Coroutine> moveRoutines = new Queue<Coroutine>();
        public bool hasReachedADestination;
        public GameObject startSphere;
        public GameObject endSphere;
        public struct MeshLinks
        {
            public Vector3 startPos, endPos;
            public MeshLinks(Vector3 s=new Vector3(), Vector3 e=new Vector3())
            {
                startPos = s; endPos = e;
            }
        }

        // lightweight handling of start and end positions on the offmesh link
        public MeshLinks meshLinks;


        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(startSphere.transform.position, .2f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(endSphere.transform.position, .2f);
        }

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            waitForEndOfFrame = new WaitForEndOfFrame();
            startSphere = new GameObject("PathFindingStartSphere");
            endSphere = new GameObject("PathFindingEndSphere");
        }

        public void GoToTarget()
        {
            #region move the agent towards destination
            meshLinks = new MeshLinks();
            agent.enabled = true;
            agent.isStopped = false;
            hasReachedADestination = false;

            startSphere.transform.parent = null;
            endSphere.transform.parent = null;

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
                    // // if they're zero vectors then initialize them!
                    // if (meshLinks.startPos == Vector3.zero && meshLinks.endPos == Vector3.zero)
                    // {
                    //     meshLinks.startPos = agent.currentOffMeshLinkData.startPos;
                    //     meshLinks.endPos = agent.currentOffMeshLinkData.endPos;
                    // }

                    // assign positions before completing the off mesh link
                    startSphere.transform.position = agent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = agent.currentOffMeshLinkData.endPos;
                    agent.CompleteOffMeshLink();
                    yield return waitForEndOfFrame;

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
                    // if (meshLinks.startPos != Vector3.zero && meshLinks.endPos != Vector3.zero)
                    // {
                    //     startSphere.transform.position = meshLinks.startPos;
                    //     endSphere.transform.position = meshLinks.endPos;
                    // }
                    //  no links at this point, just the detination, which is the player
                    startSphere.transform.position = agent.destination;
                    endSphere.transform.position = agent.destination;
                    agent.isStopped = true;
                    hasReachedADestination = true;
                    yield break;
                }

                yield return waitForEndOfFrame;
            }
        }

    }
}
