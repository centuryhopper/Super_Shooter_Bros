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

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI STARTED WALKING");
            EnemyMovement e = c.GetEnemyMovement(a);
            Vector3 direction = e.aiProgress.pathFindingAgent.startOffMeshPosition - e.transform.position;
            // UnityEngine.Debug.Log($"pathfindng agent name: {e.aiProgress.pathFindingAgent.gameObject.name}");

            // move right if the pathfinding agent is to your right
            // otherwise, move left (what if dir.z is 0?)
            if (direction.z > 0)
            {
                e.moveRight = true;
                e.moveLeft = false;
            }
            else
            {
                e.moveLeft = true;
                e.moveRight = false;
            }
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);
            Vector3 direction = e.aiProgress.pathFindingAgent.transform.position - e.transform.position;

            // vector between the enemy and the start off mesh position
            Vector3 enemyToStartOffMesh = e.aiProgress.pathFindingAgent.startOffMeshPosition - e.transform.position;

            // UnityEngine.Debug.Log($"agent to ai distance: {Vector3.SqrMagnitude(direction)}");

            // checkpoint is close enough to me, so stop walking
            if (Vector3.SqrMagnitude(direction) < 2f)
            {
                UnityEngine.Debug.Log($"AI STOPPING");
                e.moveLeft = e.moveRight = false;

                // go back to idle animation when close enough to a offmesh link check point
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);

            }
            // whether we should jump on to a platform
            else if (Vector3.SqrMagnitude(enemyToStartOffMesh) < 2f)
            {
                e.moveLeft = e.moveRight = false;
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.jump_platform], true);
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], false);
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.jump_platform], false);
        }
    }
}
