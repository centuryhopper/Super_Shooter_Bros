using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.PathFind;
using Game.States;
using UnityEngine.AI;

namespace Game.EnemyAI
{

    public enum AI_Walk_Transitions
    {
        start_walking,

    }

    [CreateAssetMenu(fileName = "StartWalking", menuName = "ability/AI/StartWalking", order = 0)]
    public class StartWalking : StateData
    {
        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI STARTED WALKING");
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }

}
