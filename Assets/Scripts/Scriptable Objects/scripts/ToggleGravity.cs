using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Toggle Gravity", order = 0)]
    public class ToggleGravity : StateData
    {
        public bool gravitySwitch;
        public bool onStart, onEnd;
        public float customTiming = 0.9f;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (onStart)
            {
                toggleGravity(c.GetPlayerMoveMent(a));
            }
        }
        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (customTiming != 0)
            {
                // if we've reached more than customTiming
                // time of the animation
                if (customTiming <= asi.normalizedTime)
                {
                    toggleGravity(c.GetPlayerMoveMent(a));
                }
            }
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (onEnd)
            {
                toggleGravity(c.GetPlayerMoveMent(a));
            }
        }

        private void toggleGravity(PlayerMovement p)
        {
            p.RB.velocity = new Vector3(0,0,0);
            p.RB.useGravity = gravitySwitch;
        }
    }
}
