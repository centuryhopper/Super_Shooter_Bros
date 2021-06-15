using UnityEngine;
using Game.PathFind;

namespace Game.EnemyAI
{

    // root of the tree between pathfinding agent and the enemy
    public class AIProgress : MonoBehaviour
    {
        public PathFindingAgent pathFindingAgent;
        public Transform player;

        void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
            if (pathFindingAgent == null || pathFindingAgent.startSphere == null)
                return;
            // draw a line between the enemy position and the pathfinding agent's startOffMeshPos
            Debug.DrawLine(transform.position, pathFindingAgent.startSphere.transform.position, Color.green);

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