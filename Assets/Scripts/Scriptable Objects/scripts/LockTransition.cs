using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/LockTransition")]
    public class LockTransition : StateData
    {
        public float unlockTime;

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }

        // public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        // {
        //     characterState.characterControl.animationProgress.LockTransition = true;
        // }

        // public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        // {
        //     if (stateInfo.normalizedTime > UnlockTime)
        //     {
        //         characterState.characterControl.animationProgress.LockTransition = false;
        //     }
        //     else
        //     {
        //         characterState.characterControl.animationProgress.LockTransition = true;
        //     }
        // }

        // public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        // {

        // }

        // }
    }
}