using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Lock Transition")]
    public class LockTransition : StateData
    {
        public float unlockTime;

        private PlayerMovement playerMovement;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);
            playerMovement.animationProgress.hasLockedTransition = true;
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (asi.normalizedTime > unlockTime)
            {
                playerMovement.animationProgress.hasLockedTransition = false;
            }
            else
            {
                playerMovement.animationProgress.hasLockedTransition = true;
            }
        }


        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }
    }
}