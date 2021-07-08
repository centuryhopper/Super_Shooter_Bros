using System.Collections.Generic;
using System.Linq;
using Game.Enums;
using Game.Hash;
using Game.States;
using UnityEngine;
using Game.HealthManager;
using Game.Interfaces;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/move", order = 0)]
    public class MoveForward : StateData
    {
        public AnimationCurve speedGraph;
        public float speed;
        public float faceDirection;

        [Range(.01f, 5)]
        public float directionBlock = 0.3f;

        [Range(.01f,5f)]
        public float blockDistance = 0.5f;
        private PlayerMovement playerMovement;
        private float movementDirection = 1;
        private List<GameObject> spheresList;
        public float distanceOfDetection = 0;




        override public void OnEnter(CharacterState character, Animator a, AnimatorStateInfo asi)
        {
            playerMovement = character.GetPlayerMoveMent(a);
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            MovePlayer(playerMovement, a, asi);
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
        }


        // TODO potential stutters may occur when moving into a wall. Need more analysis
        // todo further down the road.
        /// <summary>
        /// moves the player left and right
        /// </summary>
        void MovePlayer(PlayerMovement p, Animator a, AnimatorStateInfo asi)
        {
            // help decide whether to walk forward
            // and backwards based on player's
            // facing direction
            faceDirection = p.faceDirection;

            // side scroller
            if (playerMovement.moveRight)
            {
                a.SetFloat(HashManager.Instance.animationParamsDict[AnimationParameters.faceDirection], faceDirection);

                // facing right
                if (faceDirection == 1)
                {
                    if (!IsBlocked(p))
                    {
                        movementDirection = 1;
                        // multiple by the speed graph value so that we can still move while we jump
                        p.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                    }
                    else
                    {
                        UnityEngine.Debug.Log($"obstruction on the right");
                    }
                }
                // facing left
                else if (faceDirection == -1)
                {
                    movementDirection = -1;
                    // multiple by the speed graph value so that we can still move while we jump
                    p.transform.Translate(Vector3.forward * -speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                }

                // todo rotation will be determined by the mouse position
                // todo fix transform translate to work according to player aim
            }
            else if (playerMovement.moveLeft)
            {
                a.SetFloat(HashManager.Instance.animationParamsDict[AnimationParameters.faceDirection], -faceDirection);

                // facing right
                if (faceDirection == 1)
                {
                    movementDirection = -1;
                    // multiple by the speed graph value so that we can still move while we jump
                    p.transform.Translate(Vector3.forward * -speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                }
                // facing left
                else if (faceDirection == -1)
                {
                    if (!IsBlocked(p))
                    {
                        movementDirection = 1;
                        // multiple by the speed graph value so that we can still move while we jump
                        p.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                    }
                    else
                    {
                        UnityEngine.Debug.Log($"obstruction on the left");
                    }
                }
            }
            else
            {
                // go back to idle animation
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.move], false);
            }
        }

        // todo only move left or right if theres no obstruction
        /// <summary>
        /// Used to check whether the player has bumped into a wall
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool IsBlocked(PlayerMovement p)
        {
            if (movementDirection > 0)
            {
                spheresList = p.collisionSpheres.frontSphereGroundCheckers;
                directionBlock = 0.3f;
            }
            else if (movementDirection < 0)
            {
                spheresList = p.collisionSpheres.backSphereGroundCheckers;
                directionBlock = -0.3f;
            }

            return spheresList.Any((GameObject sphere) =>
            {
                // show the rays
                Debug.DrawRay(sphere.transform.position, p.transform.forward * directionBlock, Color.black);

                // the raycast will ignore the enemy layer and the attack radius layer
                bool didRayHitSomething = Physics.Raycast(sphere.transform.position, p.transform.forward * directionBlock, out RaycastHit hit, blockDistance, ~(1 << 8) & ~(1 << 9));

                if (didRayHitSomething)
                {
                    UnityEngine.Debug.Log($"checking to see if it was an enemy");
                    // check if it was an enemy
                    // if it was, make sure it's dead before crossing it, otherwise,
                    // will not be able to walk thru it
                    bool isEnemyDead = hit.transform.root.name.Contains("Ragdoll");
                    // found an enemy
                    if (isEnemyDead)
                    {
                        UnityEngine.Debug.Log($"enemy is dead");
                        return false;
                    }
                    UnityEngine.Debug.Log($"enemy is not dead");
                    UnityEngine.Debug.Log($"{hit.transform.root.name}");
                }

                if (didRayHitSomething)
                {
                    UnityEngine.Debug.Log($"the ray did hit something, but either the enemy isn't dead or the ray didn't hit an enemy");
                }

                // project a ray downwards
                return didRayHitSomething;
            });
        }
    }
}
