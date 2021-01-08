using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Move Up", order = 0)]
    public class MoveUp : StateData
    {
        public AnimationCurve speedGraph;
        public float speed = 3;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {


        }
        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // only run when gravity is turned off
            if (!c.GetPlayerMoveMent(a).RB.useGravity)
            {
                c.GetPlayerMoveMent(a).transform.Translate(Vector3.up * speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
            }
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

    }
}
