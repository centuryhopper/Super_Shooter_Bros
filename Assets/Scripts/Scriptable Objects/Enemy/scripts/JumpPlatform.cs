using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.PathFind;
using Game.States;
using Game.Enums;
using Game.Hash;
using Game.EnemyAI;

namespace Game.EnemyAbilities
{
    // NPC follows the path finding agent. And the path finding agent follows the player

    [CreateAssetMenu(fileName = "JumpPlatform", menuName = "ability/AI/JumpPlatform", order = 0)]
    public class JumpPlatform : StateData
    {
        public float jumpForce = 4f;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI JUMPED");
            EnemyMovement e = c.GetEnemyMovement(a);

            e.jump = e.moveUp = true;

            // Ensure the enemy is facing the proper direction when making the jump
            if (e.aiProgress.pathFindingAgent.startSphere.transform.position.z < e.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                e.FaceForward(true);
            }
            else
            {
                e.FaceForward(false);
            }

            e.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            // transition when the enemy is high enough in the air

            // float topDist = e.aiProgress.pathFindingAgent.EndSphere.transform.position.y - e.;
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], true);
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.force_transition], false);
        }
    }
}
