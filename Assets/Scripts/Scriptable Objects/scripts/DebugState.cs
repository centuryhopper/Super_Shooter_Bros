using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// Used to retrieve information from animation states
    /// This script should be used as a debugger tool
    /// and should not be on any animation states in the
    /// production build
    /// </summary>
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Debug State", order = 0)]
    public class DebugState : StateData
    {
        // private PlayerMovement playerMovement = null;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            Debug.Log(asi.IsName("HangingIdle") ? "turning collider off" : "turning collider on");


        }
        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }
    }
}
