using System.Collections.Generic;
using Game.Enums;
using Game.Hash;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "idle", menuName = "idle")]
    public class Idle : StateData
    {
        private PlayerController playerController = null;
        private PlayerMovement playerMovement = null;
        private RigBuilder rigBuilder = null;
        private List<RigLayer> rigLayers = null;

        override public void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            playerController = c.GetPlayerController(a);
            playerMovement = c.GetPlayerMoveMent(a);
            rigBuilder = a.GetComponent<RigBuilder>();
            rigLayers = rigBuilder.layers;

            // rigBuilder.enabled = true;
            // rigLayers.ForEach(r =>
            // {
            //     r.active = true;
            //     Debug.Log($"{r.name}");
            // });

            // todo set each rig layer and child values to hardcoded values

            // rigLayers[0].rig.weight = 1;
            // MultiAimConstraint m = rigLayers[0].rig.transform.GetChild(0).GetComponent<MultiAimConstraint>();

            // prevents the bug: player jumping while airborne
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);

            // player can double jump again
            PlayerMovement.numJumps = 2;
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // Debug.Log($"here in idle");
            // foreach (RigLayer rigLayer in rigLayers)
            // {
            //     switch (rigLayer.name)
            //     {
            //         case "RigLayer_BodyAim":

            //             rigLayer.rig.weight = 1;

            //             // headaim
            //             MultiAimConstraint headAim = rigLayer.rig.transform.Find("HeadAim").GetComponent<MultiAimConstraint>();
            //             Debug.Log($"head aim multi constraint weight: {headAim.weight}");
            //             headAim.weight = 1;
            //             Debug.Log($"head aim multi constraint weight after: {headAim.weight}");
            //             // Debug.Log($"head weight: {headAim.data.sourceObjects[0].weight}");
            //             headAim.data.sourceObjects.SetWeight(0, 1);


            //             break;
            //         case "RigLayer_WeaponPose":
            //             rigLayer.rig.weight = 1;
            //             break;
            //         case "RigLayer_WeaponAim":
            //             rigLayer.rig.weight = 1;
            //             break;
            //         case "RigLayer_HandIK":
            //             rigLayer.rig.weight = 1;
            //             break;
            //         default:
            //             Debug.LogWarning($"Unregistered RigLayer");
            //             break;
            //     }

            // }





            // only determine when to switch to the walk animation
            if (playerController.moveRight)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else if (playerController.moveLeft)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], true);
            }
            else if (playerController.jump)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], true);
            }
            else
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            ;
        }
    }
}