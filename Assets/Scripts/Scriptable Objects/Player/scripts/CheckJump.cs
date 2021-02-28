using UnityEngine;
using Game.Hash;
using Game.Enums;
using Game.States;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckJump", menuName = "ability/CheckJump", order = 0)]
    public class CheckJump : StateData
    {
        private PlayerMovement playerMovement;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            if (playerMovement.jump)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);
            }
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }
}
