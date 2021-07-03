using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyAI
{
    [RequireComponent(typeof(AgentLinkMover), typeof(NavMeshAgent))]
    public class EnemyAIController : MonoBehaviour
    {
        private const string isMoving = "isMoving";
        private const string jump = "jump";
        private const string didLand = "didLand";
        private AgentLinkMover linkMover = null;
        private Animator animator = null;
        public Transform target = null;
        public float updateRate = 0.1f;
        private NavMeshAgent agent = null;
        private Coroutine followCoroutine = null;
        public Transform player = null;
        
        void OnEnable()
        {
            linkMover.onLinkStart += handleLinkStart;
            linkMover.onLinkEnd += handleLinkEnd;
        }

        void OnDisable()
        {
            linkMover.onLinkStart -= handleLinkStart;
            linkMover.onLinkEnd -= handleLinkEnd;
        }
        
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            linkMover = GetComponent<AgentLinkMover>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        }

        // change this to a follow target coroutine so we can call it when enemy has spotted the player
        // IEnumerator Start()
        // {
        //     WaitForSeconds wait = new WaitForSeconds(updateRate);

        //     while (this.enabled)
        //     {
        //         agent.SetDestination(target.position);
        //         yield return wait;
        //     }
        // }

        public void StartChasing()
        {
            if (followCoroutine == null)
            {
                followCoroutine = StartCoroutine(FollowTarget());
            }
            else
            {
                Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
            }
        }

        private IEnumerator FollowTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);

            while (gameObject.activeSelf)
            {
                agent.SetDestination(player.transform.position);
                yield return wait;
            }
        }

        /// <summary>
        /// Handles what animation to play when starting the jump
        /// </summary>
        private void handleLinkStart()
        {
            animator.SetTrigger(jump);
        }

        /// <summary>
        /// Handles what animation to play when finishing the jump
        /// </summary>
        private void handleLinkEnd()
        {
            animator.SetTrigger(didLand);
        }
    }
}
