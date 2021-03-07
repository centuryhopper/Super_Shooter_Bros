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
            EnemyMovement e = c.GetEnemyMovement(a);
            UnityEngine.Debug.Log($"SENDING AGENT from {e.gameObject.name}");

            // instantiate pathfinding agent
            if (e.aiProgress.pathFindingAgent == null)
            {
                e.aiProgress.pathFindingAgent = Instantiate(Resources.Load<PathFindingAgent>("PathFindingAgent"), e.transform.position + new Vector3(0, 0, 1f), Quaternion.identity);
            }

            // turn off navmesh
            e.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;

            // AI child will be where the enemy is
            e.aiProgress.transform.position = e.transform.position;

            #region temporarily commented out so that the editor button I created has full control over pathfinding agent
            // e.aiProgress.pathFindingAgent.GoToTarget();
            #endregion

        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);
            Vector3 direction = e.aiProgress.pathFindingAgent.transform.position - e.transform.position;

            // if the pathfinding agent has reached a destination (could be an offmesh link position or the player position), then start to physically move the enemy towards that destination as well
            if (e.aiProgress.pathFindingAgent.hasReachedADestination && Vector3.SqrMagnitude(direction) >= 2f)
            {
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], true);
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }

}
