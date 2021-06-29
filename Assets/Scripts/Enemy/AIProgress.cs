using UnityEngine;
using Game.PathFind;
using Game.HealthManager;

namespace Game.EnemyAI
{

    // root of the tree between pathfinding agent and the enemy
    public class AIProgress : MonoBehaviour
    {
        public PathFindingAgent pathFindingAgent;
        public Transform player;

        void Start()
        {
            player = HealthDamageManager.instance.player.transform;
        }

        void Update()
        {
            if (pathFindingAgent == null || pathFindingAgent.startSphere == null)
                return;
            // draw a line between the enemy position and the pathfinding agent's startOffMeshPos
            Debug.DrawLine(transform.position, pathFindingAgent.startSphere.transform.position, Color.green);

        }

        public bool isEndSphereHigher()
        {
            if (endSphereIsStraight()) return false;
            return pathFindingAgent.endSphere.transform.position.y > pathFindingAgent.startSphere.transform.position.y;
        }

        public bool isEndSphereLower()
        {
            if (endSphereIsStraight()) return false;
            return pathFindingAgent.endSphere.transform.position.y < pathFindingAgent.startSphere.transform.position.y;
        }


        /// <summary>
        /// returns true if start and end sphere are roughly on the same level
        /// </summary>
        /// <returns></returns>
        public bool endSphereIsStraight()
        {
            return Mathf.Abs(pathFindingAgent.endSphere.transform.position.y - pathFindingAgent.startSphere.transform.position.y) <= 0.01f;
        }

        // TODO use these functions in the state machine scripts (replace wherever we get the distance)
        public float EnemyToEndOffMeshDistance()
        {
            Vector3 endOffMeshPos = pathFindingAgent.endSphere.transform.position;
            return Vector3.SqrMagnitude(endOffMeshPos - transform.position);
        }

        public float EnemyToStartOffMeshDistance()
        {
            Vector3 startOffMeshPos = pathFindingAgent.startSphere.transform.position;
            return Vector3.SqrMagnitude(startOffMeshPos - transform.position);
        }


    }
}