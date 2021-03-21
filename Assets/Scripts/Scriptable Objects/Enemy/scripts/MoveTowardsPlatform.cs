

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

    [CreateAssetMenu(fileName = "MoveTowardsPlatform", menuName = "ability/AI/MoveTowardsPlatform", order = 0)]
    public class MoveTowardsPlatform : StateData
    {
        // [Range(0,10)]
        public float glideSpeed = 13f;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            // move enemy ai towards off meshLink end position
            // when it is close enough, then transition to landing
            Vector3 endOffMeshPos = e.aiProgress.pathFindingAgent.endOffMeshPosition;
            // Vector3.MoveTowards(e.transform.position, endOffMeshPos, glideSpeed * Time.deltaTime);

            e.transform.Translate(Vector3.forward * glideSpeed * Time.deltaTime);

            Vector3 enemyToEndOffMeshVector = endOffMeshPos - e.transform.position;

            if (Vector3.SqrMagnitude(enemyToEndOffMeshVector) < .5f)
            {

                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], true);
            }


        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], false);
        }
    }
}
