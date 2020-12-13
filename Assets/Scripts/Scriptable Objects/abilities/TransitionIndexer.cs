using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
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
        }

        public override void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            throw new System.NotImplementedException();
        }

        private bool ShouldMakeTransition(PlayerController playerController)
        {
            foreach (var transitionCondition in transitionConditions)
            {
                switch (transitionCondition)
                {
                    case AirBorneTransitions.UP:
                    {
                        // if (!playerController.moveUp)
                        // {

                        // }
                    }
                    break;
                    case AirBorneTransitions.DOWN:
                    {
                        // if (!playerController.moveDown)
                        // {

                        // }
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

                    }
                    break;
                }
            }

            return true;
        }
    }
}
