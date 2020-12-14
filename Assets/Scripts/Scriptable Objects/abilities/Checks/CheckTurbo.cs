using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckSprint", menuName = "ability/Check Sprint", order = 0)]
    public class CheckTurbo : StateData
    {
        private PlayerController playerController;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerController = character.GetPlayerController(a);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // check whether the player should sprint
            if (playerController.turbo)
            {
                // Debug.Log("turbo is true");
                a.SetBool(AnimationParameters.turbo.ToString(), true);
            }
            else
            {
                a.SetBool(AnimationParameters.turbo.ToString(), false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // a.SetBool(AnimationParameters.turbo.ToString(), false);
        }
    }
}
