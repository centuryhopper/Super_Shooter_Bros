using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Teleport On Ledge", order = 0)]
    public class TeleportOnLedge : StateData
    {
        private PlayerMovement playerMovement = null;
        private RigBuilder rigBuilder = null;

        public override void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // initial cache
            playerMovement = FindObjectOfType<PlayerMovement>();
            rigBuilder = a.GetComponent<RigBuilder>();
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // if (playerMovement == null)
            // {
            //     Debug.Log($"playerMovement is null"); return;
            // }

            // if (playerMovement.GetLedgeChecker == null)
            // {
            //     Debug.Log($"ledgechecker not found"); return;
            // }

            // if (playerMovement.GetLedgeChecker.getGrabbedLedge == null)
            // {
            //     Debug.Log($"ledgechecker not found"); return;
            // }

            // // parent the jammo player back to the player character gameobject

            // // rarely happens but there might be a null ref exception here
            // Vector3 grabbedLedgeEndPosition = playerMovement.GetLedgeChecker.getGrabbedLedge.transform.position +
            //                                     playerMovement.GetLedgeChecker.getGrabbedLedge.EndPosition;

            // // adjust the player character's position to the end position of the grabbed ledge
            // playerMovement.transform.position = grabbedLedgeEndPosition;

            // // ajdust the jammo player's position to the end position of the grabbed ledge
            // playerMovement.playerSkin.position = grabbedLedgeEndPosition;

            // // parent the jammo player back to the player character
            // playerMovement.playerSkin.parent = playerMovement.transform;
            // playerMovement.playerSkin.transform.SetAsFirstSibling();
        }
    }
}
