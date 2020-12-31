using UnityEngine;

namespace Game.Inputs
{
    public class KeyboardInputs : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager.Instance.moveRight = Input.GetKey(KeyCode.D);
            VirtualInputManager.Instance.moveLeft = Input.GetKey(KeyCode.A);
            VirtualInputManager.Instance.moveUp = Input.GetKey(KeyCode.W);
            VirtualInputManager.Instance.moveDown = Input.GetKey(KeyCode.S);
            VirtualInputManager.Instance.jump = Input.GetKeyDown(KeyCode.Space);
            VirtualInputManager.Instance.turbo = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);
        }
    }
}
