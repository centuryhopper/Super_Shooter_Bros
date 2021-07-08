using System.Collections;
using System.Collections.Generic;
using Game.Enums;
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
        public AttackStats attackStats;
        public Enemy enemyPrefab;

        [Space(10)]
        [Header("state Stats")]
        public EnemyState defaultState;
        public float idleLocationRadius = 4f;
        public float idleMovespeedMultiplier = 0.5f;

        public int waypointIndex = 0;
        public float LineOfSightRange = 6f;
        public float FieldOfView = 90f;

        // TODO pick your own waypoints
        public Transform[] waypoints = new Transform[2];


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
        public float StoppingDistance = 2f;

        // this used to be from enemy class, but is
        // now moved to this scriptable object for organizing
        public void SetupAgentFromConfiguration(Enemy enemy)
        {
            #region navmesh agent inits
            enemy.agent.acceleration = Acceleration;
            enemy.agent.angularSpeed = AngularSpeed;
            enemy.agent.areaMask = AreaMask;
            enemy.agent.avoidancePriority = AvoidancePriority;
            enemy.agent.baseOffset = BaseOffset;
            enemy.agent.height = Height;
            enemy.agent.obstacleAvoidanceType = ObstacleAvoidanceType;
            enemy.agent.radius = Radius;
            enemy.agent.speed = Speed;
            enemy.agent.stoppingDistance = StoppingDistance;
            #endregion

            #region ai controller inits
            enemy.aiController.updateRate = AIUpdateInterval;
            enemy.aiController.defaultState = defaultState;
            enemy.aiController.idleLocationRadius = idleLocationRadius;
            enemy.aiController.idleMovespeedMultiplier = idleMovespeedMultiplier;
            enemy.aiController.waypointIndex = waypointIndex;
            enemy.aiController.lineOfSightChecker.FieldOfView = FieldOfView;
            enemy.aiController.lineOfSightChecker.sphereCollider.radius = LineOfSightRange;
            enemy.aiController.lineOfSightChecker.LineOfSightLayers = attackStats.lineOfSightLayers;
            #endregion

            enemy.enemyHealth = Health;

            attackStats.setUpEnemy(enemy);
        }
    }
}
