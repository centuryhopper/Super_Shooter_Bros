using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Teleport On Ledge", order = 0)]
    public class TeleportOnLedge : StateData
    {

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            PlayerMovement p = c.GetPlayerMoveMent(a);

            // rarely happens but there was a nullref exception here
            Vector3 grabbedLedgeEndPosition = p.GetLedgeChecker.getGrabbedLedge.transform.position +
                                                p.GetLedgeChecker.getGrabbedLedge.EndPosition;

            if (p is null) Debug.Log("null");

            // adjust the player character's position to the end position of the grabbed ledge
            p.transform.position = grabbedLedgeEndPosition;

            // ajdust the jammo player's position to the end position of the grabbed ledge
            p.PlayerSkin.position = grabbedLedgeEndPosition;

            // parent the jammo player back to the player character
            p.PlayerSkin.parent = p.transform;
        }
    }
}
