using System.Linq;
using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/ground detector", order = 0)]
    public class GroundDetector : StateData
    {
        [Range(.01f, 5)]
        public float distanceOfDetection;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            // throw new System.NotImplementedException();
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            PlayerMovement p = c.GetPlayerMoveMent(a);

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

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], false);
        }

        private bool IsGrounded(PlayerMovement p)
        {
            if (p.RB.velocity.y > -0.01f && p.RB.velocity.y <= 0)
            {
                return true;
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

            return p.groundCheckers.Any((GameObject obj) =>
            {
               // show the rays
               Debug.DrawRay(obj.transform.position, Vector3.down * distanceOfDetection, Color.black);

                RaycastHit hit;

               // project a ray downwards
               return (Physics.Raycast(obj.transform.position, Vector3.down, out hit, distanceOfDetection));
            });
        }
    }
}
