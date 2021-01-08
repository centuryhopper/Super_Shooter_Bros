using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{

    [CreateAssetMenu(fileName = "SecondJump", menuName = "ability/SecondJump", order = 0)]
    public class SecondJump : StateData
    {
        [Range(1, 10)]
        public float secondJumpForce = 5.14f;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            PlayerMovement.numJumps -= 1;

            // get player rigidbody and apply force to the jump
            Rigidbody rb = character.GetPlayerMoveMent(a).RB;
            rb.AddForce(Vector3.up * secondJumpForce, ForceMode.Impulse);
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.secondJump], false);
        }

    }




        #region old code
        // [Range(1, 10)]
        // public float jumpForce = 5f;
        // Rigidbody rb;
        // bool isGrounded = true;
        // bool shouldJump;
        // readonly string jumpInput = "Jump";

        // void Awake()
        // {
        //     rb = GetComponent<Rigidbody>();
        // }

        // void Update()
        // {
        //     if (isGrounded)
        //     {
        //         shouldJump = Input.GetButtonDown(jumpInput);
        //     }
        //     else
        //     {
        //         shouldJump = false;
        //     }
        // }

        // void FixedUpdate()
        // {
        //     if (shouldJump)
        //     {
        //         print("jump");
        //         rb.velocity = new Vector3(0, 10, 0);
        //     }
        // }

        // void OnCollisionEnter(Collision c)
        // {
        //     if (c.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = true;
        //     }
        // }

        // void OnCollisionExit(Collision c)
        // {
        //     if (c.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = false;
        //     }
        // }
        #endregion
}
