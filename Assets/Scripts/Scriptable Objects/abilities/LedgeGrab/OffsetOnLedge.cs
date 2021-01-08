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
            // PlayerMovement p = c.GetPlayerMoveMent(a);
            // Transform playerSkin = p.playerSkin;

            // if (p.GetLedgeChecker == null)
            // {
            //     Debug.Log($"ledge checker is null"); return;
            // }
            // playerSkin.parent = p.GetLedgeChecker.getGrabbedLedge.transform;
            // playerSkin.localPosition = p.GetLedgeChecker.getGrabbedLedge.Offset;

            // // zero out velocity to avoid bugs
            // p.RB.velocity = new Vector3(0,0,0);
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }
    }
}
