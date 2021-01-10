using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/landing", order = 0)]
    public class Landing : StateData
    {
        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

    }
}
