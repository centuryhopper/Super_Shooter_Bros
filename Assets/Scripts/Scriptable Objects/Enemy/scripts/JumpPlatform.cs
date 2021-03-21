

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

    [CreateAssetMenu(fileName = "JumpPlatform", menuName = "ability/AI/JumpPlatform", order = 0)]
    public class JumpPlatform : StateData
    {
        [Range(1, 10)]
        public float jumpForce = 5f;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI STARTED JUMPING");
            EnemyMovement e = c.GetEnemyMovement(a);

            e.jump = e.moveUp = true;
            e.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);
            // float topDist = e.aiProgress.pathFindingAgent.EndSphere.transform.position.y - e.;
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], true);

        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], false);
        }
    }
}
