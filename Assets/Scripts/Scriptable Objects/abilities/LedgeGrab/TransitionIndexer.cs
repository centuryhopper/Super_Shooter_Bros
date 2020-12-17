using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    // potential moves performed by the player when airborne
    public enum AirBorneTransitions
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ability/Transition Indexer", order = 0)]
    public class TransitionIndexer : StateData
    {
        public int Index;
        public List<AirBorneTransitions> transitionConditions = new List<AirBorneTransitions>();

        public override void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            PlayerController p = character.GetPlayerController(a);
            if (ShouldMakeTransition(p))
            {
                a.SetInteger(AnimationParameters.transitionIndex.ToString(), Index);
            }
        }

        public override void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            PlayerController p = c.GetPlayerController(a);
            if (ShouldMakeTransition(p))
            {
                a.SetInteger(AnimationParameters.transitionIndex.ToString(), Index);
            }

            // listens for a down-key press
            else
            {
                // transition the animation back to idle
                a.SetInteger(AnimationParameters.transitionIndex.ToString(), 0);
            }

        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetInteger(AnimationParameters.transitionIndex.ToString(), 0);
        }

        private bool ShouldMakeTransition(PlayerController playerController)
        {
            foreach (var transitionCondition in transitionConditions)
            {
                switch (transitionCondition)
                {
                    case AirBorneTransitions.UP:
                    {
                        if (!playerController.moveUp)
                        {
                            // Debug.Log("player isn't moving up");
                            return false;
                        }
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

            // Debug.Log("here");
            return true;
        }
    }
}
