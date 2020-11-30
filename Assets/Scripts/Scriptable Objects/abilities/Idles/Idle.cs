using System.Linq;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "idle", menuName = "idle")]
    public class Idle : StateData
    {
        PlayerController playerController = null;

        override public void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            playerController = c.GetPlayerController(a);
            Debug.Log(playerController);

            // prevents the bug: player jumping while airborne
            a.SetBool(AnimationParameters.jump.ToString(), false);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
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