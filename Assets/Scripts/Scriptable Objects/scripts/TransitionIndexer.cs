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
        private PlayerMovement playerMovement = null;


        public override void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = character.GetPlayerMoveMent(a);

            // Debug.Log($"here in indexer");
            if (playerMovement != null && ShouldMakeTransition(playerMovement))
            {
                a.SetInteger(HashManager.Instance.animationParamsDict[AnimationParameters.transitionIndex], Index);
            }
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            if (playerMovement != null && ShouldMakeTransition(playerMovement))
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

        private bool ShouldMakeTransition(PlayerMovement playerMovement)
        {
            for (int i = 0; i < transitionConditions.Count; ++i)
            {
                switch (transitionConditions[i])
                {
                    case AirBorneTransitions.UP:
                    {
                        if (!playerMovement.moveUp)
                        {
                            // Debug.Log("player isn't moving up");
                            return false;
                        }

                        // Debug.Log($"up");
                    }
                    break;
                    case AirBorneTransitions.DOWN:
                    {
                        if (!playerMovement.moveDown)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.LEFT:
                    {
                        if (!playerMovement.moveLeft)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.RIGHT:
                    {
                        if (!playerMovement.moveRight)
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
                        if (playerMovement.jump)
                        {
                            return false;
                        }
                    }
                    break;
                    case AirBorneTransitions.GRABBING_LEDGE:
                    {
                        if (!playerMovement.GetLedgeChecker.isGrabbingLedge)
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
