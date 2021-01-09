using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckSprint", menuName = "ability/Check Sprint", order = 0)]
    public class CheckTurbo : StateData
    {
        private PlayerMovement playerMovement;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = character.GetPlayerMoveMent(a);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // check whether the player should sprint
            if (playerMovement.turbo)
            {
                // Debug.Log("turbo is true");
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.turbo], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.turbo], false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // a.SetBool(AnimationParameters.turbo.ToString(), false);
        }
    }
}
