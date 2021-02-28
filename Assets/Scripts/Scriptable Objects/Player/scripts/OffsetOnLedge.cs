using System.Collections;
using System.Collections.Generic;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/OffsetOnLedge", order = 0)]
    public class OffsetOnLedge : StateData
    {
        private PlayerMovement playerMovement = null;

        public Vector3 offset;

        Vector3 debugStartPos;


        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = c.GetPlayerMoveMent(a);
            // playerMovement.transform.position += offset;
            debugStartPos = playerMovement.transform.position;
        }
        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            playerMovement.transform.position = debugStartPos + offset;
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {

        }

    }
}
