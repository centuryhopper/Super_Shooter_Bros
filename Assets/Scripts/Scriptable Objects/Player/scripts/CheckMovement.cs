using Game.Enums;
using Game.Hash;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckMovement", menuName = "ability/Check Movement", order = 0)]
    public class CheckMovement : StateData
    {
        private PlayerMovement playerMovement = null;

        override public void OnEnter(CharacterState character, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = character.GetPlayerMoveMent(a);
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            // check whether the player should sprint
            if (playerMovement.moveLeft || playerMovement.moveRight)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {}
    }
}
