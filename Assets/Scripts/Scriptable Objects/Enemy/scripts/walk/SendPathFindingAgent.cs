using System.Collections;
using System.Collections.Generic;
using Game.States;
using UnityEngine;

namespace Game.EnemyAI
{
    [CreateAssetMenu(fileName = "SendPathFindingAgent", menuName = "ability/AI/SendPathFindingAgent", order = 0)]
    public class SendPathFindingAgent : StateData
    {

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }

}
