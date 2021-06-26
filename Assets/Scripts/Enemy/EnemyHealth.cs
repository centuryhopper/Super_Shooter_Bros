using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;
using Game.HealthManager;

namespace Game.EnemyAI
{
    public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
    {
        public float enemyHealth = 100f;
        public bool isDead { get; set; } = false;
        [SerializeField] GameObject enemyRobot = null;
        [SerializeField] GameObject enemyRobotRagdoll = null;
        EnemyMovement enemyMovement = null;
        Rigidbody rb = null;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            // initially disable ragdoll
            enemyRobotRagdoll.SetActive(false);
        }

        public void takeDamage(float damage)
        {
            UnityEngine.Debug.Log($"player taking damage");
            enemyHealth -= damage;
            if (enemyHealth < 0f)
            {
                enemyHealth = 0f;
            }
        }

        public void die()
        {
            if (enemyHealth < 0f)
            {
                isDead = true;
                handleDeath();
            }
        }

        public void handleDeath()
        {
            HealthDamageManager.instance.copyTransformData(enemyRobot.transform, enemyRobotRagdoll.transform, rb.velocity);
            rb.velocity = Vector3.zero;

            // turn on ragdoll and turn off player robot mesh
            enemyRobot.SetActive(false);
            enemyRobotRagdoll.SetActive(true);
        }

        public void resetDeathStatus()
        {
            this.isDead = false;
        }
    }
}
