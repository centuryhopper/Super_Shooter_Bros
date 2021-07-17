using Game.States;
using UnityEngine;

namespace Game.EnemyAbilities
{
    [CreateAssetMenu(fileName = "New State", menuName = "AI/ability/force transition", order = 0)]
    public class ForceJumpTransition : StateData
    {
        [Range(0, 1f)]
        public float transitionTime;

        override public void OnEnter(CharacterState character, Animator a, AnimatorStateInfo asi)
        {
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            // timer
            if (asi.normalizedTime >= transitionTime)
            {
                a.SetBool("force_transition", true);
            }
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool("force_transition", false);
        }
    }
}
