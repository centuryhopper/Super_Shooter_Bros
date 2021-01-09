using Game.Enums;
using Game.Hash;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/jump", order = 0)]
    public class Jump : StateData
    {
        [Range(1, 10)]
        public float jumpForce;
        private PlayerMovement playerMovement = null;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            // ensure we don't play the landing animation too early
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.isGrounded], false);

            playerMovement = character.GetPlayerMoveMent(a);

            // get player rigidbody and apply force to the jump
            Rigidbody rb = character.GetPlayerMoveMent(a).RB;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            PlayerMovement.numJumps -= 1;
        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            // Debug.Log("second Jump: " + playerController.jump + "and num jumps: " + PlayerMovement.numJumps);

            // listen for another jump
            if (playerMovement.jump && PlayerMovement.numJumps == 1)
            {
                // Debug.Log("second jump triggered");

                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.secondJump], true);
            }
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);
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
