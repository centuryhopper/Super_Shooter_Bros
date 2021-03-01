using System.Collections;
using System.Collections.Generic;
using Game.States;
using UnityEngine;
using Game.GenericCharacter;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Lock Transition")]
    public class LockTransition : StateData
    {
        public float unlockTime;

        private PlayerMovement playerMovement;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);
            playerMovement.animationProgress.hasLockedTransition = true;
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
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


        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }
    }
}