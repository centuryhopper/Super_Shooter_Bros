using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Game.Hash;
using Game.Enums;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "CheckJump", menuName = "ability/CheckJump", order = 0)]
    public class CheckJump : StateData
    {
        private PlayerMovement playerMovement;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
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

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }
}
