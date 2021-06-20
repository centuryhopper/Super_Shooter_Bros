using Game.PathFind;
using Game.States;
using UnityEngine;
using UnityEngine.AI;
using Game.Enums;
using Game.Hash;
using Game.EnemyAI;

namespace Game.EnemyAbilities
{
    [CreateAssetMenu(fileName = "SendPathFindingAgent", menuName = "ability/AI/SendPathFindingAgent", order = 0)]
    public class SendPathFindingAgent : StateData
    {
        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.jump_platform], false);
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);

            EnemyMovement e = c.GetEnemyMovement(a);
            UnityEngine.Debug.Log($"SENDING AGENT from {e.gameObject.name}");

            // instantiate pathfinding agent
            if (e.aiProgress.pathFindingAgent == null)
            {
                e.aiProgress.pathFindingAgent = Instantiate(Resources.Load<PathFindingAgent>("PathFindingAgent"), e.transform.position + new Vector3(0, 1f, 1f), Quaternion.identity);

                // set speed
                e.aiProgress.pathFindingAgent.SetSpeed(60f);
            }

            if (!e.aiProgress.pathFindingAgent.isActiveAndEnabled) return;

            // turn off navmesh
            e.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            e.aiProgress.pathFindingAgent.GetComponent<CapsuleCollider>().enabled = false;

            // AI child will be where the enemy is
            e.aiProgress.transform.position = e.transform.position;

            #region temporarily commented out so that the editor button I created has full control over pathfinding agent
            e.aiProgress.pathFindingAgent.GoToTarget();
            #endregion

        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            if (!e.aiProgress.pathFindingAgent.isActiveAndEnabled) return;

            // if player is far enough, send pathfinding agent once again (TEMPORARY SOLUTION because we'll probably move this logic to the enemy fight state machine script)
            Vector3 agentToPlayer = e.aiProgress.player.position - e.aiProgress.pathFindingAgent.transform.position;
            if (Vector3.SqrMagnitude(agentToPlayer) > 3f)
            {
                // e.aiProgress.pathFindingAgent.GoToTarget();
            }

            // if the pathfinding agent has reached a destination (could be an offmesh link position or the player position), then start to physically move the enemy towards that destination as well
            if (e.aiProgress.pathFindingAgent.enemyShouldMove)
            {
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], true);
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            // a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);
        }
    }

}
