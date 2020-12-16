using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/OffsetOnLedge", order = 0)]
    public class OffsetOnLedge : StateData
    {
        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // parent the ledge to the jammo player gameobject and
            // then set the jammo player's local position to the specified ledge offset
            PlayerMovement p = c.GetPlayerMoveMent(a);
            Transform playerSkin = p.PlayerSkin;
            playerSkin.parent = p.GetLedgeChecker.getLedge.transform;
            playerSkin.localPosition = p.GetLedgeChecker.getLedge.Offset;

            // zero out velocity to avoid bugs
            p.RB.velocity = new Vector3(0,0,0);
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // parent the jammo player back to the player character gameobject?
        }
    }
}
