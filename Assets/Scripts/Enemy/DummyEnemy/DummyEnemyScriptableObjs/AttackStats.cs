using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Game.Pooling;
using UnityEngine;

namespace Game.EnemyAI
{
    /// <summary>
    /// Holds the base stats for an enemy. These can be modified at object creation time to buff up enemies and to reset their stats if they died or were modified at runtime
    /// </summary>
    [CreateAssetMenu(fileName = "AttackConfiguration", menuName = "AI/Configuration/AttackStats")]
    public class AttackStats : ScriptableObject
    {
        [Header("General configurations")]
        public bool isRanged = false;
        public float damage = 5f;
        public float attackRadius = 1.5f;
        public float attackDelay = 1.5f;

        [Space(10)]
        [Header("Ranged configurations")]
        public Bullet bulletPrefab;
        public Vector3 bulletSpawnOffset = new Vector3(0,1,0);
        public LayerMask lineOfSightLayers;

        public void setUpEnemy(Enemy enemy)
        {
            (enemy.attackRadius.sphereCollider is null ? enemy.attackRadius.GetComponent<SphereCollider>() : enemy.attackRadius.sphereCollider).radius = attackRadius;
            enemy.attackRadius.attackDelay = attackDelay;
            enemy.attackRadius.damage = damage;

            if (isRanged)
            {
                // populate parameters for enemies with ranged attack configs
                // public Bullet bulletPrefab;
                // public Vector3 bulletSpawnOffset = new Vector3(0,1,0);
                // public LayerMask lineOfSightLayers;
            }
        }


    }
}




