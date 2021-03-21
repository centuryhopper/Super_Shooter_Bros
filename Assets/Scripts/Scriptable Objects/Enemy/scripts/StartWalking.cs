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


            // It might be better to change the vector to point to the path finder's
            // current start or end sphere positions
            Vector3 direction = e.aiProgress.pathFindingAgent.startSphere.transform.position - e.transform.position;
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

            // Vector3 startOffMeshPos = e.aiProgress.pathFindingAgent.startSphere.transform.position;
            // Vector3 endOffMeshPos = e.aiProgress.pathFindingAgent.endSphere.transform.position;
            // UnityEngine.Debug.Log($"offmesh link distance {endOffMeshPos.y - startOffMeshPos.y}");
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            Vector3 startOffMeshPos = e.aiProgress.pathFindingAgent.startSphere.transform.position;
            Vector3 endOffMeshPos = e.aiProgress.pathFindingAgent.endSphere.transform.position;
            Vector3 enemyToPathFindingAgent = e.aiProgress.pathFindingAgent.transform.position - e.transform.position;
            Vector3 enemyToStartOffMesh = startOffMeshPos - e.transform.position;

            // UnityEngine.Debug.Log($"agent to ai distance: {Vector3.SqrMagnitude(direction)}");

            // checkpoint is close enough to me, so stop walking
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

            else if (Vector3.SqrMagnitude(enemyToStartOffMesh) < 0.01f && endOffMeshPos.y > startOffMeshPos.y)
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
