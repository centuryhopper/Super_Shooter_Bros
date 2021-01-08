using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "Check Turbo and Movement", menuName = "ability/Check Turbo and Movement", order = 0)]
    public class CheckTurboAndMovement : StateData
    {
        private PlayerController playerController;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerController = character.GetPlayerController(a);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // check whether the player should sprint
            if ((playerController.moveLeft || playerController.moveRight) && playerController.turbo)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.turbo], true);
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.turbo], false);
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }
}