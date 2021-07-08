using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.AI;


namespace Game.EnemyAI
{
    /// <summary>
    /// Script that handles objects that are inside this gameobject's sphere collider
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public class AttackRadius : MonoBehaviour
    {
        public SphereCollider sphereCollider;
        private List<IDamageable> damageables = new List<IDamageable>();

        /// <summary>
        /// how much damage to GIVE to victims of this enemy
        /// </summary>
        public float damage = 10;
        public float attackDelay = 0.1f;
        public delegate void AttackEvent(IDamageable target);
        public AttackEvent OnAttack = delegate { };
        private Coroutine attackCoroutine;
        WaitForSeconds wait;
        [SerializeField] Animator animator = null;
        private const string attack = "attack", stopAttack = "stopAttack";
        [SerializeField] NavMeshAgent agent = null;

        private void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
            agent = GetComponentInParent<NavMeshAgent>();
            wait = new WaitForSeconds(attackDelay);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageables.Add(damageable);

                // disable navmesh agent to stop chasing the IDamageable
                agent.enabled = false;

                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(Attack());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                UnityEngine.Debug.Log($"player leaving range");

                // re-enable navmesh agent to start chasing the IDamageable again
                agent.enabled = true;

                damageables.Remove(damageable);
                if (damageables.Count == 0)
                {
                    UnityEngine.Debug.LogWarning($"enemy has nothing to attack");
                    StopCoroutine(attackCoroutine);
                    attackCoroutine = null;

                    animator.ResetTrigger(attack);
                    animator.SetTrigger(stopAttack);
                }
                else
                {
                    UnityEngine.Debug.Log($"enemy still has something to attack");
                }
            }
        }

        // coroutine for attacking the player
        private IEnumerator Attack()
        {
            yield return wait;

            while (damageables.Count > 0)
            {
                UnityEngine.Debug.Log($"here in attack coroutine");
                IDamageable damageable = null;
                for (var i = 0; i < damageables.Count; i++)
                {
                    damageable = damageables[i];

                    // play ai attack animation
                    OnAttack.Invoke(damageable);
                }


                damageable = null;

                yield return wait;

                // TODO may not need this line below because player attack system is a little different from the enemies'
                damageables.RemoveAll(DisabledDamageables);
            }

            attackCoroutine = null;
        }

        private bool DisabledDamageables(IDamageable Damageable)
        {
            return Damageable != null && !Damageable.getTransform().gameObject.activeSelf;
        }
    }
}
