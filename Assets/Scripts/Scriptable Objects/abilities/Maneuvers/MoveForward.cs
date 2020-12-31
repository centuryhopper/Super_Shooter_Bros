using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/move", order = 0)]
    public class MoveForward : StateData
    {
        public AnimationCurve speedGraph;
        public float speed;
        private PlayerController playerController;
        private PlayerMovement playerMovement;
        public int facingSign;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            playerController = character.GetPlayerController(a);
            playerMovement = character.GetPlayerMoveMent(a);

            // float playerSkinFaceDirection = playerMovement.PlayerSkin.eulerAngles.y;
            // a.SetFloat(AnimationParameters.walkDirection.ToString(), 1);

        }

        override public void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            MovePlayer(playerMovement, a, asi);
        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(AnimationParameters.move.ToString(), false);
        }

        /// <summary>
        /// moves the player left and right
        /// </summary>
        void MovePlayer(PlayerMovement p, Animator a, AnimatorStateInfo asi)
        {
            float playerSkinFaceDirection = p.PlayerSkin.eulerAngles.y;

            // side scroller
            if (playerController.moveRight)
            {
                facingSign = playerSkinFaceDirection == 0 ? 1 : playerSkinFaceDirection == 180 ? -1 : 0;
                a.SetFloat(AnimationParameters.walkDirection.ToString(), facingSign);

                // multiple by the speed graph value so that we can still move while we jump
                p.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                // p.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                // p.transform.rotation = Quaternion.LookRotation(Vector3.forward, p.transform.up);

                // todo rotation will be determined by the mouse position
                // todo fix transform translate to work according to player aim
            }
            else if (playerController.moveLeft)
            {
                facingSign = playerSkinFaceDirection == 0 ? -1 : playerSkinFaceDirection == 180 ? 1 : 0;
                a.SetFloat(AnimationParameters.walkDirection.ToString(), facingSign);

                // multiple by the speed graph value so that we can still move while we jump
                p.transform.Translate(Vector3.forward * -speed * speedGraph.Evaluate(asi.normalizedTime) * Time.deltaTime);
                // p.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                // transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                // p.transform.rotation = Quaternion.LookRotation(-Vector3.forward, p.transform.up);
                // todo rotation will be determined by the mouse position
                // todo fix transform translate to work according to player aim
            }
            else
            {
                // go back to idle animation
                a.SetBool(AnimationParameters.move.ToString(), false);
            }
        }
    }
}
