using Game.PathFind;
using Game.States;
using UnityEngine;
using UnityEngine.AI;
using Game.Enums;
using Game.Hash;

namespace Game.EnemyAI
{
    [CreateAssetMenu(fileName = "SendPathFindingAgent", menuName = "ability/AI/SendPathFindingAgent", order = 0)]
    public class SendPathFindingAgent : StateData
    {
        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement enemyMovement = c.GetEnemyMovement(a);
            UnityEngine.Debug.Log($"SENDING AGENT to {enemyMovement.gameObject.name}");

            // instantiate pathfinding agent
            if (enemyMovement.aiProgress.pathFindingAgent == null)
            {
                enemyMovement.aiProgress.pathFindingAgent = Instantiate(Resources.Load<PathFindingAgent>("PathFindingAgent"));
            }

            // turn off navmesh
            enemyMovement.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;

            // start the agent to the enemy sending it
            enemyMovement.aiProgress.transform.position = enemyMovement.transform.position;

            enemyMovement.aiProgress.pathFindingAgent.GoToTarget();

        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement enemyMovement = c.GetEnemyMovement(a);

            // if the pathfinding agent has reached a destination (could be an offmesh link position or the player position), then start to physically move the enemy towards that destination as well
            if (enemyMovement.aiProgress.pathFindingAgent.hasReachedADestination)
            {
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], true);
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);
        }
    }

}
