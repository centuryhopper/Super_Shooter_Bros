using UnityEngine;
using Game.Inputs;

namespace Game.PlayerCharacter
{
    public class PlayerController : MonoBehaviour
    {
        public bool jump;
        public bool moveLeft;
        public bool moveRight;

        /// <summary>
        /// Is true when player holds down
        /// shift or when player is "run" mode
        /// </summary>
        /// <value></value>
        public bool turbo;

        void Update()
        {
            jump = VirtualInputManager.Instance.jump;
            moveLeft = VirtualInputManager.Instance.moveLeft;
            moveRight = VirtualInputManager.Instance.moveRight;
            turbo = VirtualInputManager.Instance.turbo;
            print(turbo);
        }
    }
}
