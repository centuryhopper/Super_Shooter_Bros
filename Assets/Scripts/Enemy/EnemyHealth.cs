using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.EnemyAI
{
    public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
    {
        public float enemyHealth = 100f;
        public bool isDead { get; set; } = false;
        [SerializeField] GameObject enemyRobot = null;
        [SerializeField] GameObject enemyRobotRagdoll = null;
        Rigidbody rb = null;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            // initially disable ragdoll
            enemyRobotRagdoll.SetActive(false);
        }

        void Update()
        {
            // make sure we don't die twice with the "!isDead" check
            if (enemyHealth <= 0f && !isDead)
            {
                die();
                handleDeath();
            }
        }

        public void takeDamage(float damage)
        {
            enemyHealth -= damage;
            if (enemyHealth < 0f)
            {
                enemyHealth = 0f;
            }
        }

        public void die()
        {
            isDead = true;
        }

        public void handleDeath()
        {
            // HealthDamageManager.instance.copyTransformData(enemyRobot.transform, enemyRobotRagdoll.transform, rb.velocity);
            rb.velocity = Vector3.zero;

            // TODO need to stop using this because it won't scale for multiple enemies
            // Physics.IgnoreLayerCollision(7,8, true);
            // UnityEngine.Debug.Log($"ignoring collisions between {LayerMask.LayerToName(7)} and {LayerMask.LayerToName(8)}");

            // turn on ragdoll and turn off player robot mesh
            enemyRobot.SetActive(false);
            enemyRobotRagdoll.SetActive(true);
            // have the ragdoll be its own parent so that disabling the enemy bot
            // won't disable the ragdoll
            enemyRobotRagdoll.transform.parent = null;
            this.gameObject.SetActive(false);
        }

        public void resetDeathStatus()
        {
            this.isDead = false;
        }
    }
}
