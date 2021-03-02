using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.PathFind;
using Game.States;
using Game.Enums;

namespace Game.EnemyAI
{

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
