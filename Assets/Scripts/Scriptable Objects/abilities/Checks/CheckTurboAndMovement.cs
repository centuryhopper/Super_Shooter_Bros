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
                a.SetBool(AnimationParameters.turbo.ToString(), true);
                a.SetBool(AnimationParameters.move.ToString(), true);
            }
            else
            {
                a.SetBool(AnimationParameters.turbo.ToString(), false);
                a.SetBool(AnimationParameters.move.ToString(), false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // a.SetBool(AnimationParameters.turbo.ToString(), false);
        }
    }
}