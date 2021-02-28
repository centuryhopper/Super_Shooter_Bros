using Game.Enums;
using Game.Hash;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "idle", menuName = "ability/idle")]
    public class Idle : StateData
    {
        private PlayerMovement playerMovement = null;

        override public void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);

            // prevents the bug: player jumping while airborne
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], true);

            // player can double jump again
            PlayerMovement.numJumps = 2;
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            // only determine when to switch to the walk animation
            if (playerMovement.moveRight)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else if (playerMovement.moveLeft)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else if (playerMovement.jump)
            {
                // Debug.Log($"jump");
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            ;
        }
    }
}