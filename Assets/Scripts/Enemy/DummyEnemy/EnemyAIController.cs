using System;
using System.Collections.Generic;
using System.Collections;
using Game.Enums;
using UnityEngine;
using UnityEngine.AI;
using Game.Interfaces;

namespace Game.EnemyAI
{
    [RequireComponent(typeof(AgentLinkMover), typeof(NavMeshAgent))]
    public class EnemyAIController : MonoBehaviour
    {
        [Header("General")]
        public NavMeshTriangulation triangulation;
        private const string isMoving = "isMoving";
        private const string jump = "jump";
        private const string didLand = "didLand";
        private Animator animator = null;
        public Transform target = null;
        public float updateRate = 0.1f;
        private NavMeshAgent agent = null;
        private Coroutine stateCoroutine = null;
        public Transform player = null;
        public delegate void StateChangeEvent(EnemyState oldState, EnemyState newState);
        public StateChangeEvent onStateChange = delegate {};
        [Space(10)]
        [Header("State config")]
        private AgentLinkMover linkMover = null;
        public EnemyLineOfSightChecker lineOfSightChecker;
        private EnemyState state;
        public EnemyState State
        {
            get => state;
            set
            {
                // calls handleStateChange
                onStateChange.Invoke(state, value);
                state = value;
            }
        }

        public EnemyState defaultState;

        [Space(10)]
        [Header("Idle state parameters")]
        public float idleLocationRadius = 4f;
        public float idleMovespeedMultiplier = 0.5f;

        [Space(10)]
        [Header("Patrol state parameters")]
        public int waypointIndex = 0;

        // TODO choose your own waypoints for each enemy
        // Use transform[] instead
        Vector3[] waypoints = new Vector3[2];
        [SerializeField] Animation anim = null;

        void Awake()
        {
            if (agent is null)
                agent = GetComponent<NavMeshAgent>();
            if (linkMover is null)
                linkMover = GetComponent<AgentLinkMover>();
            if (animator is null)
                animator = GetComponentInChildren<Animator>();
            if (lineOfSightChecker is null)
                lineOfSightChecker = GetComponentInChildren<EnemyLineOfSightChecker>();
        }

        void Update()
        {
            animator.SetBool(isMoving, agent.velocity.magnitude > Mathf.Epsilon);
        }

        void OnEnable()
        {
            linkMover.onLinkStart += handleLinkStart;
            linkMover.onLinkEnd += handleLinkEnd;
            lineOfSightChecker.OnGainSight += handleGainSight;
            lineOfSightChecker.OnLoseSight += handleLoseSight;

            onStateChange += handleStateChange;
        }

        void OnDisable()
        {
            linkMover.onLinkStart -= handleLinkStart;
            linkMover.onLinkEnd -= handleLinkEnd;
            onStateChange -= handleStateChange;

            lineOfSightChecker.OnGainSight -= handleGainSight;
            lineOfSightChecker.OnLoseSight -= handleLoseSight;

            state = defaultState;
        }

        private void handleGainSight(IHealable player)
        {
            UnityEngine.Debug.LogWarning($"{gameObject.name} sees the player");
            State = EnemyState.Chase;
        }

        private void handleLoseSight(IHealable player)
        {
            UnityEngine.Debug.LogWarning($"{gameObject.name} lost sight of the player");
            State = defaultState;
        }

        private void handleStateChange(EnemyState oldState, EnemyState newState)
        {
            if (oldState == newState) return;

            // since we are changing state in this method, we need to
            // stop the last coroutine from running if it was
            if (stateCoroutine != null) StopCoroutine(stateCoroutine);

            // decrease the speed of enemy when moving in idle state
            if (oldState == EnemyState.Idle)
            {
                agent.speed /= idleMovespeedMultiplier;
            }

            // check states
            // TODO maybe change to interfaces later on for maintainability?
            // do a difference coroutine for each state
            switch(newState)
            {
                case EnemyState.Idle:
                    stateCoroutine = StartCoroutine(doIdleMotion());
                    break;
                case EnemyState.Patrol:
                    stateCoroutine = StartCoroutine(doPatrolMotion());
                    break;
                case EnemyState.Chase:
                    stateCoroutine = StartCoroutine(chaseTarget());
                    break;
            }
        }

        private IEnumerator doPatrolMotion()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);

            yield return new WaitUntil(() => agent.enabled && agent.isOnNavMesh);
            agent.SetDestination(waypoints[waypointIndex]);

            while (true)
            {
                if (agent.isOnNavMesh && agent.enabled && agent.remainingDistance <= agent.stoppingDistance)
                {
                    waypointIndex++;
                    waypointIndex%=waypoints.Length;

                    // if (waypointIndex >= waypoints.Length)
                    // {
                    //     waypointIndex = 0;
                    // }

                    agent.SetDestination(waypoints[waypointIndex]);
                }

                yield return wait;
            }
        }

        private IEnumerator doIdleMotion()
        {
            WaitForSeconds Wait = new WaitForSeconds(updateRate);

            agent.speed *= idleMovespeedMultiplier;

            while (true)
            {
                // make sure the enemy is enabled and on the navmesh
                if (!agent.enabled || !agent.isOnNavMesh)
                {
                    yield return Wait;
                }
                else if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector2 point = UnityEngine.Random.insideUnitCircle * idleLocationRadius;
                    NavMeshHit hit;
                    // TODO change point.x to 0 DONE
                    if (NavMesh.SamplePosition(agent.transform.position + new Vector3(0, 0, point.y), out hit, 2f, agent.areaMask))
                    {
                        agent.SetDestination(hit.position);
                    }
                }

                yield return Wait;
            }
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

        public void spawn()
        {
            #region old code
            // if (followCoroutine == null)
            // {
            //     followCoroutine = StartCoroutine(FollowTarget());
            // }
            // else
            // {
            //     Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
            // }
            #endregion

            // populate our waypoints
            for (var i = 0; i < waypoints.Length; i++)
            {
                if (NavMesh.SamplePosition(triangulation.vertices[UnityEngine.Random.Range(0, triangulation.vertices.Length)], out NavMeshHit hit, 2f, agent.areaMask))
                {
                    waypoints[i] = hit.position;
                }
                else
                {
                    Debug.LogError("Unable to find position for navmesh near Triangulation vertex!");
                }
            }

            onStateChange.Invoke(EnemyState.Spawn, defaultState);
        }

        private IEnumerator chaseTarget()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);

            while (gameObject.activeSelf)
            {
                // base layer is 0, attack layer is 1
                if (agent.enabled && !animator.GetCurrentAnimatorStateInfo(1).IsName("AttackLayer.Punch"))
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

        void OnDrawGizmos()
        {
            int n = waypoints.Length;
            for (var i = 0; i < n; i++)
            {
                Gizmos.DrawSphere(waypoints[i], .25f);
                Gizmos.DrawLine(waypoints[i], waypoints[(i+1) % n]);
            }
        }
    }
}
