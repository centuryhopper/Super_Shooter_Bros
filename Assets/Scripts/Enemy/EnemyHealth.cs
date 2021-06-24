using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.EnemyAI
{
    public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
    {
        public float enemyHealth = 100f;
        public bool isDead { get; set;} = false;
        [SerializeField] GameObject enemyRobot = null;
        [SerializeField] GameObject enemyRobotRagdoll = null;
        EnemyMovement enemyMovement = null;
        Rigidbody rb = null;

        void Start()
        {
            // rb = GetComponent<Rigidbody>();
            // playerMovement = GetComponent<PlayerMovement>();
            // // initially disable ragdoll
            // playerRobotRagdoll.SetActive(false);
        }

        public void takeDamage(float damage)
        {

        }

        public void die()
        {

        }

        public void handleDeath()
        {

        }
    }
}
