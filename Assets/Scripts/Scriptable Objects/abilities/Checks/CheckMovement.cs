using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckMovement", menuName = "ability/Check Movement", order = 0)]
    public class CheckMovement : StateData
    {
        private PlayerController playerController;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerController = character.GetPlayerController(a);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // check whether the player should sprint
            if (playerController.moveLeft || playerController.moveRight)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {}
    }
}
