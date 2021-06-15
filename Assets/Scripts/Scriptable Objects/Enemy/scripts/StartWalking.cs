using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.PathFind;
using Game.States;
using Game.Enums;
using Game.Hash;

namespace Game.EnemyAI
{
    // NPC follows the path finding agent. And the path finding agent follows the player

    [CreateAssetMenu(fileName = "StartWalking", menuName = "ability/AI/StartWalking", order = 0)]
    public class StartWalking : StateData
    {
        [Range(0, 6)]
        public float speed;
        public AnimationCurve speedGraph;
        private Transform player;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI STARTED WALKING");
            EnemyMovement e = c.GetEnemyMovement(a);
            WalkToTarget(e);

            

        }

        public void WalkToTarget(EnemyMovement e)
        {
            player = GameObject.FindWithTag("Player").transform;

            // My observation so far is that the vector from enemy to end off mesh yields less bugs
            // than if we got the vector from enemy to start off mesh. My reasoning would be because
            // that the enemy character could be between the pathfinding agent's start and end position.
            // and we really don't want the enemy character to go the start position and then to the end.
            // In such an edge case, we just want it to go to the end position because that's where it would ultimately end up anyway
            Vector3 enemyToEndOffMesh = e.aiProgress.pathFindingAgent.endSphere.transform.position - e.transform.position;

            // move right if the pathfinding agent is to your right
            // otherwise, move left (what if dir.z is 0?)
            if (enemyToEndOffMesh.z > 0)
            {
                e.moveRight = true;
                e.moveLeft = false;
            }
            else if (enemyToEndOffMesh.z < 0)
            {
                e.moveLeft = true;
                e.moveRight = false;
            }
            else if (enemyToEndOffMesh.z == 0)
            {
                e.moveLeft = e.moveRight = false;
            }
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            Vector3 startOffMeshPos = e.aiProgress.pathFindingAgent.startSphere.transform.position;
            Vector3 endOffMeshPos = e.aiProgress.pathFindingAgent.endSphere.transform.position;
            Vector3 enemyToPathFindingAgent = e.aiProgress.pathFindingAgent.transform.position - e.transform.position;

            Vector3 enemyToStartOffMesh = startOffMeshPos - e.transform.position;

            // checkpoint is close enough to me, so stop walking
            // TODO theres currently a bug for when you change the startOffMesPos too early to a place thats unreachable to the enemy, it will keep on sprinting forever because it only stops until it reaches the startOffMesPos. A fix to this could be to not move the pathfinding agent until the enemy AI reached its startoffmesh position first
            if (Vector3.SqrMagnitude(enemyToPathFindingAgent) < 2f)
            {
                UnityEngine.Debug.Log($"AI STOPPING");
                e.moveLeft = e.moveRight = false;

                // go back to idle animation when close enough to a offmesh link check point
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);
            }

            // whether we should jump on to a platform
            // don't jump if the start offmesh link is at a higher position
            // than the end offmesh link because that means there's a drop

            else if (e.aiProgress.EnemyToStartOffMeshDistance() < 0.01f && endOffMeshPos.y > startOffMeshPos.y)
            {
                // e.hasReachedStartOffMesh = true;
                e.moveLeft = e.moveRight = false;
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.jump_platform], true);
            }

            else if (e.aiProgress.EnemyToStartOffMeshDistance() < 0.01f && endOffMeshPos.y < startOffMeshPos.y)
            {
                // e.hasReachedStartOffMesh = true;
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.fall_platform], true);
            }

            else if (e.aiProgress.EnemyToStartOffMeshDistance() < 0.5f && endOffMeshPos.y == startOffMeshPos.y)
            {
                e.moveLeft = e.moveRight = false;

                // temporary solution for resetting the AI
                Vector3 playerToEnemyAI = e.transform.position - player.position;
                if (playerToEnemyAI.sqrMagnitude > 1f)
                {
                    a.gameObject.SetActive(false);
                    a.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.jump_platform], false);
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.fall_platform], false);
        }
    }
}
