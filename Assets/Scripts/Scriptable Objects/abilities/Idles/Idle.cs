using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "idle", menuName = "idle")]
    public class Idle : StateData
    {
        PlayerController playerController = null;
        PlayerMovement playerMovement = null;

        RigBuilder rigBuilder = null;

        override public void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            playerController = c.GetPlayerController(a);
            playerMovement = c.GetPlayerMoveMent(a);
            rigBuilder = a.GetComponent<RigBuilder>();


            rigBuilder.enabled = true;

            // todo set each rig layer and child values to hardcoded values
            List<RigLayer> rigLayers = rigBuilder.layers;
            rigLayers[0].rig.weight = 1;
            // MultiAimConstraint m = rigLayers[0].rig.transform.GetChild(0).GetComponent<MultiAimConstraint>();

            // prevents the bug: player jumping while airborne
            a.SetBool(AnimationParameters.jump.ToString(), false);

            // player can double jump again
            PlayerMovement.numJumps = 2;
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            float playerSkinFaceDirection = playerMovement.PlayerSkin.eulerAngles.y;
            // Debug.Log(playerSkinFaceDirection);

            // only determine when to switch to the walk animation
            if (playerController.moveRight)
            {
                a.SetBool(AnimationParameters.move.ToString(), true);
            }
            else if (playerController.moveLeft)
            {
                a.SetBool(AnimationParameters.move.ToString(), true);
            }
            else if (playerController.jump)
            {
                a.SetBool(AnimationParameters.jump.ToString(), true);
            }
            else
            {
                a.SetBool(AnimationParameters.move.ToString(), false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            ;
        }
    }
}