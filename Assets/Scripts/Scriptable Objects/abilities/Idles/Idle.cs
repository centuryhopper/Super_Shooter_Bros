using System.Linq;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "idle", menuName = "idle")]
    public class Idle : StateData
    {
        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            // prevents the bug: player jumping while airborne
            a.SetBool(AnimationParameters.jump.ToString(), false);
        }

        override public void UpdateAbility(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // only determine when to switch to the walk animation
            if (VirtualInputManager.Instance.moveRight)
            {
                a.SetBool(AnimationParameters.move.ToString(), true);
            }
            else if (VirtualInputManager.Instance.moveLeft)
            {
                a.SetBool(AnimationParameters.move.ToString(), true);
            }
            else if (VirtualInputManager.Instance.jump)
            {
                a.SetBool(AnimationParameters.jump.ToString(), true);
            }
            else
            {
                a.SetBool(AnimationParameters.move.ToString(), false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            ;
        }
    }
}