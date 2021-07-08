using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Interfaces;
using Game.HealthManager;
using Game.Pooling;

namespace Game.EnemyAI
{
    public class Enemy : PoolableObject, IDamageable
    {
        [Header("Custom Script Components")]
        public EnemyAIController aiController;
        public AttackRadius attackRadius;

        [Space(10)]
        public NavMeshAgent agent;
        public float enemyHealth = 100;
        private const string attack = "attack", stopAttack = "stopAttack";
        public Animator animator;
        Coroutine lookRoutine;
        Rigidbody rb = null;
        [SerializeField] GameObject enemyRobot = null;
        [SerializeField] GameObject enemyRobotRagdoll = null;

        void onAttack(IDamageable target)
        {
            animator.ResetTrigger(stopAttack);
            animator.SetTrigger(attack);
            if (lookRoutine != null)
            {
                StopCoroutine(lookRoutine);
            }

            lookRoutine = StartCoroutine(lookAt(target.getTransform()));
        }
        
        IEnumerator lookAt(Transform target)
        {
            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

            float time = 0;
            while (time < 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * 2;
                yield return null;
            }

            transform.rotation = lookRotation;
        }

        void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            attackRadius.OnAttack += onAttack;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            attackRadius.OnAttack -= onAttack;
            agent.enabled = false;
        }

        public void takeDamage(float damage)
        {
            enemyHealth = Mathf.Max(enemyHealth - damage, 0f);

            if (enemyHealth <= 0f)
            {
                // TODO disable the animated model and enable the ragdoll
                HealthDamageManager.instance.copyTransformData(enemyRobot.transform, enemyRobotRagdoll.transform, rb.velocity);
                rb.velocity = Vector3.zero;

                // turn on ragdoll and turn off player robot mesh
                enemyRobot.SetActive(false);
                enemyRobotRagdoll.SetActive(true);

                // TODO destroy the enemy ragdoll after x seconds
                enemyRobotRagdoll.transform.parent = null;
                this.gameObject.SetActive(false);
            }
        }

        public Transform getTransform()
        {
            return transform;
        }
    }
}
