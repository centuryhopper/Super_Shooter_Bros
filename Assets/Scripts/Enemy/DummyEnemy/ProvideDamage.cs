using System.Collections;
using System.Collections.Generic;
using Game.HealthManager;
using UnityEngine;


namespace Game.EnemyAI
{
    /// <summary>
    /// a component on the ai animated model for calling animation events
    /// </summary>
    public class ProvideDamage : MonoBehaviour
    {
        Transform enemyTransform;
        Transform playerTransform;
        float enemyAttackRange = 4f;
        [SerializeField] AttackRadius attackRadius = null;

        void Start()
        {
            enemyTransform = transform.parent;
            playerTransform = HealthDamageManager.instance.player.transform;
        }

        // void Update()
        // {
        //     float distanceToPlayer = (enemyTransform.position - playerTransform.position).sqrMagnitude;
        //     UnityEngine.Debug.Log($"{distanceToPlayer}");
        // }

        /// <summary>
        /// This function is meant to be an animation event (needs monobehaviour to work)
        /// </summary>
        public void damagePlayer()
        {
            float distanceToPlayer = (enemyTransform.position - playerTransform.position).sqrMagnitude;

            if (distanceToPlayer < enemyAttackRange)
                HealthDamageManager.instance.Jab(attackRadius.damage);
        }
    }
}
