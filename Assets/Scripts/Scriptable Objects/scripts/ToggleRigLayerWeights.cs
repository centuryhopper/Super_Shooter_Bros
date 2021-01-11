using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Toggle Rig Weights", order = 0)]
    public class ToggleRigLayerWeights : StateData
    {
        public bool onStart, onEnd;

        [Range(0,1)]
        public int rigWeight;
        private PlayerMovement playerMovement = null;
        private Action<int> toggleRigCaller;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // Debug.Log($"setting rig weights to {rigWeight}");

            playerMovement = c.GetPlayerMoveMent(a);

        }
        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (onEnd)
            {
                
            }
        }
    }
}
