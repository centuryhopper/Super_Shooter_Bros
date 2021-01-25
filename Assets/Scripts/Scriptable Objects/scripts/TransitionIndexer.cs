using System.Collections.Generic;
using Game.Enums;
using Game.Hash;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Transition Indexer", order = 0)]
    public class TransitionIndexer : StateData
    {
        public int Index;
        public List<AirBorneTransitions> transitionConditions = new List<AirBorneTransitions>();
        private PlayerController playerController = null;


        public override void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerController = character.GetPlayerController(a);

            // Debug.Log($"here in indexer");
            if (playerController != null && ShouldMakeTransition(playerController))
            {
                a.SetInteger(HashManager.Instance.animationParamsDict[AnimationParameters.transitionIndex], Index);
            }
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (playerController != null && ShouldMakeTransition(playerController))
            {
                a.SetInteger(HashManager.Instance.animationParamsDict[AnimationParameters.transitionIndex], Index);
            }

            // listens for a down-key press
            else
            {
                // transition the animation back to idle
                // if (playerController is null)
                //     Debug.Log($"playerController is null");
                // else
                //     Debug.Log($"couldnt make transition");
                a.SetInteger(HashManager.Instance.animationParamsDict[AnimationParameters.transitionIndex], 0);
            }
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetInteger(HashManager.Instance.animationParamsDict[AnimationParameters.transitionIndex], 0);
            // if (asi.IsName("CrouchIdle"))
            // {
            //     Debug.Log($"leaving crouch idle");
            // }
        }

        private bool ShouldMakeTransition(PlayerController playerController)
        {
            for (int i = 0; i < transitionConditions.Count; ++i)
            {
                switch (transitionConditions[i])
                {
                    case AirBorneTransitions.UP:
                    {
                        if (!playerController.moveUp)
                        {
                            // Debug.Log("player isn't moving up");
                            return false;
                        }

                        // Debug.Log($"up");
                    }
                    break;
                    case AirBorneTransitions.DOWN:
                    {
                        if (!playerController.moveDown)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.LEFT:
                    {
                        if (!playerController.moveLeft)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.RIGHT:
                    {
                        if (!playerController.moveRight)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.ATTACK:
                    {

                    }
                    break;
                    case AirBorneTransitions.JUMP:
                    {
                        if (playerController.jump)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.GRABBING_LEDGE:
                    {
                        if (!playerController.ledgeChecker.isGrabbingLedge)
                        {
                            return false;
                        }
                    }
                    break;
                }
            }

            return true;
        }
    }
}
