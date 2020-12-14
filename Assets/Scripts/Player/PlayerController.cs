using UnityEngine;
using Game.Inputs;

namespace Game.PlayerCharacter
{
    public class PlayerController : MonoBehaviour
    {
        public bool jump;
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;

        /// <summary>
        /// Is true when player holds down
        /// shift or when player is "run" mode
        /// </summary>
        /// <value></value>
        public bool turbo;
        public bool secondJump;
        public LedgeChecker ledgeChecker { get; private set; }

        void Awake()
        {
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
        }

        void Update()
        {
            jump = VirtualInputManager.Instance.jump;
            moveLeft = VirtualInputManager.Instance.moveLeft;
            moveRight = VirtualInputManager.Instance.moveRight;
            moveUp = VirtualInputManager.Instance.moveUp;
            moveDown = VirtualInputManager.Instance.moveDown;
            turbo = VirtualInputManager.Instance.turbo;
            secondJump = VirtualInputManager.Instance.secondJump;
        }
    }
}
