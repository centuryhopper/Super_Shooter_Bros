using System.Linq;
using Game.Enums;
using Game.Hash;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/ground detector", order = 0)]
    public class GroundDetector : StateData
    {
        [Range(.01f, 5)]
        public float distanceOfDetection;
        private PlayerMovement p = null;

        override public void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            p = c.GetPlayerMoveMent(a);
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            if (IsGrounded(p))
            {
                // Debug.Log("on ground");
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], true);
            }
            else
            {
                // Debug.Log("falling");
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], false);
            }
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], false);
        }

        private bool IsGrounded(PlayerMovement p)
        {
            if (p.RB.velocity.y > -0.001f && p.RB.velocity.y <= 0)
            {
                if (p.contactPoints != null)
                {
                    foreach (ContactPoint c in p.contactPoints)
                    {
                        // get the bottom of collider using the center position
                        float colliderBottom = (p.transform.position.y + p.BoxCollider.center.y) - (p.BoxCollider.size.y / 2f);

                        // then compare that to the contact point
                        float yDifference = Mathf.Abs(c.point.y - colliderBottom);

                        // this if check works properly
                        // but Mathf.Approximately on yDiff and 0
                        // did not, so keep in mind not to use that method unless
                        // you absolutely know what you're doing
                        if (yDifference <= .01f)
                        {
                            return true;
                        }
                    }
                }
            }

            #region old code (replaced by the linq statement below)
            // foreach (GameObject obj in p.groundCheckers)
            // {
            //     // show the rays
            //     Debug.DrawRay(obj.transform.position, Vector3.down * distanceOfDetection, Color.black);

            //     RaycastHit hit;

            //     // project a ray downwards
            //     if (Physics.Raycast(obj.transform.position, Vector3.down, out hit, distanceOfDetection))
            //     {
            //         return true;
            //     }
            // }
            #endregion

            return p.collisionSpheres.bottomSphereGroundCheckers.Any((GameObject obj) =>
            {
                // show the rays
                Debug.DrawRay(obj.transform.position, Vector3.down * distanceOfDetection, Color.black);

                // project a ray downwards
                return (Physics.Raycast(obj.transform.position, Vector3.down, out RaycastHit hit, distanceOfDetection));
            });
        }
    }
}
