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

    [CreateAssetMenu(fileName = "FallPlatform", menuName = "ability/AI/FallPlatform", order = 0)]
    public class FallPlatform : StateData
    {
        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI FALLING");

            EnemyMovement e = c.GetEnemyMovement(a);

            // face left or right depending on the end off mesh positions
            if (e.transform.position.z < e.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                e.FaceForward(true);
            }
            else if (e.transform.position.z > e.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                e.FaceForward(false);
            }
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);

            // facing right
            if (e.IsFacingForward)
            {
                if (e.transform.position.z < e.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    e.moveRight = true;
                    e.moveLeft = false;
                }
                else
                {
                    e.moveRight = false;
                    e.moveLeft = false;

                    a.gameObject.SetActive(false);
                    a.gameObject.SetActive(true);
                }
            }

            // facing left
            else
            {
                if (e.transform.position.z > e.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    e.moveRight = false;
                    e.moveLeft = true;
                }
                else
                {
                    e.moveRight = false;
                    e.moveLeft = false;

                    a.gameObject.SetActive(false);
                    a.gameObject.SetActive(true);
                }
            }

        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }
}

