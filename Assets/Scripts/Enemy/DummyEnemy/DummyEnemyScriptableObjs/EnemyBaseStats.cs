using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.EnemyAI
{
    /// <summary>
    /// Holds the base stats for an enemy. These can be modified at object creation time to buff up enemies and to reset their stats if they died or were modified at runtime
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "AI/Configuration")]
    public class EnemyBaseStats : ScriptableObject
    {
        [Header("Enemy Stats")]
        public float Health = 100;
        public float attackDelay = 1f;
        public float damage = 5f;
        public float attackRadius = 1.5f;

        [Space(10)]

        [Header("NavMeshAgent configurations")]
        public float AIUpdateInterval = 0.1f;
        public float Acceleration = 8;
        public float AngularSpeed = 120;
        [Tooltip("-1 means everything")]
        public int AreaMask = -1;
        public int AvoidancePriority = 50;
        public float BaseOffset = 0;
        public float Height = 2f;
        public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        public float Radius = 0.5f;
        public float Speed = 3f;
        public float StoppingDistance = 0.5f;
    }
}
