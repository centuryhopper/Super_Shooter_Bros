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
        public float updateSpeed = 0.1f;
        private NavMeshAgent agent = null;

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

        IEnumerator Start()
        {
            WaitForSeconds wait = new WaitForSeconds(updateSpeed);

            while (this.enabled)
            {
                agent.SetDestination(target.position);
                yield return wait;
            }
        }

        private void handleLinkStart()
        {
            animator.SetTrigger(jump);
        }

        private void handleLinkEnd()
        {
            animator.SetTrigger(didLand);
        }
    }
}
