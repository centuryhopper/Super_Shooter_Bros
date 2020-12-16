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

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (onStart)
            {
                PlayerMovement p = c.GetPlayerMoveMent(a);
                toggleGravity(p);
            }
        }
        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (onEnd)
            {
                PlayerMovement p = c.GetPlayerMoveMent(a);
                toggleGravity(p);
            }
        }

        private void toggleGravity(PlayerMovement p)
        {
            p.RB.velocity = new Vector3(0,0,0);
            p.RB.useGravity = gravitySwitch;
        }
    }
}
