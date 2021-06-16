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
        [SerializeField] Transform playerTarget;
        NavMeshAgent agent;
        WaitForEndOfFrame waitForEndOfFrame;
        WaitForSeconds waitForSeconds;
        Coroutine moveRoutine;
        public bool hasReachedADestination, enemyShouldMove;
        public GameObject startSphere;
        public GameObject endSphere;
        public Transform enemyTarget;
        public List<Vector3> meshLinks = new List<Vector3>(2);

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }

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
            waitForSeconds = new WaitForSeconds(0.5f);
            startSphere = new GameObject("PathFindingStartSphere");
            endSphere = new GameObject("PathFindingEndSphere");
            playerTarget = GameObject.FindWithTag("Player").transform;
            enemyTarget = GameObject.Find("EnemyFighter").transform;
        }

        void OnEnable()
        {
            UnityEngine.Debug.Log($"stopping the previous coroutine");
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
            }
        }

        public void GoToTarget()
        {
            meshLinks.Clear();

            #region move the agent towards destination
            agent.enabled = true;
            agent.isStopped = false;
            hasReachedADestination = false;
            enemyShouldMove = false;

            startSphere.transform.parent = null;
            endSphere.transform.parent = null;

            agent.SetDestination(playerTarget.position);
            #endregion

            moveRoutine = StartCoroutine(Move());
        }

        // pause the agent every time it reaches the end of an off mesh link before reaching the
        // target
        IEnumerator Move()
        {
            while (true)
            {
                if (agent.isOnOffMeshLink)
                {
                    UnityEngine.Debug.Log($"agent on off mesh link");
                    if (meshLinks.Count == 0)
                    {
                        meshLinks.Add(agent.currentOffMeshLinkData.startPos);
                        meshLinks.Add(agent.currentOffMeshLinkData.endPos);
                    }

                    startSphere.transform.position = agent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = agent.currentOffMeshLinkData.endPos;

                    // let the agent make the jump
                    agent.CompleteOffMeshLink();

                    agent.isStopped = true;
                    enemyShouldMove = true;

                    // https://riptutorial.com/csharp/example/29811/the-difference-between-break-and-yield-break
                    break;
                }

                // once the agent reaches destination
                var dist = transform.position - agent.destination;
                UnityEngine.Debug.Log($"agent to player distance: {Vector3.SqrMagnitude(dist)}");
                if (Vector3.SqrMagnitude(dist) <= 1f)
                {
                    if (meshLinks.Count > 0)
                    {
                        startSphere.transform.position = meshLinks[0];
                        endSphere.transform.position = meshLinks[1];
                    }
                    else
                    {
                        startSphere.transform.position = agent.destination;
                        endSphere.transform.position = agent.destination;
                    }


                    //  no links at this point, just the destination, which is the player
                    UnityEngine.Debug.Log($"pathfinding agent has reached the player");
                    agent.isStopped = true;
                    hasReachedADestination = true;
                    enemyShouldMove = true;
                    break;
                }

                yield return waitForEndOfFrame;
            }
            yield return waitForSeconds;
        }

    }
}
